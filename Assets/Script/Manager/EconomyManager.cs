using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEconomyCardType
{
    AddGold,                //°ñµå È¹µæ
    GoldGainBonus,              //°ñµå È¹µæ ½Ã Ãß°¡ È¹µæ
    SoulGainBonus,              //¼Ò¿ï È¹µæ ½Ã Ãß°¡ È¹µæ·ü
    ShopDiscount,           //¼¥ ÇÒÀÎ
    UpgradeCostDiscount,    //¾÷±×·¹ÀÌµå ºñ¿ë ÇÒÀÎ
    StageClearGold          //Å¬¸®¾î °ñµå Ãß°¡ È¹µæ·ü
}

public class EconomyData
{

}

public class EconomyManager : Singleton<EconomyManager>
{
    private Dictionary<eEconomyCardType, float> EconomyData = new Dictionary<eEconomyCardType, float>();

    //È÷¾î·Î ¿µÀÔ, ·¹º§¾÷, ½ºÅ³ ¿ÀÇÂ µî¿¡ »ç¿ë
    private int m_Gold = 0;
    public int Gold { get { return m_Gold; } }
    //ºÎÇÏ ÀÎ¿ø¼ö ´Ã¸®´Âµ¥ »ç¿ë
    private int m_Soul = 0;
    public int Soul { get { return m_Soul; } }


    public void AddGold(int gold)
    {
        //°ñµå ¹öÇÁ °ü·ÃÇØ¼­ Àû¿ë ÇÊ¿ä
        float GoldGainBonus = GetEconomyData(eEconomyCardType.GoldGainBonus);
        m_Gold += (int)(gold * GoldGainBonus) ;
        GoldText.GoldUIUpdate();
    }

    public void AddSoul(int soul)
    {
        float SoulGainBonus = GetEconomyData(eEconomyCardType.SoulGainBonus);
        m_Soul += (int)(Soul * SoulGainBonus) ;
    }

    public bool UseGold(int use)
    {
        if(m_Gold > use)
        {
            m_Gold -= use;
            GoldText.GoldUIUpdate();
            return true;
        }

        return false;
    }

    public bool UseSoul(int use)
    {
        if (m_Soul > use)
        {
            m_Soul -= use;
            return true;
        }

        return false;
    }

    public void StageClear(int stage)
    {
        float StageClearGold = GetEconomyData(eEconomyCardType.StageClearGold);

        AddGold(Mathf.FloorToInt(50 * Mathf.Sqrt(stage) * StageClearGold));
        AddSoul(Mathf.FloorToInt(Mathf.Sqrt(stage)));
    }

    public void AddEconomyData(eEconomyCardType economyData, float count)
    {
        if (EconomyData.ContainsKey(economyData))
            EconomyData[economyData] += count;
        else
            EconomyData.Add(economyData, count);
    }

    public float GetEconomyData(eEconomyCardType economyData)
    {
        float temp = 1;
        if (EconomyData.ContainsKey(economyData))
            temp += EconomyData[economyData];
        else
            EconomyData.Add(economyData, 0);

        return temp;
    }

    public void ClearEconomyData()
    {
        EconomyData.Clear();
    }
}
