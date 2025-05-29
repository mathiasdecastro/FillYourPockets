using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

    public GameObject tutorial;

    private void Start() => tutorial.SetActive(false);

    public void OnGameButtonClicked()
    {
        tutorial.SetActive(true); 
        OnStartTutorial();
    }

    public void OnSaveButtonClicked() => SceneManager.LoadScene("SaveScene");

    private void OnStartTutorial()
    {
        tutorial.SetActive(true); 
        StartCoroutine(EndTutorialAndLoadGameScene());
    }

    private static IEnumerator EndTutorialAndLoadGameScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameScene");
    }
}


