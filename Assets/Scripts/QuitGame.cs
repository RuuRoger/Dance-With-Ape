using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private Button _quitButton;

    private void ExitGame() => Application.Quit();

    private void Awake() => _quitButton.onClick.AddListener(ExitGame);
}
