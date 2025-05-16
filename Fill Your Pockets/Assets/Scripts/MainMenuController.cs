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
        OnStartTutorial();
    }

    public void TestButtonClick()
    {
        Debug.Log("Le bouton fonctionne !");
    }


    public void OnSaveButtonClicked()
    {
        Debug.Log("oe ça marche");
        SceneManager.LoadScene("SaveScene");
    }

    public void OnStartTutorial()
    {
        tutorial.SetActive(true); 

        StartCoroutine(EndTutorialAndLoadGameScene());
    }

    public IEnumerator EndTutorialAndLoadGameScene()
    {
        yield return new WaitForSeconds(2f); 
        SceneManager.LoadScene("GameScene");
    }
}
