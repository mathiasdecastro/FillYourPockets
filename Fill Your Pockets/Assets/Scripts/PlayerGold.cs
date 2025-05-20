using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    private int gold = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            Chest chest = other.GetComponent<Chest>();
            
            if (chest != null)
            {
                int collectedGold = chest.GetGold();
                gold += collectedGold;
                Debug.Log("Tu as récupéré : " + collectedGold);
            }
        }
    }
}