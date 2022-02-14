using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _template;
    [SerializeField] private Transform _spawnTarget;
    [SerializeField] private float _spawnDelay;

    private Transform[] _spawnPoints;
    private float _elapsedTime;
    private int _currentPoint;

    private void Start()
    {
        _spawnPoints = new Transform[_spawnTarget.childCount];
        _elapsedTime = _spawnDelay;

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = _spawnTarget.GetChild(i);
            SpawnObject(i);
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime < _spawnDelay)
            return;

        if (_currentPoint >= _spawnTarget.childCount)
            _currentPoint = 0;

        SpawnObjectIfPointFree();
    }

    private void SpawnObject(int index)
    {
        GameObject newObject = Instantiate(_template.gameObject, _spawnPoints[index]);
    }

    private void SpawnObjectIfPointFree()
    {
        if (_spawnPoints[_currentPoint].childCount > 0)
        {
            _currentPoint++;
            return;
        }
        SpawnObject(_currentPoint);
        _elapsedTime = 0;
    }
}
