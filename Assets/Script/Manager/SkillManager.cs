using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ų ����Ʈ�� ���� �ִٰ� ��ų���� �ִ� Ŭ����
public class SkillManager : Singleton<SkillManager>
{
    //��ų ����Ʈ ����Ʈ
    [SerializeField]
    private List<GameObject> SkillEffects = new List<GameObject>();

    private List<System.Type> m_ActiveSkills = new List<System.Type>();
    private List<System.Type> m_PassiveSkills = new List<System.Type>();

    public void Awake()
    {
        m_ActiveSkills.Add(typeof(HeavenlySwordSkill));
        m_ActiveSkills.Add(typeof(WheelWindSkill));
        m_ActiveSkills.Add(typeof(MultiShotSkill));
        m_ActiveSkills.Add(typeof(ArrowRainSkill));
        m_ActiveSkills.Add(typeof(BlizzardSkill));
        m_ActiveSkills.Add(typeof(DaggerThrowSkill));
        m_ActiveSkills.Add(typeof(FireballSkill));
        m_ActiveSkills.Add(typeof(SneakAttackSkill));

        m_PassiveSkills.Add(typeof(MoveSpeedSkill));
        m_PassiveSkills.Add(typeof(AttackRangeSkill));
        m_PassiveSkills.Add(typeof(DamageRateSkill));
        m_PassiveSkills.Add(typeof(SkillDamageRateSkill));
        m_PassiveSkills.Add(typeof(UnitAddSkill));
        m_PassiveSkills.Add(typeof(FitnessUpSkill));
        m_PassiveSkills.Add(typeof(PowerUpSkill));
    }

    public T GetSkillEffect<T>(int index)
    {
        return SkillEffects[index].GetComponent<T>();
    }

    public GameObject GetSkillEffect(int index)
    {
        return SkillEffects[index];
    }
    public Skill GetRandomActiveSkill()
    {
        return (Skill)Activator.CreateInstance(m_ActiveSkills[UnityEngine.Random.Range(0, m_ActiveSkills.Count)]);
    }

    public Skill GetRandomPassiveSkill()
    {
        return (Skill)Activator.CreateInstance(m_PassiveSkills[UnityEngine.Random.Range(0, m_PassiveSkills.Count)]);
    }
}
