using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public ChestData chestData;
    public TurnData turnData;
    public List<EnemyData> enemyDatas;
    public List<TurretData> turretDatas;
    public List<TrapData> trapDatas;

    public GameData(PlayerData playerData, ChestData chestData, TurnData turnData, List<EnemyData> enemyDatas, List<TurretData> turretDatas, List<TrapData> trapDatas)
    {
        this.playerData = playerData;
        this.chestData = chestData;
        this.turnData = turnData;
        this.enemyDatas = enemyDatas;
        this.turretDatas = turretDatas;
        this.trapDatas = trapDatas;
    }
}