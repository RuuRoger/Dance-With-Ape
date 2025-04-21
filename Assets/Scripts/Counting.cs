using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System;

public class Counting : MonoBehaviour
{
    #region Fields
    [Header("Audios")]
    [SerializeField] private AudioClip _bipCounter;
    [SerializeField] private AudioClip _bipFinishCounter;

    [Header("Buttons to Control")]
    [SerializeField] private GameObject[] _buttons;

    private TMP_Text _textCountingToStartRound;
    private AudioSource _textAudioSource;

    #endregion

    #region Events
    public static event Action OnStartDanceMonkey; //Goes to "Monkey"

    #endregion

    #region Methods

    //Let start coroutine
    private void TriggerCountingRound() => StartCoroutine(CountingToStartRound());

    /*
    This croutine do:
    1. Make numbers/text visible
    2. DIseable buttons
    3.Count to "start" with a yoyo efect
    4. Enable again the buttons
    5. Notify to Monkey that can start to dance
    */

    private IEnumerator CountingToStartRound()
    {
        _textCountingToStartRound.alpha = 1f;

        foreach (GameObject button in _buttons)
        {
            button.SetActive(false);
        }

        for (int i = 3; i > 0; i--)
        {
            _textCountingToStartRound.text = i.ToString();
            _textAudioSource.clip = _bipCounter;
            _textAudioSource.Play();
            yield return _textCountingToStartRound.transform
                .DOScale(1.5f, 0.25f)
                .SetLoops(2, LoopType.Yoyo)
                .WaitForCompletion();
        }

        _textCountingToStartRound.text = "Go!";
        _textAudioSource.clip = _bipFinishCounter;
        _textAudioSource.Play();
        yield return _textCountingToStartRound.DOFade(0, 0.5f).WaitForCompletion();

        foreach (GameObject button in _buttons)
        {
            button.SetActive(true);
        }

        OnStartDanceMonkey?.Invoke();
    }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        _textCountingToStartRound = GetComponent<TMP_Text>();
        _textAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ButtonController.OnTurnCountAgain += TriggerCountingRound;
    }

    private void OnDisable()
    {
        ButtonController.OnTurnCountAgain -= TriggerCountingRound;
    }

    private void Start()
    {
        StartCoroutine(CountingToStartRound());
    }

    #endregion
}
