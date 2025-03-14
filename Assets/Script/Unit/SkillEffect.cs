using System;
using UnityEngine;
using System.Collections;

public enum eSkillEffectMoveType
{
    Move,
    Animation,
    Immediately,
}

public class SkillEffect : MonoBehaviour
{
    //[HideInInspector]
    //public Damage Damage;

    [HideInInspector]
    public Unit Target;
    [HideInInspector]
    public Action<int> CallBack;
    [HideInInspector]
    public int Index = 0;

    [SerializeField]
    private AnimationCurve m_Curve;
    [SerializeField]
    private float m_Speed = 0.1f;
    [SerializeField]
    private eSkillEffectMoveType m_MoveType = eSkillEffectMoveType.Move;
    private float m_Time = 0;
    private float m_Distance = 0;

    [SerializeField]
    private GameObject m_ArrivalEffect = null;

    public void Init(Unit target, Action<int> callBack)
    {
        Target = target;
        CallBack = callBack;
        switch (m_MoveType)
        {
            case eSkillEffectMoveType.Move:
                m_Distance = Vector3.Distance(transform.position, target.transform.position);
                break;
            case eSkillEffectMoveType.Immediately:
                Active();
                break;
        }
    }

    private Vector3 tempPos = Vector3.one;
    public void Update()
    {
        float dis = Vector3.Distance(transform.position, Target.transform.position);
        if (dis <= 0.1f)
        {
            tempPos = transform.position;
            Vector3 temp = Vector3.Lerp(transform.position, Target.transform.position, m_Time);
            transform.position = new Vector3(temp.x, temp.y, m_Curve.Evaluate(1f - dis / m_Distance));
            transform.rotation.SetLookRotation(tempPos - transform.position);

            m_Time += Time.deltaTime * m_Speed;
        }
        else
        {
            m_Time = 0;
            Active();
        }


    }

    public void Active()
    {
        if (m_ArrivalEffect != null)
            ObjectPool.Instance.GetObject(m_ArrivalEffect, Target.transform);
        CallBack(Index);
        Index++;
    }

    public void OnDisable()
    {
        ObjectPool.Instance.Restore(gameObject);
    }
}
