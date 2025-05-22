using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public void Update()
    {
        PlayerCombat playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        PlayerMovement playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PlayerGold playerGold = GameObject.Find("Player").GetComponent<PlayerGold>();

        EnemyCombat enemyCombat = GameObject.Find("Enemy").GetComponent<EnemyCombat>();
        EnemyMovement enemyMovement = GameObject.Find("Enemy").GetComponent<EnemyMovement>();

        TrapController trap = GameObject.Find("Trap").GetComponent<TrapController>();
        TowerController turret = GameObject.Find("Turret").GetComponent<TowerController>();
        Chest chest = GameObject.Find("Chest").GetComponent<Chest>();
        TurnManager turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();

        ChestData chestData = new ChestData(chest.storedGold);
        TurretData turretData = new TurretData((Vector2)turret.transform.position);
        TrapData trapData = new TrapData((Vector2)trap.transform.position);
        TurnData turnData = new TurnData(turnManager);
        
        EnemyData enemyData = new EnemyData(enemyCombat.health, (Vector2)enemyMovement.transform.position);
        PlayerData playerData = new PlayerData(playerGold.gold, playerCombat.health, (Vector2)playerMovement.transform.position);

        GameData gameData = new GameData(playerData, chestData, turnData, enemyData, turretData, trapData);
        
        if (Input.GetKeyDown(KeyCode.S))
            SaveManager.Save(gameData);

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameData newData = SaveManager.Load();

            playerCombat.health = newData.playerData.health;
            playerGold.gold = newData.playerData.gold;
            playerMovement.transform.position = (Vector2)newData.playerData.position;
            
            enemyCombat.health = newData.enemyData.health;
            enemyMovement.transform.position = (Vector2)newData.enemyData.position;
            
            chest.storedGold = newData.chestData.gold;

            turnManager.currentTurn = newData.turnData.currentTurn;
            turnManager.isGameOver = newData.turnData.isGameOver;
            turnManager.maxTurns = newData.turnData.maxTurns;
            turnManager.stage = newData.turnData.stage;
            
            turret.transform.position = (Vector2)newData.turretData.position;
            
            trap.transform.position = (Vector2)newData.trapData.position;
        }
    }
}