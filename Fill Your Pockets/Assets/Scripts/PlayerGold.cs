using UnityEngine;
using TMPro;

public class PlayerGold : MonoBehaviour
{
    public int gold = 0;
    public TextMeshPro goldText;

    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            Chest chest = other.GetComponent<Chest>();

            if (chest != null)
            {
                int collectedGold = chest.GetGold();
                gold += collectedGold;
                goldText.text = goldText.ToString();
                Debug.Log("Tu as récupéré : " + collectedGold);
            }
        }
    }
}