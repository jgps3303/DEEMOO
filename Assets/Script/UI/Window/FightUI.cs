using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

//战斗界面
public class FightUI : UIBase
{
    private Text cardCountTxt;//卡牌数量
    private Text noCardCountTxt;//弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text defTxt;//防御数值

    private List<CardItem> cardItemList;


    private void Awake()
    {
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        defTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    //更新血量显示
    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    //更新能量
    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }

    //防御更新
    public void UpdateDefense()
    {
        defTxt.text = FightManager.Instance.DefenseCount.ToString();
    }
    //更新卡堆数量
    public void UpdateCardCount()
    {

        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
    }
    //更新弃牌堆数量
    public void UpdateUsedCardCount()
    {

        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }


    public void CreateCardItem(int count)
    {
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }
    }
    public void UpdateCardItemPos()
    {
        float offset = 800f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2f * offset + offset * 0.5f, -500);
        for (int i = 0; i < cardItemList.Count; i++)
            {
                cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
                startPos.x = startPos.x + offset;
            }
    }

    public void RemoveCard(CardItem item)
    {
    AudioManager.Instance.PlayEffect("Cards/cardShove");//移除音效
    item.enabled = false;//禁用卡牌逻辑
    //添加到弃牌集合
    FightCardManager.Instance.usedCardList.Add(item.data["Id"]);
    //更新使用后的卡牌数量
    noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    //从集合中删除
    cardItemList.Remove(item);
    //刷新卡牌位置
    UpdateCardItemPos();
    //卡牌移到弃牌堆效果
    item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);
    item.transform.DOScale(0, 0.25f);
    Destroy(item.gameObject, 1);
    }  
    
 
//玩家回合结束，切换到敌人回合
private void onChangeTurnBtn()
{
    //只有玩家回合才能切换
    if (FightManager.Instance.fightUnit is Fight_PlayerTurn)
    {
        FightManager.Instance.ChangeType(FightType.Enemy);
    }
    
}

//删除所有卡牌
public void RemoveAllCards()
{
    for (int i = cardItemList.Count - 1; i >= 0; i--)
    {
        RemoveCard(cardItemList[i]);
    }
}
    

}
