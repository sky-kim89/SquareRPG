using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo_Window : BackBaseWindow
{
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
    private Text m_NameText = null;
    [SerializeField]
    private Text m_Level = null;
    [SerializeField]
    private Text m_LevelUpGold = null;
    [SerializeField]
    private Text m_AddUnitGold = null;

    [SerializeField]
    private Text m_AP = null;
    [SerializeField]
    private Text m_HP = null;
    [SerializeField]
    private Text m_UnitCount = null;

    [SerializeField]
    private Text m_AttackSpeed = null;
    [SerializeField]
    private Text m_MoveSpeed = null;
    [SerializeField]
    private Text m_AttackRange = null;

    [SerializeField]
    private List<Image> m_Skills = new List<Image>();
    [SerializeField]
    private List<Text> m_SkillTexts = new List<Text>();

    private HeroUnit m_Unit = null;
    public static UnitInfo_Window Open(HeroUnit unit)
    {
        UnitInfo_Window win = WindowManager.Instance.Open<UnitInfo_Window>(WindowIds.UnitInfo_Window);
        win.OnInit(unit);
        return win;
    }

    public void OnInit(HeroUnit unit)
    {
        m_Unit = unit;
        m_Boby.color = m_Unit.UnitData.UnitColors[0];
        m_Head.color = m_Unit.UnitData.UnitColors[1];
        m_Hair.color = m_Unit.UnitData.UnitColors[2];
        m_EyeR.color = m_Unit.UnitData.UnitColors[3];
        m_EyeL.color = m_Unit.UnitData.UnitColors[4];

        m_NameText.text = m_Unit.UnitData.Name;
        m_Level.text = m_Unit.TotalLevel.ToString();

        m_Jop.sprite = InGameUI.Instance.GetSprite(m_Unit.UnitData.Weapon.ToString());

        m_LevelUpGold.text = (m_Unit.UnitData.Level * m_Unit.UnitData.Level).ToString();
        m_AddUnitGold.text = ((m_Unit.UnitData.AddUnitCount + 1) * 100).ToString();
        m_AP.text = m_Unit.AP.ToString();
        m_HP.text = m_Unit.MaxHP.ToString();
        m_UnitCount.text = (m_Unit.m_BuffUnitData.UnitCount + 1).ToString();

        m_AttackSpeed.text = m_Unit.m_BuffUnitData.AttackSpeed.ToString();
        m_MoveSpeed.text = m_Unit.m_BuffUnitData.MoveSpeed.ToString();
        m_AttackRange.text = m_Unit.m_BuffUnitData.AttackRange.ToString();

        for (int i = 0; i < m_Unit.SkillList.Count; i++)
        {
            m_Skills[i].sprite = InGameUI.Instance.GetSprite(m_Unit.SkillList[i].Data.Name);
            m_SkillTexts[i].text = string.Format(m_Unit.SkillList[i].Data.Description, m_Unit.SkillList[i].Data.Value * 100);
        }
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
        m_Unit.LevelUp(1);
        OnInit(m_Unit);
    }
    public void OnClickAddUnitButton()
    {
        m_Unit.AddUnit(1);
        OnInit(m_Unit);
    }

    public void OnclickSkillOpenButton(int index)
    {
        //패시브 스킬 오픈 하게 수정.
    }
}
