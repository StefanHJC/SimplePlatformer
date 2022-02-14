using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private TMP_Text _count;

    private void Start()
    {
        _count = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowCurrentAmount();
    }

    private void ShowCurrentAmount()
    {
        _count.text = _player.RescourceAmount.ToString();
    }
}
