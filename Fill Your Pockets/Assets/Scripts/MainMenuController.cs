using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject tutorialPopup;
    public GameObject tutorial;

    public Image fadeOverlay;
    public float fadeSpeed = 1.5f;


    private void Start() => tutorialPopup.SetActive(false);

    public void OnGameButtonClicked()
    {
        tutorialPopup.SetActive(true); 
        OnStartTutorial();
    }

    public void OnSaveButtonClicked() => SceneManager.LoadScene("SaveScene");

    public void OnStartTutorial()
    {
        tutorial.SetActive(true); 
        StartCoroutine(EndTutorialAndLoadGameScene());
    }

    public IEnumerator EndTutorialAndLoadGameScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameScene");
    }
}


