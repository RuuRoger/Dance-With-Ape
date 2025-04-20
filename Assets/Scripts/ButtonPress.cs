using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour, IPointerClickHandler
{
    #region Fields
    [SerializeField] private int _buttonID;

    #endregion

    #region Events
    public static event Action<int> OnButtonTouched;

    #endregion

    #region Methods

    //This method can be use with muse or hand!!!!!!!!!
    public void OnPointerClick(PointerEventData eventData) => Touch();

    private void Touch()
    {
        //Goes to "ButtonController"
        OnButtonTouched?.Invoke(_buttonID);
    }

    private void DiseableObject() => this.enabled = false;

    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        ButtonController.OnFinishGame += DiseableObject;
    }

    private void OnDisable()
    {
        ButtonController.OnFinishGame -= DiseableObject;
    }

    #endregion

}
