using MyProjeckt;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveDamageStats
{
    public float Damage = 0;
    public float DamageDealt = 0;
    public float DamageReduced = 0;

    public void ReSet()
    {
        Damage = 0;
        DamageDealt = 0;
        DamageReduced = 0;
    }
}


public class BattleResult_Window : BackBaseWindow
{
    [SerializeField]
    private List<ShowDamageMeterUI> m_ShowDamageMeterUI = new List<ShowDamageMeterUI>();

    public override void OnInit()
    {
        SaveDamageStats max = new SaveDamageStats();
        max.Damage = UnitManager.Instance.MyHeroUniy.Max(unit => unit.SaveDamageStats.Damage);
        max.DamageDealt = UnitManager.Instance.MyHeroUniy.Max(unit => unit.SaveDamageStats.DamageDealt);
        max.DamageReduced = UnitManager.Instance.MyHeroUniy.Max(unit => unit.SaveDamageStats.DamageReduced);

        for (int i = 0; i < m_ShowDamageMeterUI.Count; i++)
        {
            if(UnitManager.Instance.MyHeroUniy.Count > i)
                m_ShowDamageMeterUI[i].Init(UnitManager.Instance.MyHeroUniy[i], max);
            else
                m_ShowDamageMeterUI[i].Init(null, max);
        }
    }

    public void OnClickShopButton()
    {
        Close();
        WindowManager.Instance.Open<UnitShop_Window>(WindowIds.UnitShop_Window);
    }

    public void OnClickCardSelectButton()
    {
        Close();
        WindowManager.Instance.Open<CardSelect_Window>(WindowIds.CardSelect_Window);
    }

    public override void BackButtonClick()
    {
        //Close();
    }
}
