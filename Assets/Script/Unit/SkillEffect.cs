using System;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public enum eSkillEffectMoveType
{
    Arrow,
    Collider,
    Immediately,
}

public class SkillEffect : MonoBehaviour
{
    [HideInInspector]
    private Damage m_Damage;

    [HideInInspector]
    public Unit Target;
    [HideInInspector]
    public int Index = 0;

    [SerializeField]
    private AnimationCurve m_Curve;
    [SerializeField]
    private float m_Speed = 0.1f;
    [SerializeField]
    private eSkillEffectMoveType m_MoveType = eSkillEffectMoveType.Arrow;
    private float m_Time = 0;
    private float m_Distance = 0;

    [SerializeField]
    private GameObject m_ArrivalEffect = null;

    //Ÿ�� ��ų
    public void Init(Unit target, Damage damage)
    {
        Target = target;
        m_Damage = damage;
        Index = 0;
        switch (m_MoveType)
        {
            case eSkillEffectMoveType.Arrow:
                m_Distance = Vector3.Distance(transform.position, target.transform.position);
                break;
            case eSkillEffectMoveType.Immediately:
                Active();
                break;
            case eSkillEffectMoveType.Collider:
                break;
        }
    }

    //�浹 üũ ��ų
    public void Init(Unit target, List<Damage> damage)
    {
        //����Ʈ ���
        //
    }

    private Vector3 tempPos = Vector3.one;
    public void Update()
    {
        if (m_MoveType == eSkillEffectMoveType.Arrow)
        {
            if (m_Distance != 0)
            {
                float dis = Vector3.Distance(transform.position, Target.transform.position);
                if (dis >= 0.1f)
                {
                    tempPos = transform.position;
                    Vector3 temp = Vector3.Lerp(transform.position, Target.transform.position, m_Time);
                    transform.position = new Vector3(temp.x, m_Curve.Evaluate(1f - dis / m_Distance), temp.z);
                    if (transform.position != tempPos)
                        transform.rotation = Quaternion.LookRotation(transform.position - tempPos);
                    //Debug.Log(transform.eulerAngles);

                    m_Time += Time.deltaTime * m_Speed;
                }
                else
                {
                    m_Time = 0;
                    Active();
                }
            }
        }
    }

    public void Active()
    {
        if (m_ArrivalEffect != null)
        {
            GameObject obj = ObjectPool.Instance.GetObject(m_ArrivalEffect, SkillManager.Instance.transform);
            obj.transform.position = Target.transform.position;
            SkillEffect effect = obj.GetComponent<SkillEffect>();
            if (effect != null)
                effect.Init(Target, m_Damage);

            ObjectPool.Instance.Restore(gameObject);
        }
        else
        {
            if (Target != null && Target.IsDie)
                ObjectPool.Instance.Restore(gameObject);
            else
                Target.Hit(m_Damage);
        }

        Index++;
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null && !unit.IsDie && m_Damage != null && m_Damage.Unit.isEnemy != unit.isEnemy)
            unit.Hit(m_Damage);
    }

    private void OnParticleCollision(GameObject other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null && !unit.IsDie && m_Damage != null && m_Damage.Unit.isEnemy != unit.isEnemy)
            unit.Hit(m_Damage);
    }
}
