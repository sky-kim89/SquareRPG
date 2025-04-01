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

    Last
}

//버프 종류
//중첩, 시간 제한, 타수?
public class Buff
{
    //버프 발동 조건 추가 필요
    public eUnitStateType UnitState = eUnitStateType.None;
    public Dictionary<eBuffType, float> BuffList = new Dictionary<eBuffType, float>();

    public int MaxStack = 0;
    public int Stack = 0;
    public int MaxCoolTime = 0;
    public int CoolTime = 0;

    public Action<Unit> Active = null;
}
