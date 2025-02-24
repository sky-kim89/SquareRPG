using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    //적 체력 비례 딜 같은거 고민이 필요 할 듯.
    public Damage(Unit unit, int damageRate)
    {
        m_Unit = unit;
        DamageRate = damageRate;
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
            return (int)(((float)m_Unit.AP) * DamageRate);
        }
    }

    public int DamageRate = 1000;
    public float Knockback = 1f;
}
