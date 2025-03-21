using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBuffType
{
    AP,
    HP,
    AddUnitCount,
    DamageRate,
    SkillDamageRate,
    AttackRange,
    AttackSpeed,
    MoveSpeed,
    SkillCoolTime,

    Last
}

//���� ����
//��ø, �ð� ����, Ÿ��?
public class Buff
{
    public Dictionary<eBuffType, float> BuffList = new Dictionary<eBuffType, float>();
    public int MaxStack = 0;
    public int Stack = 0;
    public int MaxCoolTime = 0;
    public int CoolTime = 0;

    public void Active(Unit unit)
    {

    }
}
