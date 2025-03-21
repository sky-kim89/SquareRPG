using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    //적 체력 비례 딜 같은거 고민이 필요 할 듯.
    public Damage(Unit unit, float damageRate, float knockback = 1)
    {
        m_Unit = unit;
        DamageRate = damageRate;
        Knockback = knockback;
    }

    private Unit m_Unit = null;
    public Unit Unit
    {
        get
        {
            return m_Unit;
        }
    }

    private int m_DamagePoint = 0;
    public int DamagePoint
    {
        get
        {
            return (int)(((float)m_Unit.AP * (float)m_Unit.DamageRate) * DamageRate);
        }
    }

    //스킬 데미지 증폭
    public float DamageRate = 1f;
    //스킬 맞고 움직일 넉백(구현?)
    public float Knockback = 1f;
}
