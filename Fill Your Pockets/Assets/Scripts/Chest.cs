using UnityEngine;

[System.Serializable]
public class ChestData
{
    public int gold;

    public ChestData(int gold) => this.gold = gold;
}

public class Chest : MonoBehaviour
{
    [SerializeField] private int goldPerInterval = 5;
    [SerializeField] private float intervalSeconds = 3f;

    private float timer = 0f;
    public int storedGold = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervalSeconds)
        {
            storedGold += goldPerInterval;
            timer = 0f;
        }
    }

    public int GetGold()
    {
        Destroy(gameObject);

        return storedGold;
    }
}