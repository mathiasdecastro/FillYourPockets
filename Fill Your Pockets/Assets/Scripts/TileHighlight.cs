using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlight : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Tilemap highlightTilemap;
    public TileBase highlightTile;
    public Transform player;
    public int range = 3;

    void Start()
    {
        ShowReachableTiles();
    }

    public void ShowReachableTiles()
    {
        highlightTilemap.ClearAllTiles();

        Vector3Int playerCell = walkableTilemap.WorldToCell(player.position);

        for (int x = -range; x <= range; x++)
            for (int y = -range; y <= range; y++)
            {
                Vector3Int offset = new Vector3Int(x, y, 0);
                Vector3Int cell = playerCell + offset;

                if (walkableTilemap.HasTile(cell) && IsInRange(playerCell, cell, range))
                    highlightTilemap.SetTile(cell, highlightTile);
            }
    }

    private bool IsInRange(Vector3Int from, Vector3Int to, int maxRange)
    {
        int dx = Mathf.Abs(to.x - from.x);
        int dy = Mathf.Abs(to.y - from.y);
       
        return dx + dy <= maxRange;
    }
}
