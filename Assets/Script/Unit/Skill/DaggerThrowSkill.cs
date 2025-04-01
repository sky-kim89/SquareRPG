using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerThrowSkill : ActiveSkill
{
    public DaggerThrowSkill()
    {
        Data = new SkillData();
        Data.Name = "DaggerThrow";
        Data.Icon = "DaggerThrow";
        Data.ActiveEffectIndex = -1;
        Data.TargetEffectIndex = 2;
        Data.Description = "적 히어로에게 2개의 단검을 던진져 {0}의 피해를 준다.";
        Data.MaxCoolTime = 6;
        Data.Knockback = 1;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Skill";
        Data.Value = 1.2f;
        Data.WeaponType = eWeaponType.Dagger;
    }

    private int ActiveCount = 1;
    private float activeTime = 0.2f;
    private int count = 0;

    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        if (Data.Animation != string.Empty)
            unit.PlayAnimation(Data.Animation);
        SkillEffect skillEffect = SkillManager.Instance.GetSkillEffect<SkillEffect>(Data.TargetEffectIndex);
        skillEffect.Init(target, new Damage(unit, Data.Value * unit.SkillDamageRate));
        skillEffect.transform.position = unit.transform.position;

        if (ActiveCount >= count)
        {
            count = 0;
            CoolTime = Data.MaxCoolTime;
        }
        else
        {
            count++;
            CoolTime = activeTime;
        }
    }
}
