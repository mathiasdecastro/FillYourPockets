using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
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
                AddGold(collectedGold);
                Debug.Log("Tu as récupéré : " + collectedGold);
            }
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
        Debug.Log("Gold : " + gold);
    }
}