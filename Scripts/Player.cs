using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;

    public event UnityAction BombIsActive;

    private void OnEnable()
    {
        _bomb.IsExploded += OnIsExploded;
    }

    private void OnDisable()
    {
        _bomb.IsExploded -= OnIsExploded;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void OnIsExploded()
    {
        _bomb.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Food food))
        {
            Destroy(collision.gameObject);
            //isAte.Invoke();
        }
    }

    public void InstantiateBomb()
    {
        _bomb.transform.position = transform.position;
        _bomb.gameObject.SetActive(true);
        BombIsActive.Invoke();
    }
}
