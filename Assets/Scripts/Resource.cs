using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]

public class Resource : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private AudioSource _onPickUpSound;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _onPickUpSound = GetComponent<AudioSource>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _reached?.Invoke();
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
            StartCoroutine(DeleteAfterPlayingEffects());
        }
    }

    private IEnumerator DeleteAfterPlayingEffects()
    {
        var WaitForEffectsEnding = new WaitForSeconds(_onPickUpSound.clip.length + 1F);

        yield return WaitForEffectsEnding;

        Destroy(gameObject);
    }
}
