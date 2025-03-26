using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    public Image Boby = null;
    public Image Head = null;
    public Image Hair = null;
    public Image EyeL = null;
    public Image EyeR = null;

    public Text Level = null;
    public Text UnitCount = null;
    public Image HP = null;
    public Image Cool = null;

    public Image Skill_1 = null;
    public Image Skill_2 = null;

    public Image DieCover = null;

    private HeroUnit m_Unit = null;

    public void Init(HeroUnit unit)
    {
        if (unit != null)
        {
            gameObject.SetActive(true);
            m_Unit = unit;
            DieCover.gameObject.SetActive(unit.UnitState == eUnitStateType.Die || unit.UnitState == eUnitStateType.Dieing);
            Boby.color = m_Unit.UnitData.UnitColors[0];
            Head.color = m_Unit.UnitData.UnitColors[1];
            Hair.color = m_Unit.UnitData.UnitColors[2];
            EyeR.color = m_Unit.UnitData.UnitColors[3];
            EyeL.color = m_Unit.UnitData.UnitColors[4];

            Level.text = m_Unit.TotalLevel.ToString();
            HP.fillAmount = m_Unit.HP / m_Unit.MaxHP;
            UnitCount.text = m_Unit.LifeUnitCount.ToString();

            Skill_1.sprite = InGameUI.Instance.GetSprite(unit.SkillList[1].Data.Name);
            Skill_2.sprite = InGameUI.Instance.GetSprite(unit.SkillList[0].Data.Name);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void LevelUp()
    {
        m_Unit.LevelUp(1);
        Init(m_Unit);
    }


    private void Update()
    {
        if (m_Unit != null)
        {
            Cool.fillAmount = (m_Unit.SkillList[1] as ActiveSkill).CoolPercent;
        }
    }
}
