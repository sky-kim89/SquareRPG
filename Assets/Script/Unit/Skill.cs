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
    평타,
    Active_Attack,
    Active_Buff,
    패시브
}

public class SkillData
{
    public string Name = "";
    public string Description = "";

    public string Icon = "";

    public float MaxCoolTime = 0;

    public GameObject ActiveEffect = null;
    public SkillEffect TargetEffect = null;

    public eSkillType SkillType = eSkillType.평타;

    //데미지, 버프 index
    public float Value = 1;
}

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

public class ActiveSkill : Skill
{
    public override void Active(Unit unit, Unit target)
    {
        if (Data.MaxCoolTime < CoolTime)
        {
            Unit = unit;
            Target = target;
            SkillEffect skillEffect = ObjectPool.Instance.GetObject<SkillEffect>(Data.TargetEffect.gameObject, target.transform);
            skillEffect.Init(target, (i) =>
            {
                target.Hit(new Damage(unit, Data.Value));
            });
        }
    }
}

public class BuffSkill : Skill
{

}

public class PassiveSkill : Skill
{

}
