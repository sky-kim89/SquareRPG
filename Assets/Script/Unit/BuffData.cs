using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
[System.Serializable]
public class BuffData
{
    //버프 고유 이름
    public string BuffName = string.Empty;

    //버프 발동 조건 추가 필요
    public eUnitStateType eTriggerType = eUnitStateType.None;
    public List<Buff> BuffList = new List<Buff>();

    public bool IsApply {  get { return Stack > 0 || eTriggerType == eUnitStateType.None; } }

    public int MaxStack = 1;
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

    public bool Apply(Unit unit)
    {
        if ((unit.UnitState == eTriggerType || eTriggerType == eUnitStateType.None )&& (MaxCoolTime == 0 || CoolTime < 0))
        {
            if (MaxStack > Stack)
            {
                Stack++;
                Duration = MaxDuration;
                CoolTime = MaxCoolTime;
                if (Command != null)
                    Command(unit);
                for (int i = 0; i < BuffList.Count; i++)
                {
                    BuffList[i].Stack = Stack;
                }
                return true;
            }

            if (MaxStack == 0)
            {
                if (Command != null)
                    Command(unit);
            }
        }

        return false;
    }

    public bool TickDown(Unit unit)
    {
        if (MaxDuration == 0 || Duration < 0)
        {
            if (Stack > 0)
            {
                Stack--;
                if (Stack > 0)
                {
                    Duration = MaxDuration;
                }

                for (int i = 0; i < BuffList.Count; i++)
                {
                    BuffList[i].Stack = Stack;
                }
                return true;
            }
        }

        return false;
    }

    public BuffData Clone()
    {
        BuffData copy = (BuffData)this.MemberwiseClone();
        copy.BuffList = BuffList.Select(b => b.Clone()).ToList();
        return copy;
    }

    public Action<Unit> Command = null;
}

[System.Serializable]
public class Buff
{
    public Buff(eTargetType unitType, eBuffType buffType, float value)
    {
        eTargetType = unitType;
        eBuffType = buffType;
        m_Value = value;
    }
    public Buff Clone()
    {
        return new Buff(this.eTargetType, this.eBuffType, this.m_Value);
    }

    public int Stack = 0;

    public eTargetType eTargetType = eTargetType.All;
    public eBuffType eBuffType = eBuffType.AP;
    private float m_Value = 0;
    public float Value { get { return m_Value * Stack; } }
}
