using UnityEngine;
using TMPro;

public class PlayerGold : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldText;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Chest"))
            return;
        
        var chest = other.GetComponent<Chest>();

        if (!chest)
            return;
        
        var collectedGold = chest.GetGold();
        gold += collectedGold;
        goldText.text = gold.ToString();
        
        Debug.Log("You got : " + collectedGold + " golds");
    }
}