using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAttackSkill : ActiveSkill
{
    public SneakAttackSkill()
    {
        Data = new SkillData();
        Data.Name = "SneakAttack";
        Data.Icon = "SneakAttack";
        Data.ActiveEffectIndex = 3;
        Data.TargetEffectIndex = 3;
        Data.Description = "적 히어로 뒤로 이동 후 2회 공격하여 {0}%의 피해를 준다.";
        Data.MaxCoolTime = 15;
        Data.Knockback = 0.5f;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Attack_Dagger";
        Data.Value = 3.0f;
        Data.WeaponType = eWeaponType.Dagger;
    }
    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        if (Data.Animation != string.Empty)
            unit.PlayAnimation(Data.Animation);
        GameObject effect = SkillManager.Instance.GetSkillEffect(Data.ActiveEffectIndex);
        effect.transform.position = unit.transform.position;

        unit.transform.position = target.transform.forward * -1;

        SkillEffect skillEffect = SkillManager.Instance.GetSkillEffect<SkillEffect>(Data.TargetEffectIndex);
        skillEffect.Init(target, new Damage(unit, Data.Value * unit.SkillDamageRate));
        skillEffect.transform.position = unit.transform.position;

        CoolTime = Data.MaxCoolTime;
    }
}
