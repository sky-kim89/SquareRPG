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

    public static float GetBuffTypeToValue(this List<Buff> buffs, eBuffType type)
    {
        float value = 0;

        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].BuffList.ContainsKey(type))
            {
                switch (type)
                {
                    case eBuffType.AP:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.HP:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.AddUnitCount:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.DamageRate:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.SkillDamageRate:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.AttackRange:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.AttackSpeed:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.MoveSpeed:
                        value += buffs[i].BuffList[type];
                        break;
                    case eBuffType.SkillCoolTime:
                        value *= (1f - buffs[i].BuffList[type]);
                        break;
                }
            }
        }

        return value;
    }
}
