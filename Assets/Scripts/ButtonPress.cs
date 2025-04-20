using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerClickHandler
{
    #region Fields
    [SerializeField] private int _buttonID;
    private bool _isInteractable;

    #endregion

    #region Events
    public static event Action<int> OnButtonTouched; // Goes to ButtonController

    #endregion

    #region Methods

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInteractable)
            Touch();
    }

    private void Touch()
    {
        OnButtonTouched?.Invoke(_buttonID);
    }

    public void EnableButton() => _isInteractable = true;
    public void DisableButton() => _isInteractable = false;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _isInteractable = false;
    }

    private void OnEnable()
    {
        ButtonController.OnFinishGame += DisableButton;
        Monkey.OnEnableButtons += EnableButton;
    }

    private void OnDisable()
    {
        ButtonController.OnFinishGame -= DisableButton;
    }

    #endregion
}

