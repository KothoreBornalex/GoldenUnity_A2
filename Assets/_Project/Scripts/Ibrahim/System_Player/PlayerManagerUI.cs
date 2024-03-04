using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PlayerManager _player;


    // Start is called before the first frame update
    void Start()
    {
        _canvas.worldCamera = Camera.main;
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        if(_player != null) _player.IsSelected += _player_IsSelected;
        if (_player != null) _player.IsUnSelected += _player_IsUnSelected;
    }

    private void OnDisable()
    {
        if (_player != null) _player.IsSelected -= _player_IsSelected;
        if (_player != null) _player.IsUnSelected -= _player_IsUnSelected;
    }

    private void _player_IsSelected()
    {
        _canvas.enabled = true;
    }

    private void _player_IsUnSelected()
    {
        _canvas.enabled = false;
    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
