using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public float health;
    public Vector2 position;
    
    public PlayerData(int gold, float health, Vector2 position)
    {
        this.gold = gold;
        this.health = health;
        this.position = position;
    }
}