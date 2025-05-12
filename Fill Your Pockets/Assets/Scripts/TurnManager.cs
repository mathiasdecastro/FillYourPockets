using UnityEngine;

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

public class TurnManager : MonoBehaviour
{
    public int maxTurns = 3;
    public StageType stage;
    public bool isGameOver = false;

    private int currentTurn = 0;

    void Start()
    {
        stage = StageType.PlayerMoveFirst;
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
                stage = StageType.PlayerAttack;
                break;

            case StageType.PlayerMoveSecond:
                stage = StageType.EnemyMoveFirst;
                break;

            case StageType.PlayerAttack:
                stage = StageType.PlayerMoveSecond;
                break;

            case StageType.EnemyMoveFirst:
                stage = StageType.EnemyAttack;
                break;

            case StageType.EnemyMoveSecond:
                stage = StageType.TurretAttack;
                break;

            case StageType.EnemyAttack:
                stage = StageType.EnemyMoveSecond;
                break;

            case StageType.TurretAttack:
                stage = StageType.EndTurn;
                break;

            case StageType.EndTurn:
                if (!isGameOver)
                    stage = StageType.PlayerMoveFirst;
                break;

            default:
                break;
        }
    }

    public void EndTurn()
    {
        if (stage == StageType.EndTurn && !isGameOver)
        {
            Debug.Log("Tour : " + (currentTurn + 1) + " Terminé.");
            currentTurn++;
        }
        
        if (currentTurn >= maxTurns)
        {
            Debug.Log("La partie est finie");
            isGameOver = true;
        }

        NextStage();
    }
}
