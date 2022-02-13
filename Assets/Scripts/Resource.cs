using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(SpriteRenderer))]

public class Resource : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();
    [SerializeField] private AudioSource _onPickUpSound;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _reached?.Invoke();
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
            Destroy(gameObject, _onPickUpSound.clip.length);
        }
    }
}
