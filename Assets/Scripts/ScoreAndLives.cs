using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Collections;

public class ScoreAndLives : MonoBehaviour
{
    [SerializeField] private TMP_Text _bannanaScore;
    [SerializeField] private TMP_Text _livesTextUI;
    [SerializeField] private GameObject _banana;
    [SerializeField] private GameObject _monkeyUI;
    [SerializeField] private int _livesValue;
    [SerializeField] private GameObject _containerUIMenuGame;
    private int _score;
    private AudioSource _gameOversound;
    private bool _gameOverFlag; //This flag is to play sund one time only

    public static event Action OnFinishGame; //Goes to Monkey, ButtonPress, ButtonControler, Counting

    private void ShowUI()
    {
        //Banana UI
        _score++;
        if ((_score % 5 == 0) && _score > 0)
        {
            _livesValue++;
            _livesTextUI.text = _livesValue.ToString();
            Debug.Log($"Vidas: {_livesValue}");
        }
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
        if (_livesValue <= 0 && !_gameOverFlag)
        {
            _gameOverFlag = true;

            Debug.Log("Game over!");
            OnFinishGame?.Invoke();

            StartCoroutine(MenuGameOver());
           
        }
    }

    private IEnumerator MenuGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        _containerUIMenuGame.SetActive(true);
        _gameOversound.Play();
    }


    private void CurrentLives()
    {
        _livesValue = int.Parse(_livesTextUI.text);
    }

    private void Awake()
    {
        _score = int.Parse(_bannanaScore.text);
        _livesTextUI.text = _livesValue.ToString();
        _gameOversound = GetComponent<AudioSource>();
        _gameOverFlag = false;

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


}
