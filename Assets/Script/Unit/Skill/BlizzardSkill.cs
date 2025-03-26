using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardSkill : ActiveSkill
{
    public BlizzardSkill()
    {
        Data = new SkillData();
        Data.Name = "Blizzard";
        Data.Icon = "Blizzard";
        Data.ActiveEffectIndex = -1;
        Data.TargetEffectIndex = 7;
        Data.Description = "눈보라를 이르켜 넓은 범위의 적에게 {0}%의 피해를 준다";
        Data.MaxCoolTime = 33;
        Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Skill";
        Data.Value = 0.4f;
        Data.WeaponType = eWeaponType.Wand;
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
