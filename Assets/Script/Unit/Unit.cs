using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

public enum eUnitStateType
{
    None = -1,
    Attack = 0,
    Hit = 1,
    Move = 3,
    Kill = 5,
    Start = 6,
    Skilling = 7,
    Attacking = 8,
    Hiting = 11,
    Die = 12,
    Dieing = 13,
    //스턴 등 상태 이상 추가 필요
}
public enum eGradeType
{
    Common = 1,
    UnCommon = 2,
    Rare = 3,
    Unique = 4,
    Epic = 5,

    Last = 6
}

//직업과 상관 없이 스킬을 갖고 있음.
public enum eWeaponType
{
    Sword,
    Shield,
    Dagger,
    Bow,
    Wand,
    Last,

    ALL
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
    public eWeaponType Weapon = eWeaponType.Sword;

    public string Name = string.Empty;

    public float AP = 1; //일반 공격
    public float HP = 1; //체력
    public float SP = 1; //스킬 영향
    public float LP = 1; //리더쉽 (부하 관련)

    public int Level = 1; //레벨

    public int AddLevel = 0;
    public int UpGrade = 0;
    public int AddUnitCount = 0;

    public int UnitCount { get { return (int)(LP * 0.5f) + AddUnitCount; } }

    public float DamageRate = 1.0f;
    public float SkillDamageRate = 1.0f;
    public int Exp = 0;

    public float AttackRange = 1.5f;
    public float AttackSpeed = 1.0f;
    public float MoveSpeed = 1;
    public float SkillCoolTime = 1;

    public Skill[] Skills = new Skill[2];
    public Color[] UnitColors = new Color[5];

    public UnitData HalfData()
    {
        UnitData data = new UnitData();

        float def = GameManager.Instance.LP_def;
        float addLp = GameManager.Instance.Lp;
        data.Name = Name;

        data.AP = AP * (def + LP * addLp);
        data.SP = SP * (def + LP * addLp);
        data.HP = HP * (def + LP * addLp);
        data.LP = LP * (def + LP * addLp);

        data.DamageRate = DamageRate * (def + LP * addLp);
        data.Grade = Grade;
        data.Level = Level;
        data.AddLevel = AddLevel;

        data.AttackRange = AttackRange * 0.9f;
        data.AttackSpeed = AttackSpeed;
        data.UnitColors = UnitColors;
        data.Weapon = Weapon;
        data.MoveSpeed = MoveSpeed;
        return data;
    }


    public HeroSaveData GetSaveData()
    {
        HeroSaveData data = new HeroSaveData();
        data.Name = Name;
        data.AddLevel = AddLevel;
        data.UpGrade = UpGrade;
        data.AddUnitCount = AddUnitCount;

        return data;
    }
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
    [SerializeField]
    protected UnitColoring m_UnitColoring = null;
    [SerializeField]
    protected List<GameObject> m_WeaponObjs = new List<GameObject>();
    [SerializeField]
    protected List<GameObject> m_ArrowObjs = new List<GameObject>();

    [SerializeField]
    public UnitData UnitData = null;

    [SerializeField]
    protected UnitData m_BuffUnitData = null;

    public eUnitStateType UnitState = eUnitStateType.None;
    public bool isCanTarget { get { return UnitState != eUnitStateType.Die && UnitState != eUnitStateType.Dieing; } }

    protected List<Buff> m_Buffs = new List<Buff>();

    [SerializeField]
    protected Unit m_Target = null;

    public int TotalLevel { get { return UnitData.Level + UnitData.AddLevel; } }
    public float AP = 0;
    public float HP = 0;
    public float MaxHP = 0;

    public float DamageRate = 0;
    public float SkillDamageRate { get { return m_BuffUnitData.SP * GameManager.Instance.Sp * m_BuffUnitData.SkillDamageRate; } }

    public bool isEnemy = false;

    public AttackSkill AttackSkill = null; // 평타
    [SerializeField]
    public List<Skill> SkillList = new List<Skill>();

    public virtual void Init(UnitData data, bool enemy)
    {
        UnitData = data;
        m_Buffs.Clear();
        SetStats();
        InitSkill();
        BuffDataUpdate();

        for (int i = 0; i < m_WeaponObjs.Count; i++)
        {
            m_WeaponObjs[i].SetActive(false);
        }
        
        m_WeaponObjs[(int)data.Weapon].SetActive(true);

        m_UnitColoring.ChangeColor(data.UnitColors);

        m_RootObj.SetActive(true);
        gameObject.EnableCollider();

        UnitState = eUnitStateType.None;
        isEnemy = enemy;
        m_StateUI.SetEnemy(isEnemy);
        m_StateUI.ReSet();
    }

    private void InitSkill()
    {
        switch (UnitData.Weapon)
        {
            case eWeaponType.Bow:
            case eWeaponType.Wand:
                AttackSkill = new BowAttackSKill();
                break;
            case eWeaponType.Dagger:
                AttackSkill = new AttackSkill();
                AttackSkill.Data.Value = 0.65f;
                break;
            case eWeaponType.Sword:
            case eWeaponType.Shield:
                AttackSkill = new AttackSkill();
                break;
        }

        SkillList.Clear();
        SkillList.AddRange(UnitData.Skills);

        for(int i = 0; i < SkillList.Count; i++)
        {
            if(SkillList[i] is PassiveSkill)
                m_Buffs.Add((SkillList[i] as PassiveSkill).Buff);
        }
    }

    private void BuffDataUpdate()
    {
        m_BuffUnitData = new UnitData();
        float AP = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.AP);
        float HP = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.HP);
        float AddUnitCount = m_Buffs.GetBuffTypeToValue(eBuffType.AddUnitCount);
        float DamageRate = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.DamageRate);
        float SkillDamageRate = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.SkillDamageRate);
        float AttackRange = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.AttackRange);
        float AttackSpeed = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.AttackSpeed);
        float MoveSpeed = 1 + m_Buffs.GetBuffTypeToValue(eBuffType.MoveSpeed);
        float SkillCoolTime = m_Buffs.GetBuffTypeToValue(eBuffType.SkillCoolTime);

        m_BuffUnitData.Name = UnitData.Name;
        m_BuffUnitData.AP = UnitData.AP * AP;
        m_BuffUnitData.HP = UnitData.HP * HP;
        m_BuffUnitData.AddUnitCount = UnitData.AddUnitCount + (int)AddUnitCount;
        m_BuffUnitData.DamageRate = UnitData.DamageRate * DamageRate;
        m_BuffUnitData.SkillDamageRate = UnitData.SkillDamageRate * SkillDamageRate;
        m_BuffUnitData.AttackRange = UnitData.AttackRange * AttackRange;
        m_BuffUnitData.AttackSpeed = UnitData.AttackSpeed * AttackSpeed;
        m_BuffUnitData.MoveSpeed = UnitData.MoveSpeed * MoveSpeed;
        m_BuffUnitData.SkillCoolTime = SkillCoolTime;
    }

    private void SetStats(bool resetHP = true)
    {
        float addAP = 1f;
        float addHP = 1f;
        switch (UnitData.Weapon)
        {
            case eWeaponType.Wand:
                addAP = 1.1f;
                break;
            case eWeaponType.Sword:
                addAP = 1.1f;
                addHP = 1.1f;
                break;
            case eWeaponType.Shield:
                addHP = 1.2f;
                break;
        }

        AP = UnitData.AP * GameManager.Instance.Ap * (1f + UnitData.Level * GameManager.Instance.Level) * addAP;
        if (AP < 1) AP = 1;
        MaxHP = UnitData.HP * GameManager.Instance.Hp * (1f + UnitData.Level * GameManager.Instance.Level) * addHP;
        if (resetHP)
            HP = MaxHP;
        DamageRate = UnitData.DamageRate;
    }

    void Update()
    {
        if (!isCanTarget)
            return;

        m_Target = UnitManager.Instance.FindUnit(this);

        UnitState = eUnitStateType.None;

        if (m_Target == null)
        {
            m_Animator.Play("Idle");
        }
        else if (UnitState < eUnitStateType.Hiting)
        {
            if (InByUnitToAttackRange())
            {
                if (AttackSkill.isCoolTime && UnitState != eUnitStateType.Attacking)
                {
                    m_Animator.Play("Attack_" + UnitData.Weapon.ToString());
                    UnitState = eUnitStateType.Attacking;
                }

                if (UnitState != eUnitStateType.Attacking)
                {
                    for (int i = 0; i < SkillList.Count; i++)
                    {
                        if (SkillList[i] != null && SkillList[i].Data.SkillType == eSkillType.Active &&
                            (SkillList[i] as ActiveSkill).isCoolTime)
                            SkillList[i].Active(this, m_Target);
                    }
                }
                //Attack();
            }
            else
            {
                Move(m_BuffUnitData.MoveSpeed);
            }
        }

        AttackSkill.CoolTime -= m_BuffUnitData.AttackSpeed * Time.deltaTime;
        for (int i = 0; i < SkillList.Count; i++)
        {
            if (SkillList[i] != null)
                SkillList[i].CoolTime -= Time.deltaTime;
        }
    }

    //이동
    public virtual void Move(float speed)
    {
        if (UnitState < eUnitStateType.Hiting && UnitState != eUnitStateType.Attacking && UnitState != eUnitStateType.Skilling)
        {
            m_Animator.Play("Move");
            UnitState = eUnitStateType.Move;
            Quaternion temp = Quaternion.LookRotation(transform.position - m_Target.transform.position);
            transform.rotation = Quaternion.Euler(0, temp.eulerAngles.y, 0);

            transform.Translate(Vector3.back * Time.deltaTime * speed); //전진
        }
    }

    protected virtual bool InByUnitToAttackRange()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.forward, m_BuffUnitData.AttackRange);
        //if (Physics.Raycast(transform.position, -transform.forward, out hit, Data.AttackRange))
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Unit unit = hits[i].transform.GetComponent<Unit>();
                if (unit != null && isEnemy != unit.isEnemy)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //공격
    public virtual void Attack()
    {
        if (m_Target != null)
        {
            AttackSkill.Active(this, m_Target);
            UnitState = eUnitStateType.Attack;
            //미사일 공격을 어케하지.
            //활, 검, 창?, 탱커, 힐, 법사
        }
    }

    //피격
    public virtual void Hit(Damage damage)
    {
        if (UnitState < eUnitStateType.Hiting)
        {
            UnitState = eUnitStateType.Hit;
            //ActiveSkillOperate(damage.Unit);

            //Job.Hit(damage);
            HP -= damage.DamagePoint;
            float hp = (float)HP / (float)MaxHP;

            if(m_StateUI != null)
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

        UnitState = eUnitStateType.Dieing;
        m_StateUI.SetDisable();

        InGameUI.Instance.UnitViewListUpdate();

        DelayAction(0.5f, () =>
        {
            UnitState = eUnitStateType.Die;
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

    public void AddBuff(List<Buff> buff)
    {
        m_Buffs.AddRange(buff);
        BuffDataUpdate();
    }

    public GameObject GetArrow()
    {
        return m_ArrowObjs[(int)UnitData.Weapon];
    }

    public void PlayAnimation(string animation)
    {
        m_Animator.Play(animation);
    }

    public void LevelUp(int level)
    {
        UnitData.Level += level;
        SetStats(false);
    }
}
