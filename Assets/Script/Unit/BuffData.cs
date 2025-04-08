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
    //버프 고유 이름
    public string BuffName = string.Empty;

    //버프 발동 조건 추가 필요
    public eUnitStateType eTriggerType = eUnitStateType.None;
    public List<Buff> BuffList = new List<Buff>();

    public int MaxStack = 0;
    public int Stack = 0;
    //지속시간
    public float MaxDuration = 0;
    //지속 시간 카운팅
    public float Duration = 0;
    //중첩에 필요한 쿨타임 시간
    public float MaxCoolTime = 0;
    //쿨타임 카운팅
    public float CoolTime = 0;

    public string ActiveEffect = string.Empty;

    public Action<Unit> Apply = null;
}

public class Buff
{
    public Buff(eTargetType unitType, eBuffType buffType, float value)
    {
        eTargetType = unitType;
        eBuffType = buffType;
        Value = value;
    }

    public eTargetType eTargetType = eTargetType.All;
    public eBuffType eBuffType = eBuffType.AP;
    public float Value = 0;
}
