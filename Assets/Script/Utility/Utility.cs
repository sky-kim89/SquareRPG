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

    public static float GetBuffTypeToValue(this List<BuffData> buffs, eBuffType type, eTargetType unitType)
    {
        float value = 0;

        for (int i = 0; i < buffs.Count; i++)
        {
            Buff buff = buffs[i].BuffList.Find(temp => temp.eBuffType == type && temp.eTargetType == unitType );
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
                }
            }
        }

        return value;
    }
}
