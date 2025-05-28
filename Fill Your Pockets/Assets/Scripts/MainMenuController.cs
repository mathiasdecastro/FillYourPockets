using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    private GameObject _tutorialPopup;
    private GameObject _tutorial;

    private void Start() => _tutorialPopup.SetActive(false);

    public void OnGameButtonClicked()
    {
        _tutorialPopup.SetActive(true); 
        OnStartTutorial();
    }

    public void OnSaveButtonClicked() => SceneManager.LoadScene("SaveScene");

    private void OnStartTutorial()
    {
        _tutorial.SetActive(true); 
        StartCoroutine(EndTutorialAndLoadGameScene());
    }

    private static IEnumerator EndTutorialAndLoadGameScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameScene");
    }
}


