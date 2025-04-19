using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerClickHandler
{
    //Field
    [SerializeField] private int _buttonID;

    //Events
    public static event Action<int> UpButtonTouched;

    //This method can be use with muse or hand!!!!!!!!!
    public void OnPointerClick(PointerEventData eventData) => Touch();

    private void Touch()
    {
        UpButtonTouched?.Invoke(_buttonID);
        Debug.Log(_buttonID);
    }

}
