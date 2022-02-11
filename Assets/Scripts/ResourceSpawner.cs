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
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_currentPoint >= _spawnTarget.childCount || _elapsedTime < _spawnDelay)
            return;

        SpawnObject();
        _currentPoint++;
        _elapsedTime = 0;
    }

    private void SpawnObject()
    {
        GameObject newObject = Instantiate(_template.gameObject, _spawnPoints[_currentPoint]);
    }
}
