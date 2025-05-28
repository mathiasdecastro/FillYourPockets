using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public static class Directions
{
    public static readonly Vector2[] Isometric =
    {
        new(1, 0.5f),
        new(-1, -0.5f),
        new(1, -0.5f),
        new(-1, 0.5f),
    };
}

public abstract class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected bool isPlayer;
    [SerializeField] protected Tilemap tilemap;
    [SerializeField] protected TurnManager turnManager;
    [SerializeField] protected List<Sprite> nonWalkableTiles = new();
    [SerializeField] protected Sprite endSprite;

    protected readonly List<Vector2> BlockedPositions = new();

    protected virtual void Start()
    {
        var fences = GameObject.FindGameObjectsWithTag("Fence");

        foreach (var fence in fences)
        {
            var fencePos = new Vector2(fence.transform.position.x, fence.transform.position.y - 1);
            BlockedPositions.Add(fencePos);
        }
    }

    protected static bool IsAdjacent(Vector2 current, Vector2 target)
    {
        return Directions.Isometric.Any(dir => target == current + dir);
    }

    protected bool IsWalkable(Vector2 pos)
    {
        var worldPos = new Vector3(pos.x, pos.y, 0);
        var cell = tilemap.WorldToCell(worldPos);
        var tile = tilemap.GetTile(cell);

        if (!tile)
            return false;

        if (tile is not Tile tileTyped)
            return true;
        
        var sprite = tileTyped.sprite;

        return !nonWalkableTiles.Contains(sprite);
    }

    protected void IsEnd(Vector2 pos)
    {
        var worldPos = new Vector3(pos.x, pos.y, 0);
        var cell = tilemap.WorldToCell(worldPos);
        var tile = tilemap.GetTile(cell);

        if (tile is not Tile tileTyped)
            return;
        
        var sprite = tileTyped.sprite;

        if (!isPlayer)
            return;

        if (sprite != endSprite)
            return;
        
        turnManager.hasWon = true;
        SceneManager.LoadScene("LevelTransition");
    }
}