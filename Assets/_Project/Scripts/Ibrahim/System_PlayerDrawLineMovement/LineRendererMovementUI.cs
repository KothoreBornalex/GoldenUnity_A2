using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererMovementUI : MonoBehaviour
{
    [SerializeField] private LineRendererMovement _lineRendererMovement;
    [SerializeField] private Button _buttonGO;
    [SerializeField] private GameObject _buttonHideGO;
    [SerializeField] private Button _buttonClearPath;
    [SerializeField] private GameObject _buttonHideClearPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(_lineRendererMovement != null) _lineRendererMovement.ToggleReadyToMove += _lineRendererMovement_IsReadyToMove;
        if (_lineRendererMovement != null) _lineRendererMovement.ToggleClearPath += _lineRendererMovement_ToggleClearPath;
    }

    private void OnDisable()
    {
        if (_lineRendererMovement != null) _lineRendererMovement.ToggleReadyToMove -= _lineRendererMovement_IsReadyToMove;
        if (_lineRendererMovement != null) _lineRendererMovement.ToggleClearPath -= _lineRendererMovement_ToggleClearPath;

    }
    private void _lineRendererMovement_ToggleClearPath(bool value)
    {
        if (value)
        {
            _buttonClearPath.interactable = true;
            _buttonHideClearPath.SetActive(false);
        }
        else
        {
            _buttonClearPath.interactable = false;
            _buttonHideClearPath.SetActive(true);
        }
    }

    private void _lineRendererMovement_IsReadyToMove(bool value)
    {
        if(value)
        {
            _buttonGO.interactable = true;
            _buttonHideGO.SetActive(false);
        }
        else
        {
            _buttonGO.interactable = false;
            _buttonHideGO.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
