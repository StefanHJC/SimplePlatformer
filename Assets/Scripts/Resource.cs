using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();

    private string _onSpawnEffect;
    private string _onDestroyEffect;

    private void Start()
    {
        _onDestroyEffect = "¡¿’Õ”À œ»¬¿";
        _onSpawnEffect = "¡”À‹ ¡”À‹ ¡”À‹";

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _reached?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log(_onDestroyEffect);
    }

    private void OnEnable()
    {
    }
}
