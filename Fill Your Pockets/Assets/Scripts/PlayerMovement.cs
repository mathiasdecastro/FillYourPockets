using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public TurnManager tm;
    public Tilemap tilemap;
    public List<Sprite> nonWalkableTiles = new List<Sprite>();

    private bool hasMoved = false;
    private Animator animator;
    private Vector2 targetPosition;
    private Vector2[] directions = new Vector2[]
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

    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
{
    if (tm.stage == StageType.PlayerMoveFirst || tm.stage == StageType.PlayerMoveSecond && !tm.isGameOver)
    {
        if ((Vector2)transform.position == targetPosition)
        {
            animator.SetBool("Walk", false); 

            if (hasMoved)
            {
                tm.EndTurn();
                hasMoved = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 nextPos = ConvertToGrid(mousePos);

                if (IsAdjacent(targetPosition, nextPos) && IsWalkable(mousePos))
                {
                    animator.SetBool("Walk", true); 
                    targetPosition = nextPos;
                    hasMoved = true;
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}


    private Vector2 ConvertToGrid(Vector2 input)
    {
        float x = Mathf.Round(input.x);
        float y = Mathf.Round(input.y);

        if ((int)x % 2 == 0)
            y += 0.5f;
        else
            y += 1f;
 
        return new Vector2(x, y);
    }

    private bool IsAdjacent(Vector2 current, Vector2 target)
    {
        foreach (Vector2 dir in directions)
        {
            if (target == current + dir)
                return true;
        }

        return false;
    }

    bool IsWalkable(Vector2 pos)
    {
        Vector3 worldPos = new Vector3(pos.x, pos.y, 0);
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(cell);

        if (tile == null)
            return false;

        if (tile is Tile tileTyped)
        {
            Sprite sprite = tileTyped.sprite;
            
            if (nonWalkableTiles.Contains(sprite))
                return false;
        }

        return true;
    }
}