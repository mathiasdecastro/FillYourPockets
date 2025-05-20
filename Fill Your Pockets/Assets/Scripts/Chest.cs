using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private int goldPerInterval = 5;
    [SerializeField] private float intervalSeconds = 3f;

    private float timer = 0f;
    private int storedGold = 0;

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
        int gold = storedGold;
        Destroy(gameObject);

        return gold;
    }
}