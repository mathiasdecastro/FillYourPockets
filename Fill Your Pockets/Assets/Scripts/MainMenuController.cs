using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject tutorialPopup; 
    public GameObject tutorial; 

    private void Start()
    {   
        tutorialPopup.SetActive(false); 
    }

    public void OnGameButtonClicked()
    {
        Debug.Log("oe ça marche");
        tutorialPopup.SetActive(true); 
    }

    public void OnSaveButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnSkipTutorial()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("oe ça marche");
    }


    public void StartGame(){
        Debug.Log("ça marche");
        SceneManager.LoadScene("GameScene");
    }
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
