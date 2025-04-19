using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    [SerializeField]
    protected Image m_Boby = null;
    [SerializeField]
    protected Image m_Head = null;
    [SerializeField]
    protected Image m_Hair = null;
    [SerializeField]
    protected Image m_EyeL = null;
    [SerializeField]
    protected Image m_EyeR = null;

    [SerializeField]
    protected Image m_Jop = null;

    [SerializeField]
    protected Text m_Level = null;
    [SerializeField]
    protected Text m_UnitCount = null;
    [SerializeField]
    protected Image m_HP = null;
    [SerializeField]
    protected Image m_Cool = null;

    [SerializeField]
    protected Image m_Skill_1 = null;
    [SerializeField]
    protected Image m_Skill_2 = null;

    [SerializeField]
    protected Image m_DieCover = null;
    [SerializeField]
    protected Image m_Grade = null;

    protected HeroUnit m_Unit = null;

    public virtual void Init(HeroUnit unit)
    {
        if (unit != null)
        {
            gameObject.SetActive(true);
            m_Unit = unit;
            UIUpdate();
            m_Unit.SetStateCoolBack(eUnitStateType.Hit, UIUpdate);
            m_Unit.SetStateCoolBack(eUnitStateType.Dieing, UIUpdate);
            for(int i = 0; i < m_Unit.Units.Count; i++)
            {
                m_Unit.Units[i].SetStateCoolBack(eUnitStateType.Dieing, UIUpdate);
            }

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Init(UnitData unitData)
    {
        gameObject.SetActive(true);
        m_Boby.color = unitData.UnitColors[0];
        m_Head.color = unitData.UnitColors[1];
        m_Hair.color = unitData.UnitColors[2];
        m_EyeR.color = unitData.UnitColors[3];
        m_EyeL.color = unitData.UnitColors[4];

        m_Level.text = unitData.Level.ToString();
        m_HP.fillAmount = 1;
        //m_UnitCount.text = unitData.UnitCount.ToString();
        m_Grade.sprite = UIManager.Instance.GetSprite("Grade_" + (int)unitData.Grade);

        m_Jop.sprite = UIManager.Instance.GetSprite(unitData.Weapon.ToString());

        m_Skill_1.sprite = UIManager.Instance.GetSprite(unitData.Skills[0].Data.Name);
        m_Skill_2.sprite = UIManager.Instance.GetSprite(unitData.Skills[1].Data.Name);
    }

    public void UIUpdate()
    {
        m_DieCover.gameObject.SetActive(m_Unit.IsDie);
        m_Boby.color = m_Unit.UnitData.UnitColors[0];
        m_Head.color = m_Unit.UnitData.UnitColors[1];
        m_Hair.color = m_Unit.UnitData.UnitColors[2];
        m_EyeR.color = m_Unit.UnitData.UnitColors[3];
        m_EyeL.color = m_Unit.UnitData.UnitColors[4];

        m_Level.text = m_Unit.TotalLevel.ToString();
        m_HP.fillAmount = m_Unit.HP / m_Unit.MaxHP;
        m_UnitCount.text = m_Unit.LifeUnitCount.ToString();
        m_Grade.sprite = UIManager.Instance.GetSprite("Grade_" + (int)m_Unit.UnitData.Grade);

        m_Jop.sprite = UIManager.Instance.GetSprite(m_Unit.UnitData.Weapon.ToString());

        m_Skill_1.sprite = UIManager.Instance.GetSprite(m_Unit.SkillList[0].Data.Name);
        m_Skill_2.sprite = UIManager.Instance.GetSprite(m_Unit.SkillList[1].Data.Name);
    }

    public void LevelUp()
    {
        m_Unit.LevelUp(1); 
        UIUpdate();
    }

    public virtual void OnClickUnitInfoOpenButton()
    {
        UnitInfo_Window win = UnitInfo_Window.Open(m_Unit);
        win.CloseCall = () =>
        {
            UIUpdate();
        };
    }


    private void Update()
    {
        if (m_Unit != null && !m_Unit.IsDie && m_Unit.SkillList.Count > 0)
        {
            m_Cool.fillAmount = (m_Unit.SkillList[0] as ActiveSkill).CoolPercent;
        }
    }
}
