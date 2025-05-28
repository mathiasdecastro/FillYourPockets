using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageType
{
    None,
    PlayerAttack,
    PlayerMoveFirst,
    PlayerMoveSecond,
    EnemyAttack,
    EnemyMoveFirst,
    EnemyMoveSecond,
    TurretAttack,
    EndTurn
}

[System.Serializable]
public class TurnData
{
    public StageType stage;
    public int currentTurn;
    public int maxTurns;
    public bool isGameOver;

    public TurnData(StageType stageType, int currentTurnGame, int maxTurnsGame, bool isCurrentGameOver)
    {
        stage = stageType;
        currentTurn = currentTurnGame;
        maxTurns = maxTurnsGame;
        isGameOver = isCurrentGameOver;
    }
}

public class TurnManager : MonoBehaviour
{
    public int maxTurns = 3;
    public int currentTurn;
    public StageType stage;
    public bool isGameOver;
    public bool hasWon;
    
    [SerializeField] private Sprite playerMoveSprite;
    [SerializeField] private Sprite playerAttackSprite;
    [SerializeField] private Sprite enemyMoveSprite;
    [SerializeField] private Sprite enemyAttackSprite;
    [SerializeField] private Sprite turretAttackSprite;

    private SpriteRenderer _stageImage;
    private TextMeshProUGUI _textTurns;

    private void Start()
    {
        stage = StageType.PlayerMoveFirst;
        _stageImage = GameObject.Find("StageImage").GetComponent<SpriteRenderer>();
        _stageImage.sprite = playerMoveSprite;
        _textTurns = GameObject.Find("TextTurns").GetComponent<TextMeshProUGUI>();
        _textTurns.text = (maxTurns - currentTurn).ToString();
    }

    private void Update()
    {
        if (stage == StageType.EndTurn && !isGameOver)
            EndTurn();
    }

    private void NextStage()
    {
        switch (stage)
        {
            case StageType.PlayerMoveFirst:
                _stageImage.sprite = playerAttackSprite;
                stage = StageType.PlayerAttack;
                break;

            case StageType.PlayerMoveSecond:
                _stageImage.sprite = enemyMoveSprite;
                stage = StageType.EnemyMoveFirst;
                break;

            case StageType.PlayerAttack:
                _stageImage.sprite = playerMoveSprite;
                stage = StageType.PlayerMoveSecond;
                break;

            case StageType.EnemyMoveFirst:
                _stageImage.sprite = enemyAttackSprite;
                stage = StageType.EnemyAttack;
                break;

            case StageType.EnemyMoveSecond:
                _stageImage.sprite = turretAttackSprite;
                stage = StageType.TurretAttack;
                break;

            case StageType.EnemyAttack:
                _stageImage.sprite = enemyMoveSprite;
                stage = StageType.EnemyMoveSecond;
                break;

            case StageType.TurretAttack:
                stage = StageType.EndTurn;
                break;

            case StageType.EndTurn:
                if (!isGameOver)
                {
                    stage = StageType.PlayerMoveFirst;
                    _stageImage.sprite = playerMoveSprite;
                }
                break;

            case StageType.None:
            default:
                break;
        }
    }
    

    public void EndTurn()
    {
        if (hasWon)
        {
            isGameOver = true;
            SceneManager.LoadScene("LevelTransition");
        }
        
        if (stage == StageType.EndTurn && !isGameOver)
        {
            Debug.Log("Turn : " + (currentTurn + 1) + " ended.");
            currentTurn++;
            _textTurns.text = (maxTurns - currentTurn).ToString();
        }

        if (currentTurn >= maxTurns && !hasWon)
        {
            isGameOver = true;
            SceneManager.LoadScene("GameOverScene");
        }

        NextStage();
    }
}
