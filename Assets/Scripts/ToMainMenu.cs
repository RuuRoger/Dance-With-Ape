using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;

    private void GoToMainMenu() => SceneManager.LoadScene("Main Menu");

    private void Start() => _mainMenuButton.onClick.AddListener(GoToMainMenu);
}
