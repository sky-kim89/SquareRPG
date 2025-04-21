using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    public static void EnableCollider(this GameObject gameObject)
    {
        if (gameObject != null)
        {
            Collider[] colliders = gameObject.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public static void DisableCollider(this GameObject gameObject)
    {
        if (gameObject != null)
        {
            Collider[] colliders = gameObject.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    public static float GetBuffTypeToValue(this List<BuffData> buffs, Unit unit, eBuffType type)
    {
        float value = 0;

        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].IsApply)
            {
                Buff buff = buffs[i].BuffList.Find(temp => temp.eBuffType == type && (temp.eTargetType == unit.UnitType || temp.eTargetType == eTargetType.All));
                if (buff != null)
                {
                    switch (type)
                    {
                        case eBuffType.AP:
                            value += buff.Value;
                            break;
                        case eBuffType.HP:
                            value += buff.Value;
                            break;
                        case eBuffType.AddUnitCount:
                            value += buff.Value;
                            break;
                        case eBuffType.DamageRate:
                            value += buff.Value;
                            break;
                        case eBuffType.SkillDamageRate:
                            value += buff.Value;
                            break;
                        case eBuffType.AttackRange:
                            value += buff.Value;
                            break;
                        case eBuffType.AttackSpeed:
                            value += buff.Value;
                            break;
                        case eBuffType.MoveSpeed:
                            value += buff.Value;
                            break;
                        case eBuffType.SkillCoolTime:
                            value *= (1f - buff.Value);
                            break;
                        case eBuffType.DamageReduction:
                            value += buff.Value;
                            break;
                    }
                }
            }
        }

        return value;
    }

    public static List<BuffData> GetBuffClone(this List<BuffData> buffs)
    {
        List<BuffData> retData = new List<BuffData>();
        for (int i = 0; i < buffs.Count; i++)
        {
            BuffData temp = buffs[i].Clone();
            retData.Add(temp);
        }
        return retData;
    }

    public static int GetRecruitCost(this UnitData data)
    {
        return (int)(200 * (int)Mathf.Pow(2, (int)data.Grade - 1) * (2 - EconomyManager.Instance.GetEconomyData(eEconomyCardType.ShopDiscount))); // 100, 200, 400, 800, 1600
    }

    public static int GetHeroLevelUpCost(this UnitData data)
    {
        return Mathf.FloorToInt(80 * Mathf.Pow(data.Level, 1.1f) * (2 - EconomyManager.Instance.GetEconomyData(eEconomyCardType.UpgradeCostDiscount)));
    }

    public static int GetAddUnitCost(this UnitData data)
    {
        return (int)(300 * (2 - EconomyManager.Instance.GetEconomyData(eEconomyCardType.UpgradeCostDiscount)));
    }

    public static int GetSkillOpenCost(this UnitData data)
    {
        return (int)(data.OpenSkill * 400 * (2 - EconomyManager.Instance.GetEconomyData(eEconomyCardType.UpgradeCostDiscount)));
    }
}
