using System;
using UnityEngine;
using System.Collections.Generic;


public class ButtonController : MonoBehaviour
{
    #region Fields
    [SerializeField] private int _lives;
    private List<int> _listSequence = new List<int>();
    private List<int> _playerSequence = new List<int>();

    #endregion

    #region Properties
    public List<int> ListSequence
    {
        get { return _listSequence; }
        set { }
    }

    #endregion

    #region Events
    public static event Action OnFinishGame;

    #endregion

    #region Methods

    //Create 3 unique values to start the game
    private void RandmomValuesToStart()
    {
        for (int i = 0; i < 3; i++)
        {
            int newValue;

            do
            {
                newValue = UnityEngine.Random.Range(1, 5);
            }
            while (_listSequence.Contains(newValue));

            _listSequence.Add(newValue);
        }

        foreach (int value in _listSequence)
            Debug.Log($"Valor generado: {value}");
    }

    //Look what button was touched to then says if is correct or not
    private void ButtonsValueSequencie(int value)
    {


        switch (value)
        {
            case 1:
                Debug.Log($"Se ha pulsado {value}");
                break;

            case 2:
                Debug.Log($"Se ha pulsado {value}");
                break;

            case 3:
                Debug.Log($"Se ha pulsado {value}");
                break;

            case 4:
                Debug.Log($"Se ha pulsado {value}");
                break;

                //Not necessary default
        }

        _playerSequence.Add(value);
        CheckPlayerSequence();
    }

    //Check if value is equals with the list for the monkey movement
    private void CheckPlayerSequence()
    {
        for (int i = 0; i < _playerSequence.Count; i++)
        {
            if (_playerSequence[i] != _listSequence[i])
            {
                Debug.Log("Has fallado");
                _lives--;
                Debug.Log($"Vidas: {_lives}");
                _playerSequence.Clear();
                return;
            }
        }

        if (_playerSequence.Count == _listSequence.Count)
            Debug.Log("Siguiente ronda");
    }

    //Looks if lives is 0 or not, to notify stop the game
    private void GameOver()
    {
        if (_lives == 0)
            OnFinishGame?.Invoke(); //Goes to ButtonPRess
    }

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        RandmomValuesToStart();
    }

    private void OnEnable()
    {
        ButtonPress.OnButtonTouched += ButtonsValueSequencie;
    }

    private void OnDisable()
    {
        ButtonPress.OnButtonTouched -= ButtonsValueSequencie;
    }

    private void Update()
    {
        GameOver();
    }

    #endregion

}
