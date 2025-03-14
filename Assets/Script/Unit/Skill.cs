using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�нú� = ������ ����
//��Ƽ�� ���� = ƽ�� ����
//��Ƽ�� ����� = ƽ�� ����

//��Ƽ�� = ������Ʈ ��ȯ -> Ÿ�� + ������ ������ ����

public enum eSkillType
{
    ��Ÿ,
    Active_Attack,
    Active_Buff,
    �нú�
}

public class SkillData
{
    public string Name = "";
    public string Description = "";

    public string Icon = "";

    public float MaxCoolTime = 0;

    public GameObject ActiveEffect = null;
    public SkillEffect TargetEffect = null;

    public eSkillType SkillType = eSkillType.��Ÿ;

    //������, ���� index
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
