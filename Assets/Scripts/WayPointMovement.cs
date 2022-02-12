using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class WayPointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _wayPoints;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _spriteDirection;
    private int _currentPointIndex;

    private void Start()
    {
        _currentPointIndex = 0;
        _spriteDirection = Vector2.zero;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wayPoints = new Transform[_path.childCount];

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        MoveByPath();   
    }

    private void MoveByPath()
    {
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (transform.position.x == _wayPoints[i].position.x)
                _currentPointIndex++;

            if (_currentPointIndex >= _wayPoints.Length)
                _currentPointIndex = 0;

            SetSpriteDirection();
            MoveTo(_wayPoints[_currentPointIndex]); 
        }
    }

    private void SetSpriteDirection()
    {
        var currentDirection = GetDirection(_wayPoints[_currentPointIndex]);

        if (currentDirection.normalized.x == _spriteDirection.x)
        {
            Debug.Log("HUI");
            return;
        }
        else if (currentDirection.normalized.x == -1)
        {
            _spriteDirection.x = -1;
            _spriteRenderer.flipX = true;
        }
        else if (currentDirection.normalized.x == 1)
        {
            _spriteDirection.x = 1;
            _spriteRenderer.flipX = false;
        }
    }

    private Vector2 GetDirection(Transform point)
    {
        return (transform.position - point.position).normalized;
    }

    private void MoveTo(Transform point)
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, _speed * Time.deltaTime);
    }
}
