using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;




public class AttackCardItem : CardItem, IPointerDownHandler
{

    //按下
    public void OnPointerDown(PointerEventData eventData)
    {
        //播放声音
        AudioManager.Instance.PlayEffect("Cards/draw");

        UIManager.Instance.ShowUI<LineUI>("LineUI");
        UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);
        //隐藏鼠标
        Cursor.visible = false;
        //关闭所有协同程序
        StopAllCoroutines();
        //启动鼠标操作协同程序
        StartCoroutine(OnMouseDownRight(eventData));

    }
    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //如果按下鼠标右键跳出循环
            if (Input.GetMouseButton(1)) break;

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                pData.position,
                pData.pressEventCamera,
                out pos
                ))
            {
                UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
                // 进行射线检测是否碰到怪物
                CheckRayToEnemy();
            }

            yield return null;
        }
        Cursor.visible = true;
        UIManager.Instance.CloseUI("LineUI");
    }
    Enemy hitEnemy;//射线检测到的敌人脚本
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();//选中

            //如果按下鼠标左键使用攻击卡
            if (Input.GetMouseButtonDown(0))
            {
                //关闭所有协同程序
                StopAllCoroutines();
                //鼠标显示
                Cursor.visible = true;

                UIManager.Instance.CloseUI("LineUI");
                if (TryUse() == true)
                {
                    //播放特效
                    PlayEffect(hitEnemy.transform.position);
                    //打击音效
                    AudioManager.Instance.PlayEffect("Effect/sword");
                    //敌人受伤
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                }
                //敌人未选中
                hitEnemy.OnUnSelect();
                //设置敌人脚本null
                hitEnemy = null;
            }
        }
        else
        {
            //未射到怪物
            if (hitEnemy != null)
            {
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            }

        }

    }
}