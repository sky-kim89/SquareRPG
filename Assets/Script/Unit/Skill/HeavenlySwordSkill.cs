using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenlySwordSkill : ActiveSkill
{
    public HeavenlySwordSkill()
    {
        Data = new SkillData();
        Data.Name = "HeavenlySword";
        Data.Icon = "HeavenlySword";
        Data.ActiveEffectIndex = -1;
        Data.TargetEffectIndex = 1;
        Data.Description = "주변에 검을 떨어 뜨려 적에게 {0}%의 피해를 준다";
        Data.MaxCoolTime = 20;
        Data.Knockback = 1;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Skill";
        Data.Value = 0.6f;
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
