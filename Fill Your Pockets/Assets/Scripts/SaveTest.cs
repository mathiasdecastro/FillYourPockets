using System.Collections.Generic;
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

        List<EnemyData> enemyDatas = new List<EnemyData>() { enemyData };
        List<TurretData> turretDatas = new List<TurretData>() { turretData };
        List<TrapData> trapDatas = new List<TrapData>() { trapData };

        GameData gameData = new GameData(playerData, chestData, turnData, enemyDatas, turretDatas, trapDatas);

        if (Input.GetKeyDown(KeyCode.S))
            SaveManager.Save(gameData);
    }
}