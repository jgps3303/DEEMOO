using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoginUI : UIBase
{
    private void Awake()
    {
        Register("bg/startBtn").onClick = onStartGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {
        Close();


        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);
    }
}
