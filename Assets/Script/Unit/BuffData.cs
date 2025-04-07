using System;
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
    DamageReduction,
    Heel,

    Last,
}

//버프 종류
//중첩, 시간 제한, 타수?
public class BuffData
{
    //버프 발동 조건 추가 필요
    public eUnitStateType eTriggerType = eUnitStateType.None;
    public List<Buff> BuffList = new List<Buff>();

    public int MaxStack = 0;
    public int Stack = 0;
    public float MaxCoolTime = 0;
    public float CoolTime = 0;

    public string ActiveEffect = string.Empty;

    public Action<Unit> Apply = null;
}

public class Buff
{
    public Buff(eTargetType unitType, eBuffType buffType, float value)
    {
        unitType = eTargetType;
        buffType = eBuffType;
        value = Value;
    }

    public eTargetType eTargetType = eTargetType.All;
    public eBuffType eBuffType = eBuffType.AP;
    public float Value = 0;
}
