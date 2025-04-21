using System;
using System.Collections;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    #region Fields
    [Header("Sprites")]
    [SerializeField] private Sprite _monkeySpriteUp;
    [SerializeField] private Sprite _monkeySpriteRight;
    [SerializeField] private Sprite _monkeySpriteDown;
    [SerializeField] private Sprite _monkeySpriteLeft;

    [SerializeField] private GameObject _scriptPressButton;

    private Animator _monkeyAnimator;
    private SpriteRenderer _monkeySpriteRenderer;
    private ButtonController _buttonController;
    private AudioSource _monkeyAudioSource;

    #endregion

    #region Events
    public static event Action OnEnableButtons; // Goes to ButtonPress
    public static event Action OnDisableButtons; // Goes to ButtonPress

    #endregion

    #region Methods

    //Active spriterender monkey and start coroutine
    private void StartDance()
    {
        _monkeySpriteRenderer.enabled = true;
        StartCoroutine(PlaySequence());
    }

    /*
        Diseable buttons to dance
        Call a method to change the sprite related with the button id
        Call a method to "idle" mpnkey state. On th eanimator and quit the flip
        Notify to enable buttons
    */
    private IEnumerator PlaySequence()
    {
        OnDisableButtons?.Invoke();

        foreach (int value in _buttonController.ListSequence)
        {
            ChangeSprite(value);
            yield return new WaitForSeconds(1f);
        }

        ResetMonkey();
        OnEnableButtons?.Invoke();
    }

    // Changes the monkey's sprite based on the sequence value
    private void ChangeSprite(int value)
    {
        switch (value)
        {
            case 1:
                _monkeySpriteRenderer.sprite = _monkeySpriteUp;
                _monkeySpriteRenderer.flipY = false;
                break;

            case 2:
                _monkeySpriteRenderer.sprite = _monkeySpriteRight;
                _monkeySpriteRenderer.flipY = false;
                break;

            case 3:
                _monkeySpriteRenderer.sprite = _monkeySpriteDown;
                _monkeySpriteRenderer.flipY = true;
                break;

            case 4:
                _monkeySpriteRenderer.sprite = _monkeySpriteLeft;
                _monkeySpriteRenderer.flipY = false;
                break;

                //A "Default" is not necessary in this case
        }

        _monkeyAudioSource.Play();
    }

    /*
        Quit Flip
        start animation to wait player press buttons
    */
    private void ResetMonkey()
    {
        _monkeySpriteRenderer.flipY = false;
        _monkeyAnimator.enabled = true;
    }

    // Call this when the player is supposed to play
    private void EnableIdleAnimation()
    {
        _monkeyAnimator.enabled = true;
        _monkeySpriteRenderer.enabled = true;
    }

    //Game over
    private void DisableMonkeyForGameOver() => gameObject.SetActive(false);

    /*
        When finish round:
        diseable animator and spriterender
        call to diseable buttons
    */
    private void StopDanceForRound()
    {
        _monkeyAnimator.enabled = false;
        _monkeySpriteRenderer.enabled = false;
        OnDisableButtons?.Invoke();
    }

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _monkeyAnimator = GetComponent<Animator>();
        _monkeySpriteRenderer = GetComponent<SpriteRenderer>();
        _monkeyAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Counting.OnStartDanceMonkey += StartDance;
        ButtonController.OnFinishGame += DisableMonkeyForGameOver;
        ButtonController.OnStopDanceMonkey += StopDanceForRound;
        Monkey.OnEnableButtons += EnableIdleAnimation; // Restore idle animation during wait
    }

    private void OnDisable()
    {
        Counting.OnStartDanceMonkey -= StartDance;
        ButtonController.OnFinishGame -= DisableMonkeyForGameOver;
        ButtonController.OnStopDanceMonkey -= StopDanceForRound;
        Monkey.OnEnableButtons -= EnableIdleAnimation; // Unsubscribe idle animation event
    }

    private void Start()
    {
        _buttonController = FindAnyObjectByType<ButtonController>();
    }

    #endregion
}
