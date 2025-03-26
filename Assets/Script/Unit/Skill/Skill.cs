using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//패시브 = 버프로 제작
//엑티브 버프 = 틱톡 버프
//엑티브 디버프 = 틱톡 버프

//엑티브 = 오브젝트 소환 -> 타겟 + 데미지 정보만 전달

public enum eSkillType
{
    Attack,
    Active,
    Active_Buff,
    Passive
}

public class SkillData
{
    public string Name = "";
    public string Description = "";

    public string Icon = "";
    public string Animation = "";

    public float MaxCoolTime = 0;

    public int ActiveEffectIndex = -1;
    public int TargetEffectIndex = -1;

    public eSkillType SkillType = eSkillType.Attack;
    public eWeaponType WeaponType = eWeaponType.Sword;

    //데미지, 버프 index
    public float Value = 1;
    public float Knockback = 1;
}

[System.Serializable]
public abstract class Skill
{
    public Unit Unit = null;
    public Unit Target = null;

    public SkillData Data;
    public float CoolTime = 0;

    public virtual void Active(Unit unit, Unit target)
    {
    }
}

[System.Serializable]
public class ActiveSkill : Skill
{
    public bool isCoolTime { get { return 0 > CoolTime; } }
    public float CoolPercent { get { return Data.MaxCoolTime / CoolTime; } }
    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        if (Data.Animation != string.Empty)
            unit.PlayAnimation(Data.Animation);
        SkillEffect skillEffect = SkillManager.Instance.GetSkillEffect<SkillEffect>(Data.TargetEffectIndex);
        skillEffect.Init(target, new Damage(unit, Data.Value));

        CoolTime = Data.MaxCoolTime;
    }
}

public class BuffSkill : Skill
{

}

public class PassiveSkill : Skill
{
    public Buff Buff = new Buff();
}
