using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _down;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _left;
    [SerializeField] private Vector2 _borderX;
    [SerializeField] private Vector2 _borderY;
    [SerializeField] private GameObject _failText;

    private Vector3 _currentDirection;
    private float _currentSpeed = 0;

    private void Update()
    {
        MovePlayer(_currentDirection);
    }

    public void MoveRight()
    {
        _currentSpeed = _speed;
        _currentDirection = Vector2.right;
        _player.sprite = _right;
    }
    public void Stop()
    {
        _currentSpeed = 0;
    }
    public void MoveLeft()
    {
        _currentSpeed = _speed;
        _currentDirection = Vector2.left;
        _player.sprite = _left;
    }

    public void MoveUp()
    {
        _currentSpeed = _speed;
        _currentDirection = Vector2.up;
        _player.sprite = _up;
    }
    public void MoveDown()
    {
        _currentSpeed = _speed;
        _currentDirection = Vector2.down;
        _player.sprite = _down;
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 target = new Vector3(direction.x, direction.y, transform.position.z);
        transform.Translate(target * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health--;
            if (_health <= 0)
            {
                Time.timeScale = 0;
                _failText.SetActive(true);
            }
        }

        if (collision.gameObject.TryGetComponent(out Explosion explosion))
        {
            _health--;
            if (_health <= 0)
            {
                Time.timeScale = 0;
                _failText.SetActive(true);
            }
        }
    }
}
