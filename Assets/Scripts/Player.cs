using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _collectedResourcesAmount;

    private UnityEvent _resourcePickuped;

    public event UnityAction ResourcePickuped
    {
        add => _resourcePickuped.AddListener(value);
        remove => _resourcePickuped.RemoveListener(value);
    }
    
    public int RescourceAmount => _collectedResourcesAmount;

    private void Start()
    {
        _collectedResourcesAmount = 0;
        _resourcePickuped = new UnityEvent();
    }

    public void OnResourcePickUp()
    {
        _collectedResourcesAmount++;
        _resourcePickuped.Invoke();
    }
}
