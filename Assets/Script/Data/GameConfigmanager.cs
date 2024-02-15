using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//遊戲配置表的管理氣
public class GameConfigManager 
{
   public static GameConfigManager Instance = new GameConfigManager();

   public GameConfigData cardData;//卡牌

   public GameConfigData enemyData;//敵人

   public GameConfigData levelData;//關卡

   public GameConfigData cardTypeData;

   private TextAsset textAsset;

   //初始化配置文件

   public void Init()
   {
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);

   }

   public List<Dictionary<string, string>> GetCardLines()
   {
    return cardData.GetLines();
   }

   public List<Dictionary<string, string>> GetEnemyLines()
   {
    return enemyData.GetLines();
   }

   public List<Dictionary<string, string>> GetLevelLines()
   {
    return levelData.GetLines();
   }

   public Dictionary<string, string> GetCardById(string id)
   {
        return cardData.GetOneById(id);
   }
   public Dictionary<string, string> GetEnemyById(string id)
   {
        return enemyData.GetOneById(id);
   }
   public Dictionary<string, string> GetLevelById(string id)
   {
        return levelData.GetOneById(id);
   }
   public Dictionary<string, string> GetCardTypeById(string id)
   {
        return levelData.GetOneById(id);
   }
}
