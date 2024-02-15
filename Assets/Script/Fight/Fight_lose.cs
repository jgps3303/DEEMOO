using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_lose : FightUnit
{
    public override void Init()
    {
        Debug.Log("失败了");
        FightManager.Instance.StopAllCoroutines();
    
    }
    public override void OnUpdate()
    { 

    }
}
