using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//∆Ú≈∏
public class AttackSkill : ActiveSkill
{
    public AttackSkill()
    {
        Data = new SkillData();
        Data.MaxCoolTime = 1;
    }

    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        target.Hit(new Damage(unit, Data.Value));

        CoolTime = Data.MaxCoolTime;
    }
}

public class BowAttackSKill : AttackSkill
{
    public override void Active(Unit unit, Unit target)
    {
        Unit = unit;
        Target = target;
        SkillEffect skillEffect = ObjectPool.Instance.GetObject<SkillEffect>(unit.GetArrow(), target.transform);
        skillEffect.transform.position = unit.transform.position;
        skillEffect.Init(target, new Damage(unit, Data.Value));

        CoolTime = Data.MaxCoolTime;
    }
}