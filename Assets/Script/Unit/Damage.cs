using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    //�� ü�� ��� �� ������ ����� �ʿ� �� ��.
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

    //��ų ������ ����
    public float DamageRate = 1f;
    //��ų �°� ������ �˹�(����?)
    public float Knockback = 1f;
}
