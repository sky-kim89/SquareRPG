using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainSkill : ActiveSkill
{
    public ArrowRainSkill()
    {
        Data = new SkillData();
        Data.Name = "ArrowRain";
        Data.Icon = "ArrowRain";
        Data.ActiveEffectIndex = 5;
        Data.TargetEffectIndex = 4;
        Data.Description = "화살을 하늘로 쏴 올려 일대의 적에게 화살비 공격을 하여 {0}%의 피해를 준다";
        Data.MaxCoolTime = 20;
        Data.Knockback = 0.1f;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Skill";
        Data.Value = 0.4f;
        Data.WeaponType = eWeaponType.Bow;
    }
    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        if (Data.Animation != string.Empty)
            unit.PlayAnimation(Data.Animation);
        GameObject effect = SkillManager.Instance.GetSkillEffect(Data.ActiveEffectIndex);
        effect.transform.position = unit.transform.position;

        SkillEffect skillEffect = SkillManager.Instance.GetSkillEffect<SkillEffect>(Data.TargetEffectIndex);
        skillEffect.Init(target, new Damage(unit, Data.Value * unit.SkillDamageRate));
        skillEffect.transform.position = target.transform.position;

        CoolTime = Data.MaxCoolTime;
    }
}
