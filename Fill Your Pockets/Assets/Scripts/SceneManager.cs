using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadMenuScene() => SceneManager.LoadScene("MenuScene");

    public void LoadGameScene() => SceneManager.LoadScene("GameScene");

    public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void QuitGame() => Application.Quit();
}
