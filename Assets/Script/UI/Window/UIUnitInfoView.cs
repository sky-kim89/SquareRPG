using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitInfoView : MonoBehaviour
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
    private Image m_Grade = null;
    [SerializeField]
    private Text m_GradeText = null;

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
    private Text m_AttackRange = null;

    [SerializeField]
    private Text m_SkillOpenGold1 = null;
    [SerializeField]
    private Text m_SkillOpenGold2 = null;

    [SerializeField]
    private List<Image> m_Skills = new List<Image>();
    [SerializeField]
    private List<Text> m_SkillTexts = new List<Text>();
    [SerializeField]
    private List<GameObject> m_SkillObjs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_SkillCovers = new List<GameObject>();

    private HeroUnit m_Unit = null;

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
        m_DamageRate.text = m_Unit.m_BuffUnitData.DamageRate * 100 + "%";
        m_DamageReduction.text = m_Unit.m_BuffUnitData.DamageReduction * 100 + "%";

        m_Jop.sprite = UIManager.Instance.GetSprite(m_Unit.m_BuffUnitData.Weapon.ToString());

        m_LevelUpGold.text = m_Unit.UnitData.GetHeroLevelUpCost().ToString();
        m_AddUnitGold.text = m_Unit.UnitData.AddUnitCount < 10 ? m_Unit.UnitData.GetAddUnitCost().ToString() : "¿Ï·á";
        m_AP.text = m_Unit.AP.ToString();
        m_HP.text = m_Unit.MaxHP.ToString();
        m_UnitCount.text = m_Unit.m_BuffUnitData.UnitCount.ToString();

        m_Grade.sprite = UIManager.Instance.GetSprite("Grade_" + (int)m_Unit.m_BuffUnitData.Grade);

        m_AttackSpeed.text = m_Unit.m_BuffUnitData.AttackSpeed.ToString();
        m_MoveSpeed.text = m_Unit.m_BuffUnitData.MoveSpeed.ToString();
        m_AttackRange.text = m_Unit.m_BuffUnitData.AttackRange.ToString();

        m_SkillOpenGold1.text = m_Unit.UnitData.GetSkillOpenCost().ToString();
        m_SkillOpenGold2.text = m_Unit.UnitData.GetSkillOpenCost().ToString();

        for (int i = 0; i < m_Unit.SkillList.Count; i++)
        {
            if (m_Unit.SkillList[i] != null)
            {
                m_Skills[i].sprite = UIManager.Instance.GetSprite(m_Unit.SkillList[i].Data.Name);
                m_SkillTexts[i].text = string.Format(m_Unit.SkillList[i].Data.Description, m_Unit.SkillList[i].Data.Value * 100);
            }
        }

        m_SkillObjs[0].SetActive(m_Unit.OpneSkill > 1);
        m_SkillObjs[1].SetActive(m_Unit.OpneSkill > 2);
        m_SkillCovers[0].SetActive(m_Unit.OpneSkill == 1);
        m_SkillCovers[1].SetActive(m_Unit.OpneSkill == 2);
    }
}
