using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private GameObject highlightPrefab;
    
    private bool hasMoved = false;
    private Animator animator;
    private Vector2 targetPosition;
    private List<GameObject> _currentHighlights = new List<GameObject>();
    
    protected override void Start()
    {
        base.Start();
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (turnManager.stage == StageType.PlayerMoveFirst || turnManager.stage == StageType.PlayerMoveSecond && !turnManager.isGameOver)
        {
            if ((Vector2)transform.position == targetPosition)
            {
                animator.SetBool("Walk", false);

                if (!hasMoved && _currentHighlights.Count == 0)
                    ShowAvailableMoves();

                if (hasMoved)
                {
                    ClearHighlight();
                    turnManager.EndTurn();
                    hasMoved = false;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 nextPos = ConvertToGrid(mousePos);
                    IsEnd(mousePos);

                    if (IsAdjacent(targetPosition, nextPos) && IsWalkable(mousePos) && !blockedPositions.Contains(nextPos))
                    {
                        animator.SetBool("Walk", true);
                        targetPosition = nextPos;
                        hasMoved = true;
                    }
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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

    private void ShowAvailableMoves()
    {
        ClearHighlight();

        var pos = new Vector2(targetPosition.x, targetPosition.y - 1);
        
        foreach (var dir in Directions.Isometric)
        {
            var checkPos = pos + dir;
            
            if (IsWalkable(checkPos) && !blockedPositions.Contains(checkPos))
            {
                GameObject highlight = Instantiate(highlightPrefab, checkPos, Quaternion.identity);
                _currentHighlights.Add(highlight);
            }
        }
    }

    private void ClearHighlight()
    {
        foreach (var obj in _currentHighlights)
            Destroy(obj);
        
        _currentHighlights.Clear();
    }
}