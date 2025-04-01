using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveSkill : Skill
{
    public eGradeType Grade = eGradeType.Common;
    public Buff Buff = new Buff();

    public PassiveSkill(eGradeType grade)
    {
        Grade = grade;
    }
}

public class MoveSpeedSkill : PassiveSkill
{
    public MoveSpeedSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "MoveSpeed";
        Data.Icon = "MoveSpeed";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "이동속도가 {0}%만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.1f * ((float)Grade * 0.5f);
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.MoveSpeed, Data.Value);
    }
}
public class AttackRangeSkill : PassiveSkill
{
    public AttackRangeSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "AttackRange";
        Data.Icon = "AttackRange";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "공격 사정거리가 {0}%만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.1f * ((float)Grade * 0.5f);
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.AttackRange, Data.Value);
    }
}
public class DamageRateSkill : PassiveSkill
{
    public DamageRateSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "DamageRate";
        Data.Icon = "DamageRate";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "데미지가가 {0}%만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.1f * ((float)Grade * 0.5f);
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.DamageRate, Data.Value);
    }
}
public class SkillDamageRateSkill : PassiveSkill
{
    public SkillDamageRateSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "SkillDamageRate";
        Data.Icon = "SkillDamageRate";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "스킬 데미지가가 {0}%만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.2f * ((float)Grade * 0.5f);
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.SkillDamageRate, Data.Value);
    }
}
public class UnitAddSkill : PassiveSkill
{
    public UnitAddSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "UnitAdd";
        Data.Icon = "UnitAdd";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "부하 최대치가 {0}만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        double value = 
        Data.Value = 0.01f * (float)Grade;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.AddUnitCount, Data.Value * 100);
    }
}
public class FitnessUpSkill : PassiveSkill
{
    public FitnessUpSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "FitnessUp";
        Data.Icon = "FitnessUp";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "최대 체력이 {0}%만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.1f * ((float)Grade * 0.5f);
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.HP, Data.Value);
    }
}
public class PowerUpSkill : PassiveSkill
{
    public PowerUpSkill(eGradeType grade) : base(grade)
    {
        Data = new SkillData();
        Data.Name = "PowerUp";
        Data.Icon = "PowerUp";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "공격력이 {0}%만큼 증가";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.1f * ((float) Grade * 0.5f);
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.AP, Data.Value);
    }
}
