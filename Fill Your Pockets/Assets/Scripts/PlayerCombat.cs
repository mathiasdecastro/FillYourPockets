using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI healthText;

     void Start()
    {
        UpdateHealthText();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Health : " + health);
        UpdateHealthText();
        
        if (health <= 0)
            Destroy(gameObject);
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }
}