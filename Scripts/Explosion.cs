using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private CircleCollider2D _explosionCollider;
    [SerializeField] private float _maximumRadius;
    [SerializeField] private float _speed;

    private void OnEnable()
    {
        _bomb.IsExploded += OnIsExploded;
    }

    private void OnDisable()
    {
        _bomb.IsExploded -= OnIsExploded;
    }

    private void OnIsExploded()
    {
        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        _explosionCollider.radius = 0.1f;
        while (_explosionCollider.radius < _maximumRadius)
        {
            _explosionCollider.radius = Mathf.MoveTowards(_explosionCollider.radius, _maximumRadius, _speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
