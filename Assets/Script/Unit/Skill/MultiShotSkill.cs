using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotSkill : ActiveSkill
{
    public MultiShotSkill()
    {
        Data = new SkillData();
        Data.Name = "MultiShot";
        Data.Icon = "MultiShot";
        Data.ActiveEffectIndex = 6;
        Data.TargetEffectIndex = -1;
        Data.Description = "���濡 ȭ���� �� {0}%�� ���ظ� �ش�";
        Data.MaxCoolTime = 5;
        Data.Knockback = 0.5f;
        Data.SkillType = eSkillType.Active;
        Data.Animation = "Attack_Bow";
        Data.Value = 0.8f;
        Data.WeaponType = eWeaponType.Bow;
    }
    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        if (Data.Animation != string.Empty)
            unit.PlayAnimation(Data.Animation);
        SkillEffect skillEffect = SkillManager.Instance.GetSkillEffect<SkillEffect>(Data.ActiveEffectIndex);
        skillEffect.Init(target, new Damage(unit, Data.Value * unit.SkillDamageRate));
        skillEffect.transform.position = unit.transform.position;

        CoolTime = Data.MaxCoolTime;
    }
}
