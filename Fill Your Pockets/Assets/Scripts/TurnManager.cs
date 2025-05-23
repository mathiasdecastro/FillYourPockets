using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageType
{
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

    public TurnData(TurnManager turnManager)
    {
        stage = turnManager.stage;
        currentTurn = turnManager.currentTurn;
        maxTurns = turnManager.maxTurns;
        isGameOver = turnManager.isGameOver;
    }
}

public class TurnManager : MonoBehaviour
{
    public int maxTurns = 3;
    public int currentTurn = 0;
    public StageType stage;
    public bool isGameOver = false;
    public bool hasWon = false;
    public Sprite playerMoveSprite;
    public Sprite playerAttackSprite;
    public Sprite enemyMoveSprite;
    public Sprite enemyAttackSprite;
    public Sprite turretAttackSprite;

    private SpriteRenderer stageImage;
    private TextMeshProUGUI textTurns;

    void Start()
    {
        stage = StageType.PlayerMoveFirst;
        stageImage = GameObject.Find("StageImage").GetComponent<SpriteRenderer>();
        stageImage.sprite = playerMoveSprite;
        textTurns = GameObject.Find("TextTurns").GetComponent<TextMeshProUGUI>();
        textTurns.text = (maxTurns - currentTurn).ToString();
    }

    void Update()
    {
        if (stage == StageType.EndTurn && !isGameOver)
            EndTurn();
    }

    void NextStage()
    {
        switch (stage)
        {
            case StageType.PlayerMoveFirst:
                stageImage.sprite = playerAttackSprite;
                stage = StageType.PlayerAttack;
                break;

            case StageType.PlayerMoveSecond:
                stageImage.sprite = enemyMoveSprite;
                stage = StageType.EnemyMoveFirst;
                break;

            case StageType.PlayerAttack:
                stageImage.sprite = playerMoveSprite;
                stage = StageType.PlayerMoveSecond;
                break;

            case StageType.EnemyMoveFirst:
                stageImage.sprite = enemyAttackSprite;
                stage = StageType.EnemyAttack;
                break;

            case StageType.EnemyMoveSecond:
                stageImage.sprite = turretAttackSprite;
                stage = StageType.TurretAttack;
                break;

            case StageType.EnemyAttack:
                stageImage.sprite = enemyMoveSprite;
                stage = StageType.EnemyMoveSecond;
                break;

            case StageType.TurretAttack:
                stage = StageType.EndTurn;
                break;

            case StageType.EndTurn:
                if (!isGameOver)
                {
                    stage = StageType.PlayerMoveFirst;
                    stageImage.sprite = playerMoveSprite;
                }
                break;

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
            Debug.Log("Tour : " + (currentTurn + 1) + " Terminé.");
            currentTurn++;
            textTurns.text = (maxTurns - currentTurn).ToString();
        }

        if (currentTurn >= maxTurns && !hasWon)
        {
            isGameOver = true;
            SceneManager.LoadScene("GameOverScene");
        }

        NextStage();
    }
}
