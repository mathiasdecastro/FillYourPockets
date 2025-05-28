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

    private float _timer = 0f;
    
    public int storedGold = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= intervalSeconds)
        {
            storedGold += goldPerInterval;
            _timer = 0f;
        }
    }

    public int GetGold()
    {
        Destroy(gameObject);

        return storedGold;
    }
}