using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _enemyRenderer;
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _down;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _left;
    [SerializeField] private Sprite _angryUp;
    [SerializeField] private Sprite _angryDown;
    [SerializeField] private Sprite _angryRight;
    [SerializeField] private Sprite _angryLeft;
    [SerializeField] private Vector2 _positionX;
    [SerializeField] private Vector2 _positionY;

    private Vector2 _position;
    private bool _isFinished = true;

    private Coroutine _move = null;

    private void Update()
    {
        if (_isFinished == true)
        {
            _move = StartCoroutine(MoveEnemy());
        }
    }
    private IEnumerator MoveEnemy()
    {
        _isFinished = false;
        _position = new Vector2(transform.position.x, transform.position.y);
        Vector2 leftTargetPosition = _position + Vector2.left * 2;
        Vector2 rightTargetPosition = _position + Vector2.right * 2;
        Vector2 upTargetPosition = _position + Vector2.up * 2;
        Vector2 downTargetPosition = _position + Vector2.down * 2;

        Vector3 targetLeft = new Vector3(leftTargetPosition.x, leftTargetPosition.y, transform.position.z);
        Vector3 targetRight = new Vector3(rightTargetPosition.x, rightTargetPosition.y, transform.position.z);
        Vector3 targetUp = new Vector3(upTargetPosition.x, upTargetPosition.y, transform.position.z);
        Vector3 targetDown = new Vector3(downTargetPosition.x, downTargetPosition.y, transform.position.z);

        int randomDirectionNumber = Random.Range(1, 5);
        switch (randomDirectionNumber)
        {
            case 1:
                while (transform.position.x - targetLeft.x > 0.3f && transform.position.x > _positionX.x)
                {
                    _enemyRenderer.sprite = _left;
                    transform.position = Vector3.MoveTowards(transform.position, targetLeft, _speed * Time.deltaTime);
                    yield return null;
                }
                break;
            case 2:
                while (targetRight.x - transform.position.x > 0.3f && transform.position.x < _positionX.y)
                {
                    _enemyRenderer.sprite = _right;
                    transform.position = Vector2.MoveTowards(transform.position, targetRight, _speed * Time.deltaTime);
                    yield return null;
                }
                break;
            case 3:
                while (targetUp.y - transform.position.y > 0.3f && transform.position.y < _positionY.y)
                {
                    _enemyRenderer.sprite = _up;
                    transform.position = Vector3.MoveTowards(transform.position, targetUp, _speed * Time.deltaTime);
                    yield return null;
                }
                break;
            case 4:
                while (transform.position.y - targetDown.y > 0.3f && transform.position.y > _positionY.x)
                {
                    _enemyRenderer.sprite = _down;
                    transform.position = Vector3.MoveTowards(transform.position, targetDown, _speed * Time.deltaTime);
                    yield return null;
                }
                break;
        }
        _isFinished = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Explosion explosion))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _health--;
            _enemyRenderer.color = Color.red;
            _speed += 1;

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.TryGetComponent(out Explosion explosion))
        {
            _health--;
            _up = _angryUp;
            _down = _angryDown;
            _left = _angryLeft;
            _right = _angryRight;
            _speed += 1;

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
