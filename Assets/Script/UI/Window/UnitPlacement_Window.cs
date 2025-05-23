using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPlacement_Window : BackBaseWindow, IInfiniteScrollSetup
{
    private UIUnitInfoView m_UIUnitInfoView = null;

    private HeroUnit m_Unit = null;

    private void Start()
    {
        m_InfiniteScroll.Init();
        foreach (RectTransform tamp in m_InfiniteScroll.itemList)
            m_UIItemLines.Add(tamp.GetComponent<UIItemLine>()); // 여기 line으로 바꿔주기
    }

    public void OnInit(HeroUnit unit)
    {
        m_UIUnitInfoView.OnInit(unit);
        m_Unit = unit;

        m_CardDatas.Clear();
        m_CardDatas.AddRange(GameManager.Instance.Cards);
        ItemAllUpdate();
        m_ScrollBar.value = 1;
    }

    public override void OnInit()
    {
    }

    public override void BackButtonClick()
    {
        Close();
    }

    public void OnClickLevelUpButton()
    {
        if (EconomyManager.Instance.UseGold(m_Unit.UnitData.GetHeroLevelUpCost()))
        {
            m_Unit.LevelUp(1);
            OnInit(m_Unit);
        }
    }
    public void OnClickAddUnitButton()
    {
        if (EconomyManager.Instance.UseGold(m_Unit.UnitData.GetAddUnitCost()))
        {
            if (m_Unit.UnitData.AddUnitCount < 10)
            {
                m_Unit.AddUnit(1);
                OnInit(m_Unit);
            }
            else
            {

            }
        }
    }

    public void OnclickSkillOpenButton(int index)
    {
        //패시브 스킬 오픈 하게 수정.
        if (EconomyManager.Instance.UseGold(m_Unit.UnitData.GetSkillOpenCost()))
        {
            m_Unit.OpneSkill++;
            OnInit(m_Unit);
        }
    }

    [SerializeField]
    private InfiniteScroll m_InfiniteScroll = null;
    [SerializeField]
    private RectTransform m_ScrollRect = null;
    [SerializeField]
    private Scrollbar m_ScrollBar = null;

    private List<UIData> m_CardDatas = new List<UIData>();
    private List<UIItemLine> m_UIItemLines = new List<UIItemLine>();
    
    private int PostionToIndex(float posY)
    {
        int realIndex = Mathf.RoundToInt(posY / m_InfiniteScroll.itemScale);
        realIndex = Mathf.Abs(realIndex);

        return realIndex;
    }
    public void OnUpdateItem(int itemIndex, GameObject obj)
    {
        UIItemLine itemLine = obj.GetComponent<UIItemLine>();

        if (m_CardDatas.Count > itemIndex * itemLine.Count && itemIndex >= 0)
        {
            int range = m_CardDatas.Count - itemIndex * itemLine.Count;

            if (range < itemLine.Count)
            {
                itemLine.Init(m_CardDatas.GetRange(itemIndex * itemLine.Count, range));
            }
            else
            {
                itemLine.Init(m_CardDatas.GetRange(itemIndex * itemLine.Count, itemLine.Count));
            }
        }
        else
        {
            itemLine.Init(null);
        }
    }

    public void ItemAllUpdate()
    {
        m_CardDatas.Clear();
        m_CardDatas.AddRange(GameManager.Instance.Cards);

        for (int i = 0; i < m_UIItemLines.Count; i++)
        {
            if (m_UIItemLines.Count > i)
            {
                int realIndex = PostionToIndex(m_UIItemLines[i].transform.localPosition.y);
                OnUpdateItem(realIndex, m_UIItemLines[i].gameObject);
            }
        }

        OnPostSetupItems();
    }

    public void OnPostSetupItems()
    {
        var rectTransform = m_InfiniteScroll.GetComponent<RectTransform>();
        var delta = rectTransform.sizeDelta;
        if ((m_InfiniteScroll.itemScale * m_CardDatas.Count / 3) < m_ScrollRect.rect.height)
            delta.y = 0;
        else
            delta.y = (m_InfiniteScroll.itemScale * m_CardDatas.Count / 3) - m_ScrollRect.rect.height + m_InfiniteScroll.itemScale;
        rectTransform.sizeDelta = delta;
    }

    public void OnClickCard(CardData data)
    {

    }
}
