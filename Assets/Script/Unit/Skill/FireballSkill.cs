using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : ActiveSkill
{
    public FireballSkill()
    {
        Data = new SkillData();
        Data.Name = "Fireball";
        Data.Icon = "Fireball";
        Data.ActiveEffectIndex = -1;
        Data.TargetEffectIndex = 8;
        Data.Description = "화염구를 날려 적에게 {0}%의 피해를 준다";
        Data.MaxCoolTime = 25;
        Data.Knockback = 0.2f;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Skill";
        Data.Value = 3.2f;
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
