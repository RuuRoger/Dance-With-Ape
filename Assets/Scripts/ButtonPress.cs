using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour, IPointerDownHandler
{
    //Field
    [SerializeField] private int _buttonID;

    //Events
    public static event Action<int> UpButtonTouched;

    //This method can be use with muse or hand!!!!!!!!!
    public void OnPointerDown(PointerEventData eventData) => Touch();

    private void Touch()
    {
        UpButtonTouched?.Invoke(_buttonID);
        Debug.Log(_buttonID);
    }

}
