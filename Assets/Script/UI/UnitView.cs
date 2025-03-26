using System.Collections;
using System.Collections.Generic;
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

    private HeroUnit m_Unit = null;

    public void Init(HeroUnit unit)
    {
        m_Unit = unit;
        Boby.color = m_Unit.UnitData.UnitColors[0];
        Head.color = m_Unit.UnitData.UnitColors[1];
        Hair.color = m_Unit.UnitData.UnitColors[2];
        EyeR.color = m_Unit.UnitData.UnitColors[3];
        EyeL.color = m_Unit.UnitData.UnitColors[4];


        Level.text = m_Unit.TotalLevel.ToString();
        UnitCount.text = m_Unit.TatalUnitCount.ToString();
    }

    private void Update()
    {
        if (m_Unit != null)
        {
            Cool.fillAmount = (m_Unit.SkillList[1] as ActiveSkill).CoolPercent;
            HP.fillAmount = m_Unit.HP / m_Unit.MaxHP;
        }
    }
}
