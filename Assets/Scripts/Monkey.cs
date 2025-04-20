using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    #region Fields
    [SerializeField] private Sprite _monkeySpriteUp;
    [SerializeField] private Sprite _monkeySpriteRight;
    [SerializeField] private Sprite _monkeySpriteDown;
    [SerializeField] private Sprite _monkeySpriteLeft;
    [SerializeField] private GameObject _scriptPressButton;

    private Animator _monkeyAnimator;
    private SpriteRenderer _monkeySpriteRender;
    private ButtonController _allSequence;
    private AudioSource _monkeyAudioSource;

    #endregion

    //Events
    public static event Action OnEnableButtons; //Goes to ButtonPress
    public static event Action OnDiseableButtons; //Goes to ButtonPress

    #region Methods

    private void CallDance()
    {
        _monkeySpriteRender.enabled = true;
        StartCoroutine(CountingToStartRound());

    }
    //Give 3 seconds to leet dance the monkey. Then, call a method to make sequency
    IEnumerator CountingToStartRound()
    {
        // Debug.Log("Inicia Corrutina");
        // yield return new WaitForSeconds(3f);
        // _monkeyAnimator.enabled = false;

        OnDiseableButtons?.Invoke();

        foreach (int value in _allSequence.ListSequence)
        {
            ChangeSprite(value);
            yield return new WaitForSeconds(1f);
        }

        _monkeySpriteRender.flipY = false; //Reset
        _monkeyAnimator.enabled = true;
        OnEnableButtons?.Invoke();
    }

    //Change the sprites base on the values in the list and play sound in movement
    private void ChangeSprite(int value)
    {
        switch (value)
        {
            //Is it necessary indicate flipY because doesen't works well if only put in case 3
            case 1:
                _monkeySpriteRender.sprite = _monkeySpriteUp;
                _monkeyAudioSource.Play();
                _monkeySpriteRender.flipY = false;
                break;

            case 2:
                _monkeySpriteRender.sprite = _monkeySpriteRight;
                _monkeyAudioSource.Play();
                _monkeySpriteRender.flipY = false;
                break;

            case 3:
                _monkeySpriteRender.sprite = _monkeySpriteDown;
                _monkeySpriteRender.flipY = true;
                _monkeyAudioSource.Play();
                break;

            case 4:
                _monkeySpriteRender.sprite = _monkeySpriteLeft;
                _monkeyAudioSource.Play();
                _monkeySpriteRender.flipY = false;
                break;

                //Not necessary in this case a "default"
        }
    }

    private void DiseableMonkeyForGameOver() => this.gameObject.SetActive(false);

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _monkeyAnimator = GetComponent<Animator>();
        _monkeySpriteRender = GetComponent<SpriteRenderer>();
        _monkeyAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Counting.OnStartDanceMonkey += CallDance;
        ButtonController.OnFinishGame += DiseableMonkeyForGameOver;
    }

    private void OnDisable()
    {
        ButtonController.OnFinishGame -= DiseableMonkeyForGameOver;
    }

    private void Start()
    {
        _allSequence = FindFirstObjectByType<ButtonController>();
    }

    #endregion
}
