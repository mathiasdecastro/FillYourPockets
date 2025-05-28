using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject movementPanel; 
    public GameObject attackPanel;
    
    private CanvasGroup _movementGroup;
    private CanvasGroup _attackGroup;

    private enum Phase
    {
        Movement,
        Attack
    }

    private Phase _currentPhase;

    private void Start()
    {
        _currentPhase = Phase.Movement;
        _movementGroup = movementPanel.GetComponent<CanvasGroup>();
        _attackGroup = attackPanel.GetComponent<CanvasGroup>();
        
        UpdatePanel(); 
    }

    private void UpdatePanel()
    {
        switch (_currentPhase)
        {
            case Phase.Movement:
                movementPanel.SetActive(true); 
                attackPanel.SetActive(false);
                break;
            
            case Phase.Attack:
                movementPanel.SetActive(false);
                attackPanel.SetActive(true);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SwitchPhase()
    {
        _currentPhase = _currentPhase == Phase.Movement ? Phase.Attack : Phase.Movement;

        StartCoroutine(FadePanels());
    }

    private IEnumerator FadePanels()
    {
        const float fadeTime = 1f; 
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            var alphaValue = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            
            switch (_currentPhase)
            {
                case Phase.Movement:
                    _movementGroup.alpha = alphaValue;
                    _attackGroup.alpha = 1 - alphaValue;
                    break;
                
                case Phase.Attack:
                    _attackGroup.alpha = alphaValue;
                    _movementGroup.alpha = 1 - alphaValue;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (_currentPhase == Phase.Movement)
        {
            _movementGroup.alpha = 1;
            _attackGroup.alpha = 0;
        }
        else
        {
            _attackGroup.alpha = 1;
            _movementGroup.alpha = 0;
        }

        UpdatePanel();
    }
}