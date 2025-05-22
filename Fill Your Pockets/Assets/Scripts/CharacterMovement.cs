using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Directions
{
    public static readonly Vector2[] Isometric = new Vector2[]
    {
        new Vector2(1, 0.5f),
        new Vector2(-1, -0.5f),
        new Vector2(1, -0.5f),
        new Vector2(-1, 0.5f),
        new Vector2(2, 0),
        new Vector2(-2, 0),
        new Vector2(0, 1),
        new Vector2(0, -1)
    };
}

public abstract class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected bool isPlayer = false;
    [SerializeField] protected Tilemap tilemap;
    [SerializeField] protected TurnManager turnManager;
    [SerializeField] protected List<Sprite> nonWalkableTiles = new List<Sprite>();
    [SerializeField] protected Sprite endSprite;
        
    protected List<Vector2> blockedPositions = new List<Vector2>();

    protected virtual void Start()
    {
        GameObject[] fences = GameObject.FindGameObjectsWithTag("Fence");

        foreach (GameObject fence in fences)
        {
            Vector2 fencePos = new Vector2(fence.transform.position.x, fence.transform.position.y - 1);
            blockedPositions.Add(fencePos);
        }
    }

    protected bool IsAdjacent(Vector2 current, Vector2 target)
    {
        foreach (Vector2 dir in Directions.Isometric)
        {
            if (target == current + dir) return true;
        }

        return false;
    }

    protected bool IsWalkable(Vector2 pos)
    {
        Vector3 worldPos = new Vector3(pos.x, pos.y, 0);
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(cell);

        if (tile == null) return false;

        if (tile is Tile tileTyped)
        {
            Sprite sprite = tileTyped.sprite;
            
            if (nonWalkableTiles.Contains(sprite)) return false;
            
            if (isPlayer)
                if (sprite == endSprite) Debug.Log("End reached, thank's for playing");
        }

        return true;
    }
}