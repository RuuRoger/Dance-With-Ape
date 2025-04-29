using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using DG.Tweening;
using System;

public class ScoreAndLives : MonoBehaviour
{
    [SerializeField] private TMP_Text _bannanaScore;
    [SerializeField] private TMP_Text _livesTextUI;
    [SerializeField] private GameObject _banana;
    [SerializeField] private GameObject _monkeyUI;
    [SerializeField] private int _livesValue;
    private int _score;
    private int _scoreNumber;

    public static event Action OnFinishGame; //Goes to "Monkey"

    private void ShowUI()
    {
        //Banana UI
        _score++;
        _bannanaScore.text = _score.ToString();
        _banana.SetActive(true);
        _bannanaScore.alpha = 1f;

        //Lives UI
        _monkeyUI.SetActive(true);
        _livesTextUI.alpha = 1f;

    }

    private void DontShowUI()
    {
        //Banana UI
        _banana.SetActive(false);
        _bannanaScore.DOFade(0, 0.1f);

        //Lives UI
        _monkeyUI.SetActive(false);
        _livesTextUI.DOFade(0, 0.1f);
    }

    private void LessLives()
    {
        _livesValue--;
        _livesTextUI.text = _livesValue.ToString();
        Debug.Log($"Vidas: {_livesValue}");
    }

    private void CheckGameOver()
    {
        if (_livesValue <= 0)
        {
            Debug.Log("Game over!");
            OnFinishGame?.Invoke();
        }
    }

    private void CurrentLives()
    {
        _livesValue = int.Parse(_livesTextUI.text);
    }

    private void Awake()
    {
        _score = int.Parse(_bannanaScore.text);
        _livesTextUI.text = _livesValue.ToString();

    }

    private void OnEnable()
    {
        Monkey.OnShowScoreAndLive += ShowUI;
        ButtonController.OnFadeUI += DontShowUI;
        ButtonController.OnLivesUI += LessLives;
    }

    private void OnDisable()
    {
        Monkey.OnShowScoreAndLive -= ShowUI;
        ButtonController.OnFadeUI -= DontShowUI;
        ButtonController.OnLivesUI -= LessLives;
    }

    private void Update()
    {
        CheckGameOver();
        CurrentLives();
    }

    //Falta saber si funciona
    //Correctamente la asignacion de puntos
}
