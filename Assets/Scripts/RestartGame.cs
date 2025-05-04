using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private Button _replayGameButton;

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        _replayGameButton = GetComponent<Button>();
    }

    private void Start()
    {
        _replayGameButton.onClick.AddListener(Restart);
    }


}
