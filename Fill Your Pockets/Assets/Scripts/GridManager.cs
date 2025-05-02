using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject prefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float tileSize = 1f;

    void Start()
    {
        for (int x = 0; x < gridWidth; x++)
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 spawnPosition = new Vector3(x * tileSize, 0, y * tileSize);
                Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
            }
    }
}
