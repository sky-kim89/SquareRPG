using MyProjeckt;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum eFindUnitType
{
    Range //거리순
    //Hp 낮은
    //히어로 우선
    //부하 우선
}
public class UnitManager : Singleton<UnitManager>
{
    [SerializeField]
    private List<Transform> m_MyPoint = new List<Transform>();
    [SerializeField]
    private List<Transform> m_EnemyPoint = new List<Transform>();

    public List<HeroUnit> MyHeroUniy = new List<HeroUnit>();
    public List<HeroUnit> EnemyHeroUniy = new List<HeroUnit>();

    private List<UnitData> MyUnitData = new List<UnitData>();

    [SerializeField]
    private GameObject m_HeroPrefab = null;
    [SerializeField]
    private GameObject m_UnitPrefab = null;

    private List<Unit> m_UnitList = new List<Unit>();

    public Unit FindUnit(Unit unit, eFindUnitType findType = eFindUnitType.Range)
    {
        Unit target = null;
        switch (findType)
        {
            case eFindUnitType.Range:
                {
                    HeroUnit targetHero = FindHeroToRange(unit, unit.isEnemy ? MyHeroUniy : EnemyHeroUniy);
                    target = targetHero;
                    if (targetHero != null && targetHero.LifeUnitCount > 0)
                    {
                        Unit temp = FindUnitToRange(unit, targetHero.Units, targetHero);
                        if(temp != null)
                            target = temp;
                    }
                    

                    break;
                }
        }

        return target;
    }

    public HeroUnit FindHeroToRange(Unit unit, List<HeroUnit> unitList)
    {
        HeroUnit target = null;
        float dir = 100;
        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i] != unit && unitList[i].isEnemy != unit.isEnemy &&
                !unitList[i].IsAllDie)
            {
                //Vector3.SqrMagnitude(unitList[i].transform.position - unit.transform.position)
                float temp = Vector3.Distance(unitList[i].transform.position, unit.transform.position);
                if (dir > temp)
                {
                    target = unitList[i];
                    dir = temp;
                }
            }
        }

        return target;
    }

    public Unit FindUnitToRange(Unit unit, List<Unit> unitList, HeroUnit hero = null)
    {
        Unit target = null;
        float dir = 100;
        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i] != unit && unitList[i].isEnemy != unit.isEnemy &&
                !unitList[i].IsDie)
            {
                //Vector3.SqrMagnitude(unitList[i].transform.position - unit.transform.position)
                float temp = Vector3.Distance(unitList[i].transform.position, unit.transform.position);
                if (dir > temp)
                {
                    target = unitList[i];
                    dir = temp;
                }
            }
        }

        if (hero != null && !hero.IsDie && dir > Vector3.Distance(hero.transform.position, unit.transform.position))
            target = hero;

        return target;
    }


    public void InitMyUnit()
    {
        MyUnitData.Clear();
        for (int i = 0; i < MyInfoManager.Instance.HeroSaveDatas.Count; i++)
        {
            UnitData data = UnitRandomMachine.GetUnitData(MyInfoManager.Instance.HeroSaveDatas[i]);
            MyUnitData.Add(data);
        }
    }
    private HeroUnit CreateHero(UnitData data, int point)
    {
        HeroUnit hero = ObjectPool.Instance.GetObject<HeroUnit>(m_HeroPrefab, transform);
        //hero.isEnemy = false;
        hero.Init(data, false);
        hero.SetStateCoolBack(eUnitStateType.Die, EndGameCheck);
        hero.transform.position = m_MyPoint[point].position;

        for (int j = 0; j < data.UnitCount; j++)
        {
            Unit unit = ObjectPool.Instance.GetObject<Unit>(m_UnitPrefab, transform);
            //unit.isEnemy = false;
            unit.transform.position = hero.transform.position + new Vector3(0.8f * (1 + (int)(j * 0.1f)), 0, (j % 10 * 0.4f) * (j % 2 == 0 ? 1 : -1));
            unit.Init(data.HalfData(), false);
            unit.SetStateCoolBack(eUnitStateType.Die, EndGameCheck);
            hero.Units.Add(unit);
        }

        return hero;
    }

    public void InitEnemyUnit(int stage)
    {
        EnemyHeroUniy.Clear();

        int temp = stage * stage;
        int addCount = stage / 5;
        if (addCount > 10)
            addCount = 10;
        int unitCount = stage > 5 ? 5 : stage;
        for (int i = 0; i < unitCount; i++)
        {
            HeroUnit hero = ObjectPool.Instance.GetObject<HeroUnit>(m_HeroPrefab, transform);
            UnitData data = UnitRandomMachine.GetUnitData((temp - i).ToString());
            data.Level = stage;
            //hero.isEnemy = true;
            hero.Init(data.HalfData(), true);
            hero.SetStateCoolBack(eUnitStateType.Die, EndGameCheck);

            hero.transform.position = m_EnemyPoint[i].position;

            for (int j = 0; j < data.UnitCount + addCount; j++)
            {
                Unit unit = ObjectPool.Instance.GetObject<Unit>(m_UnitPrefab, transform);
                //unit.isEnemy = true;
                unit.transform.position = hero.transform.position + new Vector3(-0.8f * (1 + (int)(j * 0.1f)), 0, (j % 10 * 0.4f) * (j % 2 == 0 ? 1 : -1));
                unit.Init(data.HalfData(), true);
                unit.SetStateCoolBack(eUnitStateType.Die, EndGameCheck);
                hero.Units.Add(unit);
            }

            EnemyHeroUniy.Add(hero);

            m_UnitList.Add(hero);
            m_UnitList.AddRange(hero.Units);
        }
        
        int layerMask = LayerMask.NameToLayer("Enemy");
        for (int i = 0; i < EnemyHeroUniy.Count; i++)
        {
            ChangeLayerRecursively(EnemyHeroUniy[i].gameObject, layerMask);
            for(int j = 0; j < EnemyHeroUniy[i].Units.Count; j++)
            {
                ChangeLayerRecursively(EnemyHeroUniy[i].Units[j].gameObject, layerMask);
            }
        }
    }
    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }

    public void RegisterMyUnit()
    {
        MyHeroUniy.Clear();
        for (int i = 0; i < MyUnitData.Count; i++)
        {
            HeroUnit hero = CreateHero(MyUnitData[i], i);
            MyHeroUniy.Add(hero);

            m_UnitList.Add(hero);
            m_UnitList.AddRange(hero.Units);
        }

        int layerMask = LayerMask.NameToLayer("Unit");
        for (int i = 0; i < MyHeroUniy.Count; i++)
        {
            ChangeLayerRecursively(MyHeroUniy[i].gameObject, layerMask);
            for (int j = 0; j < MyHeroUniy[i].Units.Count; j++)
            {
                ChangeLayerRecursively(MyHeroUniy[i].Units[j].gameObject, layerMask);
            }
        }
    }

    public void Restore()
    {
        for(int i = 0; i < MyUnitData.Count; i++)
        {
            MyUnitData[i].Restore();
        }

        for (int i = 0; i < m_UnitList.Count; i++)
        {
            ObjectPool.Instance.Restore(m_UnitList[i].gameObject);
        }

        m_UnitList.Clear();

        SkillManager.Instance.Restore();
    }


    private void EndGameCheck()
    {
        bool isdie = true;
        for (int i = 0; i < EnemyHeroUniy.Count; i++)
        {
            isdie &= EnemyHeroUniy[i].IsAllDie;
        }

        if (isdie)
            GameManager.Instance.GameWin();

        isdie = true;
        for (int i = 0; i < MyHeroUniy.Count; i++)
        {
            isdie &= MyHeroUniy[i].IsAllDie;
        }

        if (isdie)
            GameManager.Instance.GameOver();
    }

}
