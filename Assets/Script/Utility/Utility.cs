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
            for (eBuffType j = 0; j < eBuffType.Last; j++)
            {
                if (buffs[i].BuffList.ContainsKey(j))
                {
                    switch (j)
                    {
                        case eBuffType.AP:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.HP:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.AddUnitCount:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.DamageRate:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.SkillDamageRate:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.AttackRange:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.AttackSpeed:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.MoveSpeed:
                            value += buffs[i].BuffList[j];
                            break;
                        case eBuffType.SkillCoolTime:
                            value *= (1f - buffs[i].BuffList[j]);
                            break;
                    }
                }
            }
        }

        return value;
    }
}
