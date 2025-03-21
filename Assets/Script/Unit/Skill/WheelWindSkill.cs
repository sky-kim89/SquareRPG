using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelWindSkill : ActiveSkill
{
    public WheelWindSkill()
    {
        Data = new SkillData();
        Data.Name = "WheelWind";
        Data.Icon = "WheelWind";
        Data.ActiveEffectIndex = -1;
        Data.TargetEffectIndex = 0;
        Data.Description = "360도 회전하며 주변의 적에게 {0}%의 피해를 준다";
        Data.MaxCoolTime = 7;
        Data.Knockback = 2;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "WheelWind";
        Data.Value = 1.6f;
        Data.WeaponType = eWeaponType.Sword;
    }
    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        if (Data.Animation != string.Empty)
            unit.PlayAnimation(Data.Animation);

        SkillEffect skillEffect = SkillManager.Instance.GetSkillEffect<SkillEffect>(Data.TargetEffectIndex);
        skillEffect.Init(target, new Damage(unit, Data.Value * unit.SkillDamageRate));
        skillEffect.transform.position = target.transform.position;

        CoolTime = Data.MaxCoolTime;
    }
}
