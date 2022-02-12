using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpDelay;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _currentDirection;
    private Vector2 _spriteDirection;
    private float _elapsedAfterJump;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteDirection = new Vector2(-1, 0);
        _elapsedAfterJump = _jumpDelay;
    }

    private void Update()
    {
        
        SetMoveDirection();
        Move();
        _elapsedAfterJump += Time.deltaTime;

        if (_elapsedAfterJump > _jumpDelay)
            Jump();
    }

    private void SetMoveDirection()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.A))
            {
                _currentDirection.x = -1;
                _currentDirection.y = 0;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _currentDirection.x = 1;
                _currentDirection.y = 0;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _currentDirection.y = 1;
            }
            return;
        }
        _currentDirection = Vector2.zero;
    }

    private void SetSpriteDirection()
    {
        if (_currentDirection.x == _spriteDirection.x)
        {
            return;
        }
        else if (_currentDirection.x == -1)
        {
            _spriteDirection.x = -1;
            _spriteRenderer.flipX = true;
        }
        else if (_currentDirection.x == 1)
        {
            _spriteDirection.x = 1;
            _spriteRenderer.flipX = false;
        }
    }

    private void Move()
    {
        if (_currentDirection.x == 0)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }
        SetSpriteDirection();
        _animator.SetBool("IsRunning", true);
        transform.Translate(_currentDirection.x * _speed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        if (_currentDirection.y == 0)
            return;

        _rigidbody.AddForce(Vector2.up * _jumpStrength);
        _elapsedAfterJump = 0;
    }
}
