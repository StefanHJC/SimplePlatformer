using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _collectedResourcesAmount;

    public int RescourceAmount => _collectedResourcesAmount;

    private void Start()
    {
        _collectedResourcesAmount = 0;
    }

    public void OnResourcePickUp()
    {
        _collectedResourcesAmount++;
    }
}
