using UnityEngine;

public class Chest : MonoBehaviour
{
    public int goldPerInterval = 5;
    public float intervalSeconds = 3f;

    private float timer = 0f;
    private int storeGold = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervalSeconds)
        {
            storeGold += goldPerInterval;
            timer = 0f;
            Debug.Log("Gold : " + storeGold);
        }
    }

    public int GetGold()
    {
        int goldToGive = storeGold;
        Destroy(gameObject);

       return goldToGive;
    }

    public int getStoreGold()
    {
        return storeGold;
    }
}