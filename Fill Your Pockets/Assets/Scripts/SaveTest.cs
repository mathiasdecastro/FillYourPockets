using UnityEngine;

public class SaveTest : MonoBehaviour
{
    private ChestData _chestData;
    private TurnData _turnData;
    private TurretData _turretData;
    private TrapData _trapData;
    private PlayerData _playerData;
    private EnemyData _enemyData;
    private GameData _gameData;
    
    private void Save()
    {
        var playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        var playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        var playerGold = GameObject.Find("Player").GetComponent<PlayerGold>();

        _playerData = !playerCombat
            ? new PlayerData(-1, -1f, new Vector2(-100, -100))
            : new PlayerData(playerGold.gold, playerCombat.health, playerMovement.transform.position);

        var enemyCombat = GameObject.Find("Enemy").GetComponent<EnemyCombat>();
        var enemyMovement = GameObject.Find("Enemy").GetComponent<EnemyMovement>();

        _enemyData = !enemyCombat
            ? new EnemyData(-1f, new Vector2(-100, -100))
            : new EnemyData(enemyCombat.health, enemyMovement.transform.position);
        
        var trap = GameObject.Find("Trap").GetComponent<TrapController>();
        _trapData = !trap ? new TrapData(new Vector2(-100, -100)) : new TrapData(trap.transform.position);
        
        var turret = GameObject.Find("Turret").GetComponent<TowerController>();
        _turretData = !turret ? new TurretData(new Vector2(-100, -100)) : new TurretData(turret.transform.position);
        
        var chest = GameObject.Find("Chest").GetComponent<Chest>();
        _chestData = !chest ? new ChestData(-1) : new ChestData(chest.storedGold);
        
        var turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        _turnData = !turnManager
            ? new TurnData(StageType.None, -1, -1, false)
            : new TurnData(turnManager.stage, turnManager.currentTurn, turnManager.maxTurns, turnManager.isGameOver);
        
        _gameData = new GameData(_playerData, _chestData, _turnData, _enemyData, _turretData, _trapData);
        SaveManager.Save(_gameData);
    }

    public void Load()
    {
        _gameData = SaveManager.Load();

        var playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        var playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        var playerGold = GameObject.Find("Player").GetComponent<PlayerGold>();
        playerCombat.health = _gameData.playerData.health;
        playerGold.gold = _gameData.playerData.gold;
        playerMovement.targetPosition = _gameData.playerData.position;
            
        var enemyCombat = GameObject.Find("Enemy").GetComponent<EnemyCombat>();
        var enemyMovement = GameObject.Find("Enemy").GetComponent<EnemyMovement>();
        enemyCombat.health = _gameData.enemyData.health;
        enemyMovement.transform.position = _gameData.enemyData.position;
            
        var chest = GameObject.Find("Chest").GetComponent<Chest>();
        chest.storedGold = _gameData.chestData.gold;

        var turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        turnManager.currentTurn = _gameData.turnData.currentTurn;
        turnManager.isGameOver = _gameData.turnData.isGameOver;
        turnManager.maxTurns = _gameData.turnData.maxTurns;
        turnManager.stage = _gameData.turnData.stage;
        
        var turret = GameObject.Find("Turret").GetComponent<TowerController>();
        turret.transform.position = _gameData.turretData.position;
        
        var trap = GameObject.Find("Trap").GetComponent<TrapController>();
        trap.transform.position = _gameData.trapData.position;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Save();

        if (Input.GetKeyDown(KeyCode.L))
            Load();
    }
}