using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(SpriteRenderer))]

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
        void SetLeft()
        {
            _currentDirection.x = -1;
            _currentDirection.y = 0;
        }
        void SetRight()
        {
            _currentDirection.x = 1;
            _currentDirection.y = 0;
        }
        void SetJump() => _currentDirection.y = 1;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.A))
            {
                SetLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                SetRight();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                SetJump();
            }
            return;
        }
        _currentDirection = Vector2.zero;
    }

    private void SetSpriteDirection()
    {
        void SetRight()
        {
            _spriteDirection.x = -1;
            _spriteRenderer.flipX = true;
        }
        void SetLeft()
        {
            _spriteDirection.x = 1;
            _spriteRenderer.flipX = false;
        }
        if (_currentDirection.x == _spriteDirection.x)
            return;

        else if (_currentDirection.x == -1)
            SetRight();

        else if (_currentDirection.x == 1)
            SetLeft();
    }

    private void Move()
    {
        if (_currentDirection.x == 0)
        {
            _animator.SetBool(AnimatorPlayer.Params.States.IsRunning, false);
            return;
        }
        SetSpriteDirection();
        _animator.SetBool(AnimatorPlayer.Params.States.IsRunning, true);
        transform.Translate(_currentDirection.x * _speed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        if (_currentDirection.y == 0)
            return;

        _animator.SetTrigger(AnimatorPlayer.Params.States.Jump);
        _rigidbody.AddForce(Vector2.up * _jumpStrength);
        _elapsedAfterJump = 0;
    }
}
