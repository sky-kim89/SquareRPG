using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedSkill : PassiveSkill
{
    public MoveSpeedSkill()
    {
        Data = new SkillData();
        Data.Name = "MoveSpeed";
        Data.Icon = "MoveSpeed";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "�̵��ӵ��� {0}��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        //Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.MoveSpeed, 0.1f);
    }
}
public class AttackRangeSkill : PassiveSkill
{
    public AttackRangeSkill()
    {
        Data = new SkillData();
        Data.Name = "AttackRange";
        Data.Icon = "AttackRange";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "���� �����Ÿ��� {0}��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        //Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.AttackRange, 0.1f);
    }
}
public class DamageRateSkill : PassiveSkill
{
    public DamageRateSkill()
    {
        Data = new SkillData();
        Data.Name = "DamageRate";
        Data.Icon = "DamageRate";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "���������� {0}��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        //Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.DamageRate, 0.1f);
    }
}
public class SkillDamageRateSkill : PassiveSkill
{
    public SkillDamageRateSkill()
    {
        Data = new SkillData();
        Data.Name = "SkillDamageRate";
        Data.Icon = "SkillDamageRate";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "��ų ���������� {0}��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        //Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.SkillDamageRate, 0.2f);
    }
}
public class UnitAddSkill : PassiveSkill
{
    public UnitAddSkill()
    {
        Data = new SkillData();
        Data.Name = "UnitAdd";
        Data.Icon = "UnitAdd";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "���� �ִ�ġ�� {0}��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        //Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.AddUnitCount, 1f);
    }
}
public class FitnessUpSkill : PassiveSkill
{
    public FitnessUpSkill()
    {
        Data = new SkillData();
        Data.Name = "FitnessUp";
        Data.Icon = "FitnessUp";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "�ִ� ü���� {0}%��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        //Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.HP, 0.1f);
    }
}
public class PowerUpSkill : PassiveSkill
{
    public PowerUpSkill()
    {
        Data = new SkillData();
        Data.Name = "PowerUp";
        Data.Icon = "PowerUp";
        //Data.ActiveEffectIndex = -1;
        //Data.TargetEffectIndex = 8;
        Data.Description = "���ݷ��� {0}%��ŭ ����";
        //Data.MaxCoolTime = 33;
        //Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Passive;
        //Data.Animation = "Skill";
        Data.Value = 0.1f;
        Data.WeaponType = eWeaponType.ALL;

        Buff = new Buff();
        Buff.BuffList.Add(eBuffType.AP, 0.1f);
    }
}
