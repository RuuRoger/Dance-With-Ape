using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _logo;

    private void Splash() => StartCoroutine(SplasAction());

    private IEnumerator SplasAction()
    {
        yield return new WaitForSeconds(2f);
        _logo.DOFade(0f, 2f);
        
        yield return new WaitForSeconds(2f);
        StartMainMenuScene();
    }

    private void StartMainMenuScene() => SceneManager.LoadScene("Main Menu");

    private void Start() => Splash();

}
