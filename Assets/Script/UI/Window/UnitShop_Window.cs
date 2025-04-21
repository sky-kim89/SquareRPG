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
    private Text m_Attack = null;
    [SerializeField]
    private Text m_Health = null;

    [SerializeField]
    private Text m_UnitCount = null;

    [SerializeField]
    private Text m_AttackRange = null;

    [SerializeField]
    private Text m_DamageRate = null;
    [SerializeField]
    private Text m_DamageReduction = null;

    [SerializeField]
    private Text m_SkillDamageRate = null;
    [SerializeField]
    private Text m_AttackSpeed = null;
    [SerializeField]
    private Text m_MoveSpeed = null;
    [SerializeField]
    private Text m_GradeText = null;
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

        m_AP.text = data.AP.ToString("F0");
        m_HP.text = data.HP.ToString("F0");
        m_SP.text = data.SP.ToString("F0");
        m_LP.text = data.LP.ToString("F0");

        m_Attack.text = data.Attack.ToString("F0");
        m_Health.text = data.Health.ToString("F0");

        m_AttackRange.text = data.AttackRange.ToString("F1");
        m_UnitCount.text = data.UnitCount.ToString("F1");
        m_DamageRate.text = data.DamageRate.ToString("F1");
        m_DamageReduction.text = data.DamageReduction.ToString("F1");

        m_SkillDamageRate.text = data.SkillDamageRate.ToString("F1");
        m_AttackSpeed.text = data.AttackSpeed.ToString("F1");
        m_MoveSpeed.text = data.MoveSpeed.ToString("F1");
        m_GradeText.text = data.Grade.ToString();
        m_GradeText.color = Table.StatsColors[(int)data.Grade - 1];
        m_Grade.sprite = UIManager.Instance.GetSprite("Grade_" + (int)data.Grade);


        for (int i = 0; i < data.Skills.Length; i++)
        {
            if (data.Skills[i] != null)
            {
                m_Skills[i].sprite = UIManager.Instance.GetSprite(data.Skills[i].Data.Name);
                m_SkillTexts[i].text = string.Format(data.Skills[i].Data.Description, data.Skills[i].Data.Value * 100);
            }
        }

        m_StatsText[0].color = Table.StatsColors[(int)data.AP / 4];
        m_StatsText[1].color = Table.StatsColors[(int)data.SP / 4];
        m_StatsText[2].color = Table.StatsColors[(int)data.HP / 4];
        m_StatsText[3].color = Table.StatsColors[(int)data.LP / 4];

        m_UnitBuyText.text = data.GetRecruitCost().ToString();
        m_UnitBuy.gameObject.SetActive(m_IsShop);
        m_UnitSellText.text = (data.GetRecruitCost() * 0.3f).ToString();
        m_UnitSell.gameObject.SetActive(!m_IsShop);
    }

    public override void BackButtonClick()
    {
        Close();
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
