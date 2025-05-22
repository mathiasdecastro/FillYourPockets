using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject movementPanel; 
    public GameObject attackPanel;
    private CanvasGroup movementGroup;
    private CanvasGroup attackGroup;

    private enum Phase
    {
        Movement,
        Attack
    }

    private Phase currentPhase;

    void Start()
    {
        currentPhase = Phase.Movement;
        movementGroup = movementPanel.GetComponent<CanvasGroup>();
        attackGroup = attackPanel.GetComponent<CanvasGroup>();
        
        UpdatePanel(); 
    }

    private void UpdatePanel()
    {
        if (currentPhase == Phase.Movement)
        {
            movementPanel.SetActive(true); 
            attackPanel.SetActive(false);
        }
        else if (currentPhase == Phase.Attack)
        {
            movementPanel.SetActive(false);
            attackPanel.SetActive(true);
        }
    }

    public void SwitchPhase()
    {
        if (currentPhase == Phase.Movement)
        {
            currentPhase = Phase.Attack;
        }
        else
        {
            currentPhase = Phase.Movement;
        }

        StartCoroutine(FadePanels());
    }

    private IEnumerator FadePanels()
    {
        float fadeTime = 1f; 
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alphaValue = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            if (currentPhase == Phase.Movement)
            {
                movementGroup.alpha = alphaValue;
                attackGroup.alpha = 1 - alphaValue; 
            }
            else if (currentPhase == Phase.Attack)
            {
                attackGroup.alpha = alphaValue;
                movementGroup.alpha = 1 - alphaValue; 
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (currentPhase == Phase.Movement)
        {
            movementGroup.alpha = 1;
            attackGroup.alpha = 0;
        }
        else
        {
            attackGroup.alpha = 1;
            movementGroup.alpha = 0;
        }

        UpdatePanel();
    }
}