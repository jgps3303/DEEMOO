using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.Init();
        
        AudioManager.Instance.PlayBgm("battle");

        EnemyManeger.Instance.LoadRes("10003");

        FightCardManager.Instance.Init();
        //显示战斗界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }
    public override void OnUpdate()
    { 
        base.OnUpdate();
    }
}
