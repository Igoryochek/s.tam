using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _lifetime;
    [SerializeField] private GameObject _explosion;

    public event UnityAction IsExploded;

    private void OnEnable()
    {
        _player.BombIsActive += OnBombIsActive;
    }

    private void OnDisable()
    {
        _player.BombIsActive += OnBombIsActive;
    }

    private void OnBombIsActive()
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(_lifetime);
        _explosion.transform.position = transform.position;
        _explosion.SetActive(true);
        IsExploded.Invoke();
    }
}
