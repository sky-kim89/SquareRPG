using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
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
    private Text m_Level = null;
    [SerializeField]
    private Text m_UnitCount = null;
    [SerializeField]
    private Image m_HP = null;
    [SerializeField]
    private Image m_Cool = null;

    [SerializeField]
    private Image m_Skill_1 = null;
    [SerializeField]
    private Image m_Skill_2 = null;

    [SerializeField]
    private Image m_DieCover = null;

    private HeroUnit m_Unit = null;

    public void Init(HeroUnit unit)
    {
        if (unit != null)
        {
            gameObject.SetActive(true);
            m_Unit = unit;
            UIUpdate();
            m_Unit.SetStateCoolBack(eUnitStateType.Hit, UIUpdate);
            m_Unit.SetStateCoolBack(eUnitStateType.Dieing, UIUpdate);

        }
        else
        {
            gameObject.SetActive(false);
        }
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

        m_Jop.sprite = InGameUI.Instance.GetSprite(m_Unit.UnitData.Weapon.ToString());

        m_Skill_1.sprite = InGameUI.Instance.GetSprite(m_Unit.SkillList[0].Data.Name);
        m_Skill_2.sprite = InGameUI.Instance.GetSprite(m_Unit.SkillList[1].Data.Name);
    }

    public void LevelUp()
    {
        m_Unit.LevelUp(1); 
        UIUpdate();
    }

    public void OnClickUnitInfoOpenButton()
    {
        UnitInfo_Window win = UnitInfo_Window.Open(m_Unit);
        win.CloseCall = () =>
        {
            UIUpdate();
        };
    }


    private void Update()
    {
        if (m_Unit != null)
        {
            m_Cool.fillAmount = (m_Unit.SkillList[0] as ActiveSkill).CoolPercent;
        }
    }
}
