using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System;

public class Counting : MonoBehaviour
{
    [SerializeField] private AudioClip _bipCounter;
    [SerializeField] private AudioClip _bipFinishCounter;
    private TMP_Text _textCountingToStartRound;
    private AudioSource _textAudiosource;

    //Events
    public static event Action OnStartDanceMonkey;

    IEnumerator CountingToStartRound()
    {
        _textCountingToStartRound.color = new Color(
        _textCountingToStartRound.color.r,
        _textCountingToStartRound.color.g,
        _textCountingToStartRound.color.b,
        1);

        _textAudiosource.clip = _bipCounter;
        _textCountingToStartRound.text = 3.ToString();
        _textAudiosource.Play();
        yield return _textCountingToStartRound.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo).WaitForCompletion();

        _textCountingToStartRound.text = 2.ToString();
        _textAudiosource.Play();
        yield return _textCountingToStartRound.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo).WaitForCompletion();

        _textCountingToStartRound.text = 1.ToString();
        _textAudiosource.Play();
        yield return _textCountingToStartRound.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo).WaitForCompletion();

        _textCountingToStartRound.text = "Go!";
        _textAudiosource.clip = _bipFinishCounter;
        _textAudiosource.Play();
        yield return _textCountingToStartRound.DOFade(0, 0.5f).WaitForCompletion(); // Desvanecimiento completo

        OnStartDanceMonkey?.Invoke();
    }

    private void Awake()
    {
        _textCountingToStartRound = GetComponent<TMP_Text>();
        _textAudiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(CountingToStartRound());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
            StartCoroutine(CountingToStartRound());
    }
}
