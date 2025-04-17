using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamageMeterUI : MonoBehaviour
{
    [SerializeField]
    private Text m_DamageText = null;
    [SerializeField]
    private Text m_DamageDealtText = null;
    [SerializeField]
    private Text m_DamageReducedText = null;

    [SerializeField]
    private Image m_Damage = null;
    [SerializeField]
    private Image m_DamageDealt = null;
    [SerializeField]
    private Image m_DamageReduced = null;

    [SerializeField]
    private UnitView m_UnitView = null;

    public void Init(HeroUnit unit, SaveDamageStats maxDamageStats)
    {
        if (unit != null)
        {
            gameObject.SetActive(true);
            m_UnitView.Init(unit);

            m_DamageText.text = unit.SaveDamageStats.Damage.ToString("F0");
            m_DamageDealtText.text = unit.SaveDamageStats.DamageDealt.ToString("F0");
            m_DamageReducedText.text = unit.SaveDamageStats.DamageReduced.ToString("F0");

            m_Damage.fillAmount = unit.SaveDamageStats.Damage / maxDamageStats.Damage;
            m_DamageDealt.fillAmount = unit.SaveDamageStats.DamageDealt / maxDamageStats.DamageDealt;
            m_DamageReduced.fillAmount = unit.SaveDamageStats.DamageReduced / maxDamageStats.DamageReduced;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
