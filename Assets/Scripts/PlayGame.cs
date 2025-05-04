using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    [SerializeField] private Button _playGameButton;

    private void GoTOGameScene() => SceneManager.LoadScene("Game Scene");

    private void Awake() => _playGameButton.onClick.AddListener(GoTOGameScene);

}
