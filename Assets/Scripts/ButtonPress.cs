using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerUpHandler
{
    #region Fields
    [Header("Buttons ID")]
    [SerializeField] private int _buttonID;
    private bool _isInteractable;

    #endregion

    #region Events
    public static event Action<int> OnButtonTouched; // Goes to ButtonController

    #endregion

    #region Methods

    //If boolean is true, let use buttons
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            TriggerButton();
        }
    }

    //Notify what button press the player
    private void TriggerButton() => OnButtonTouched?.Invoke(_buttonID);

    //Flag. Let active buttons
    public void EnableButton() => _isInteractable = true;

    //Flag. Diseable use buttons
    public void DisableButton() => _isInteractable = false;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        _isInteractable = false;
    }

    private void OnEnable()
    {
        ScoreAndLives.OnFinishGame += DisableButton;
        Monkey.OnEnableButtons += EnableButton;
        Monkey.OnDisableButtons += DisableButton;

    }

    private void OnDisable()
    {
        ScoreAndLives.OnFinishGame -= DisableButton;
        Monkey.OnEnableButtons -= EnableButton;
        Monkey.OnDisableButtons -= DisableButton;
    }

    #endregion
}
