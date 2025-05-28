using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private GameObject highlightPrefab;
    
    private static readonly int Walk = Animator.StringToHash("Walk");
    private readonly List<GameObject> _currentHighlights = new();
    
    private bool _hasMoved;
    private Animator _animator;
    private Camera _camera;
    
    public Vector2 targetPosition;
    
    protected override void Start()
    {
        base.Start();
        targetPosition = transform.position;
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (turnManager.stage != StageType.PlayerMoveFirst &&
            (turnManager.stage != StageType.PlayerMoveSecond || turnManager.isGameOver)) return;
        
        if ((Vector2)transform.position == targetPosition)
        {
            _animator.SetBool(Walk, false);

            if (!_hasMoved && _currentHighlights.Count == 0)
                ShowAvailableMoves();

            if (_hasMoved)
            {
                ClearHighlight();
                turnManager.EndTurn();
                _hasMoved = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                var nextPos = ConvertToGrid(mousePos);
                IsEnd(mousePos);

                if (IsAdjacent(targetPosition, nextPos) && IsWalkable(mousePos) && !BlockedPositions.Contains(nextPos))
                {
                    _animator.SetBool(Walk, true);
                    targetPosition = nextPos;
                    _hasMoved = true;
                }
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public static Vector2 ConvertToGrid(Vector2 input)
    {
        var x = Mathf.Round(input.x);
        var y = Mathf.Round(input.y);

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

            if (!IsWalkable(checkPos) || BlockedPositions.Contains(checkPos)) continue;
            
            var highlight = Instantiate(highlightPrefab, checkPos, Quaternion.identity);
            _currentHighlights.Add(highlight);
        }
    }

    private void ClearHighlight()
    {
        foreach (var obj in _currentHighlights) Destroy(obj);
        
        _currentHighlights.Clear();
    }
}