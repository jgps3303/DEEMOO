using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameConfigManager.Instance.Init();
        
        AudioManager.Instance.Init();

        RoleManager.Instance.Init();

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        AudioManager.Instance.PlayBgm("bgm1");

        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);
    }
}
