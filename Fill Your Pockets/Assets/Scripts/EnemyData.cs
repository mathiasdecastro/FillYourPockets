using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float health;
    public Vector2 position;

    public EnemyData(float health, Vector2 position)
    {
        this.health = health;
        this.position = position;
    }
}