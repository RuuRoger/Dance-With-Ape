using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    #region Fields
    [SerializeField] private int _lives;
    [SerializeField] private GameObject _crossError;
    [SerializeField] private Button[] _buttonsToPlay;
    [SerializeField] private AudioSource _crossBipError;
    [SerializeField] private AudioClip _bipError;

    private List<int> _listSequence = new List<int>();
    private List<int> _playerSequence = new List<int>();

    #endregion

    #region Properties
    public List<int> ListSequence => _listSequence;

    #endregion

    #region Events
    public static event Action OnTurnCountAgain; // Goes to "Counting"
    public static event Action OnStopDanceMonkey; // Goes to "Monkey"
    public static event Action OnFadeUI; //Goes to ScoreAndLives
    public static event Action OnLivesUI;

    #endregion

    #region Methods

    // Generate unique values for the starting sequence
    private void GenerateInitialSequence()
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

        Debug.Log($"Initial sequence: {string.Join(", ", _listSequence)}");
    }

    // Handle button press and add value to player's sequence
    private void HandleButtonPress(int value)
    {
        _playerSequence.Add(value);
        Debug.Log($"Button pressed: {value}");
        CheckPlayerSequence();
    }

    // Check if player's sequence matches the monkey's sequence
    private void CheckPlayerSequence()
    {
        for (int i = 0; i < _playerSequence.Count; i++)
        {
            if (_playerSequence[i] != _listSequence[i])
            {
                StartCoroutine(HandleError());
                OnLivesUI?.Invoke();
                _playerSequence.Clear();
                return;
            }
        }

        // Move to the next round if the sequence matches completely
        if (_playerSequence.Count == _listSequence.Count)
        {
            Debug.Log("Round complete! Generating next sequence...");
            _playerSequence.Clear();
            _listSequence.Add(UnityEngine.Random.Range(1, 5));
            OnStopDanceMonkey?.Invoke();
            OnFadeUI?.Invoke();
            OnTurnCountAgain?.Invoke();
        }
    }

    // Handle error state visually and reset
    private IEnumerator HandleError()
    {
        Time.timeScale = 0;
        _crossError.SetActive(true);
        _crossBipError.clip = _bipError;
        _crossBipError.Play();

        yield return new WaitForSecondsRealtime(0.5f);
        _crossError.SetActive(false);
        Time.timeScale = 1;
    }

    // Let use buttons or not
    private void EnableAllButtons()
    {
        foreach (Button button in _buttonsToPlay)
            button.interactable = true;
    }

    private void DisableAllButtons()
    {
        foreach (Button button in _buttonsToPlay)
            button.interactable = false;
    }

    //Diseable buttons
    private void TurnOffButtons()
    {
        foreach (Button button in _buttonsToPlay)
        {
            button.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        GenerateInitialSequence();
        DisableAllButtons();
    }

    private void OnEnable()
    {
        ButtonPress.OnButtonTouched += HandleButtonPress;
        Monkey.OnEnableButtons += EnableAllButtons;
        Monkey.OnDisableButtons += DisableAllButtons;
        ScoreAndLives.OnFinishGame += TurnOffButtons;
    }

    private void OnDisable()
    {
        ButtonPress.OnButtonTouched -= HandleButtonPress;
        Monkey.OnEnableButtons -= EnableAllButtons;
        Monkey.OnDisableButtons -= DisableAllButtons;
        ScoreAndLives.OnFinishGame -= TurnOffButtons;
    }

    #endregion
}
