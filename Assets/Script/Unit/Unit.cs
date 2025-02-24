using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum eUnitStateType
{
    None = -1,
    Attack = 0,
    Hit = 1,
    Hiting = 2,
    Move = 3,
    Die = 4,
    Kill = 5,
    Start = 6,
    Skilling = 7,
    Attacking = 8,
    Dieing = 9
    //스턴 등 상태 이상 추가 필요
}
public enum eGradeType
{
    Common = 1,
    UnCommon = 2,
    Rare = 3,
    Unique = 4,
    Epic = 5,

    List = 6
}

//직업과 상관 없이 스킬을 갖고 있음.
public enum eJobType
{
    Warrior,
    Tanker,
    Assassin,
    Archer,
    Magician,
    Healer,
}


public class HeroSaveData
{
    public string Name = string.Empty;
    public int AddLevel = 1; //강화 레벨
    public int UpGrade = 0;  //강화 등급
    public int AddUnitCount = 0; //추가 부하 수

    //장착 무기
    //장착 아이템
    //유닛 배치 정보
}

[System.Serializable]
public class UnitData
{
    //public eAttackType AttackType = eAttackType.Normal;
    //public eJobType JobType = eJobType.Warrior;
    //public eSizeType Size = eSizeType.Middle;
    public eGradeType Grade = eGradeType.Common;
    public eJobType Job = eJobType.Warrior;

    public string Name = string.Empty;

    public float AP = 1; //일반 공격
    public float SP = 1; //스킬 공격
    public float HP = 1; //체력
    public float LP = 1; //리더쉽 (부하 관련)

    public int Level = 1; //레벨

    public int AddLevel = 0;
    public int UpGrade = 0;

    public float DamageRate = 1.0f;

    public int Exp = 0;

    public float AttackRange = 1;
    public float AttackSpeed = 1.0f;
    public float MoveSpeed = 1;

    //public ISkill[] Skills = new ISkill[2];
    //public Color[] UnitColors = new Color[5];
}
//스킬이 없는 기본 유닛 = 부하
public class Unit : MonoBehaviour
{
    [SerializeField]
    protected UnitStateUI m_StateUI = null;
    [SerializeField]
    protected Animator m_Animator = null;
    [SerializeField]
    protected GameObject m_RootObj = null;


    protected UnitData UnitData = null;
    protected UnitData BuffUnitData = null;

    protected eUnitStateType m_UnitState = eUnitStateType.None;

    protected List<Buff> m_Buffs = new List<Buff>();

    protected Unit Target = null;

    public float AP
    {
        get
        {
            return BuffUnitData.AP;
        }
    }

    private float m_HP = 1;

    public float HP
    {
        get
        {
            return m_HP;
        }
    }
    public float MaxHP
    {
        get
        {
            return BuffUnitData.HP;
        }
    }

    public float SP
    {
        get
        {
            return BuffUnitData.SP;
        }
    }

    public float LP
    {
        get
        {
            return BuffUnitData.LP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(UnitData data)
    {
        UnitData = data;
    }

    //공격
    public virtual void Attack()
    {
    }

    //피격
    public virtual void Hit(Damage damage)
    {
        if (m_UnitState != eUnitStateType.Dieing)
        {
            m_UnitState = eUnitStateType.Hit;
            //ActiveSkillOperate(damage.Unit);

            //Job.Hit(damage);
            float hp = (float)HP / (float)MaxHP;

            m_StateUI.SetHP(hp);
            //m_StateUI.Hit(damage.Damage);
            if (HP <= 0)
            {
                Die(damage.Unit);
            }
            else
            {
                m_Animator.Play("Hit");
            }
        }
    }

    //죽음
    public virtual void Die(Unit unit)
    {
        if (unit != null)
        {
            unit.Kill(this);
        }

        m_Animator.Play("Die");
        gameObject.DisableCollider();

        m_UnitState = eUnitStateType.Dieing;

        DelayAction(0.5f, () =>
        {
            m_UnitState = eUnitStateType.Die;
            m_RootObj.SetActive(false);
        });
        //ActiveSkillOperate(unit);
    }

    //죽임
    public virtual void Kill(Unit unit)
    {
        //적을 죽일 경우 발동하는 스킬 발동용
    }
    protected virtual void DelayAction(float delay, Action action)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(DelayActionHandler(delay, action));
        }
    }

    protected IEnumerator DelayActionHandler(float delay, Action action)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        if (action != null)
        {
            action();
        }
    }

   
    //찾는 방식에 따라 아군도 찾아오게 음... 고민이 필요함.
    protected virtual Unit FindEnemy()
    {
        Unit target = null;
        Target = target;
        return target;
    }
}
