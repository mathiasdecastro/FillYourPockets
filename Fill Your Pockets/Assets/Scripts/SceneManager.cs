using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMenuScene()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadGameScene()
    {
        Debug.Log("Qdfeqgrgdfgbergdfgf");
        SceneManager.LoadScene("GameScene");
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
