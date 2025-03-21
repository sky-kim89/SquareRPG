using System;
using UnityEngine;
using System.Collections.Generic;

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

    //타겟 스킬
    public void Init(Unit target, Damage damage)
    {
        Target = target;
        m_Damage = damage;
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

    //충돌 체크 스킬
    public void Init(Unit target, List<Damage> damage)
    {
        //이팩트 출력
        //
    }

    private Vector3 tempPos = Vector3.one;
    public void Update()
    {
        if (m_Distance != 0)
        {
            float dis = Vector3.Distance(transform.position, Target.transform.position);
            if (dis >= 0.1f)
            {
                tempPos = transform.position;
                Vector3 temp = Vector3.Lerp(transform.position, Target.transform.position, m_Time);
                transform.position = new Vector3(temp.x, m_Curve.Evaluate(1f - dis / m_Distance) * m_Distance * 0.5f, temp.z);
                if(transform.position != tempPos)
                    transform.rotation.SetLookRotation((transform.position - tempPos).normalized);
                //Debug.Log(transform.eulerAngles);

                m_Time += Time.deltaTime * m_Speed;
            }
            else
            {
                m_Time = 0;
                Active();
                ObjectPool.Instance.Restore(gameObject);
            }
        }

        if(Target != null && !Target.isCanTarget)
            ObjectPool.Instance.Restore(gameObject);
    }

    public void Active()
    {
        if (m_ArrivalEffect != null)
            ObjectPool.Instance.GetObject(m_ArrivalEffect, Target.transform);
        Target.Hit(m_Damage);
        Index++;
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null && unit.isCanTarget && m_Damage != null && m_Damage.Unit.isEnemy != unit.isEnemy)
            unit.Hit(m_Damage);
    }

    private void OnParticleCollision(GameObject other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null && unit.isCanTarget && m_Damage != null && m_Damage.Unit.isEnemy != unit.isEnemy)
            unit.Hit(m_Damage);
    }
}
