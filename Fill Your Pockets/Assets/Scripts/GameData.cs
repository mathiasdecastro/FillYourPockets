using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public ChestData chestData;
    public TurnData turnData;
    public EnemyData enemyData;
    public TurretData turretData;
    public TrapData trapData;

    public GameData(PlayerData playerData, ChestData chestData, TurnData turnData, EnemyData enemyData, TurretData turretData, TrapData trapData)
    {
        this.playerData = playerData;
        this.chestData = chestData;
        this.turnData = turnData;
        this.enemyData = enemyData;
        this.turretData = turretData;
        this.trapData = trapData;
    }
}