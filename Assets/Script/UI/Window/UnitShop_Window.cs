using MyProjeckt;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitShop_Window : BackBaseWindow
{
    [SerializeField]
    private UnitData m_UnitData = null;
    [SerializeField]
    private Image m_Boby = null;
    [SerializeField]
    private Image m_Head = null;
    [SerializeField]
    private Image m_Hair = null;
    [SerializeField]
    private Image m_EyeL = null;
    [SerializeField]
    private Image m_EyeR = null;

    [SerializeField]
    private Image m_Jop = null;
    [SerializeField]
    private Image m_Grade = null;

    [SerializeField]
    private Text m_NameText = null;

    [SerializeField]
    private Text m_AP = null;
    [SerializeField]
    private Text m_HP = null;
    [SerializeField]
    private Text m_SP = null;
    [SerializeField]
    private Text m_LP = null;
    [SerializeField]
    private Text m_UnitCount = null;

    [SerializeField]
    private Text m_AttackRange = null;

    [SerializeField]
    private Text m_DamageRate = null;
    [SerializeField]
    private Text m_DamageReduction = null;

    [SerializeField]
    private List<Image> m_Skills = new List<Image>();
    [SerializeField]
    private List<Text> m_SkillTexts = new List<Text>();

    [SerializeField]
    private List<Text> m_StatsText = null;

    [SerializeField]
    protected List<UnitView> m_MyUnitList = new List<UnitView>();

    [SerializeField]
    protected List<ShopUnitView> m_ShopUnitList = new List<ShopUnitView>();

    [SerializeField]
    private Text m_UnitBuyText = null;
    [SerializeField]
    private GameObject m_UnitBuy = null;
    [SerializeField]
    private Text m_UnitSellText = null;
    [SerializeField]
    private GameObject m_UnitSell = null;
    private List<UnitData> m_ShopUnitDatas = null;
    public override void OnInit()
    {
        m_ShopUnitDatas = GetShopUnitData();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < m_MyUnitList.Count; i++)
        {
            if (UnitManager.Instance.MyUnitData.Count > i)
                m_MyUnitList[i].Init(UnitManager.Instance.MyUnitData[i]);
            else
                m_MyUnitList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < m_ShopUnitList.Count; i++)
        {
            if (m_ShopUnitDatas.Count > i)
                m_ShopUnitList[i].Init(m_ShopUnitDatas[i]);
            else
                m_ShopUnitList[i].Init(null);
        }
    }

    public void ViewUnitData(UnitData data, bool m_IsShop)
    {
        m_UnitData = data;

        m_Boby.color = data.UnitColors[0];
        m_Head.color = data.UnitColors[1];
        m_Hair.color = data.UnitColors[2];
        m_EyeR.color = data.UnitColors[3];
        m_EyeL.color = data.UnitColors[4];

        m_NameText.text = data.Name;

        m_Jop.sprite = UIManager.Instance.GetSprite(data.Weapon.ToString());

        m_AP.text = data.AP.ToString();
        m_HP.text = data.HP.ToString();
        m_SP.text = data.SP.ToString();
        m_LP.text = data.LP.ToString();
        m_AttackRange.text = data.AttackRange.ToString();
        m_UnitCount.text = data.UnitCount.ToString();

        m_Grade.sprite = UIManager.Instance.GetSprite("Grade_" + (int)data.Grade);


        for (int i = 0; i < data.Skills.Length; i++)
        {
            if (data.Skills[i] != null)
            {
                m_Skills[i].sprite = UIManager.Instance.GetSprite(data.Skills[i].Data.Name);
                m_SkillTexts[i].text = string.Format(data.Skills[i].Data.Description, data.Skills[i].Data.Value * 100);
            }
        }

        for(int i = 0; i < m_StatsText.Count; i++)
        {
            m_StatsText[i].color = Table.StatsColors[(int)data.Grade - 1];
        }

        m_UnitBuyText.text = data.GetRecruitCost().ToString();
        m_UnitBuy.gameObject.SetActive(m_IsShop);
        m_UnitSellText.text = (data.GetRecruitCost() * 0.3f).ToString();
        m_UnitSell.gameObject.SetActive(!m_IsShop);
    }

    public override void BackButtonClick()
    {
        Close();
        WindowManager.Instance.Open<CardSelect_Window>(WindowIds.CardSelect_Window);
    }

    private List<UnitData> GetShopUnitData()
    {
        List<UnitData> unitDatas = new List<UnitData>();
        unitDatas.Add(UnitRandomMachine.NewUnitData());
        unitDatas.Add(UnitRandomMachine.NewUnitData());
        unitDatas.Add(UnitRandomMachine.NewUnitData());
        unitDatas.Add(UnitRandomMachine.NewUnitData());
        unitDatas.Add(UnitRandomMachine.NewUnitData());
        unitDatas.Add(UnitRandomMachine.NewUnitData());
        return unitDatas;
    }

    public void OnClickMyUnit(int index)
    {
        ViewUnitData(UnitManager.Instance.MyUnitData[index], false);
    }

    public void OnClickBuyUnit()
    {
        if (EconomyManager.Instance.UseGold(m_UnitData.GetRecruitCost()) && UnitManager.Instance.MyUnitData.Count < 5)
        {
            UnitManager.Instance.MyUnitData.Add(m_UnitData);
            m_ShopUnitDatas.Remove(m_UnitData);
            UpdateUI();
        }
    }

    public void OnClickSellUnit()
    {
        if(UnitManager.Instance.MyUnitData.Count > 1)
        {
            EconomyManager.Instance.AddGold((int)(m_UnitData.GetRecruitCost() * 0.3f));
            UnitManager.Instance.MyUnitData.Remove(m_UnitData);
            UpdateUI();
        }
    }
}
