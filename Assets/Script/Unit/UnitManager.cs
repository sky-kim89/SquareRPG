using MyProjeckt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<Transform> MyPoint = new List<Transform>();
    [SerializeField]
    private List<Transform> EnemyPoint = new List<Transform>();

    private List<HeroUnit> MyHeroUniy = new List<HeroUnit>();
    private List<HeroUnit> EnemyHeroUniy = new List<HeroUnit>();

    [SerializeField]
    private GameObject HeroPrefab = null;
    [SerializeField]
    private GameObject UnitPrefab = null;

    private List<Unit> UnitList = new List<Unit>();

    public Unit FindUnit(Unit unit, eFindUnitType findType = eFindUnitType.Range)
    {
        Unit target = null;

        switch (findType)
        {
            case eFindUnitType.Range:
                {
                    float dir = 100;
                    for (int i = 0; i < UnitList.Count; i++)
                    {
                        if (UnitList[i] != unit && UnitList[i].isEnemy != unit.isEnemy &&
                            UnitList[i].isCanTarget)
                        {
                            float temp = Vector3.Distance(UnitList[i].transform.position, unit.transform.position);
                            if (dir > temp)
                            {
                                target = UnitList[i];
                                dir = temp;
                            }
                        }
                    }

                    break;
                }
        }

        return target;
    }
    

    public void InitMyUnit()
    {
        for (int i = 0; i < MyInfoManager.Instance.HeroSaveDatas.Count; i++)
        {
            HeroUnit hero = ObjectPool.Instance.GetObject<HeroUnit>(HeroPrefab, transform);
            UnitData data = UnitRandomMachine.GetUnitData(MyInfoManager.Instance.HeroSaveDatas[i]);
            //hero.isEnemy = false;
            hero.Init(data, false);

            hero.transform.position = MyPoint[i].position;

            for (int j = 0; j < data.UnitCount; j++)
            {
                Unit unit = ObjectPool.Instance.GetObject<Unit>(UnitPrefab, transform);
                //unit.isEnemy = false;
                unit.transform.position = hero.transform.position + new Vector3(0.6f * (1 + (int)(j * 0.1f)), 0, (j * 0.3f) * (j % 2 == 0 ? 1 : -1));
                unit.Init(data.HalfData(), false);
                hero.Units.Add(unit);
            }
            MyHeroUniy.Add(hero);

            UnitList.Add(hero);
            UnitList.AddRange(hero.Units);
        }
    }

    public void InitEnemyUnit(int stage)
    {
        int temp = stage * stage;
        int addCount = stage / 5;
        if (addCount > 10)
            addCount = 10;
        int unitCount = stage > 5 ? 5 : stage;
        for (int i = 0; i < unitCount; i++)
        {
            HeroUnit hero = ObjectPool.Instance.GetObject<HeroUnit>(HeroPrefab, transform);
            UnitData data = UnitRandomMachine.GetUnitData((temp - i).ToString());
            data.Level = stage;
            //hero.isEnemy = true;
            hero.Init(data.HalfData(), true);

            hero.transform.position = EnemyPoint[i].position;

            for (int j = 0; j < data.UnitCount + addCount; j++)
            {
                Unit unit = ObjectPool.Instance.GetObject<Unit>(UnitPrefab, transform);
                //unit.isEnemy = true;
                unit.transform.position = hero.transform.position + new Vector3(-0.6f * (1 + (int)(j * 0.1f)), 0, (j % 10 * 0.3f) * (j % 2 == 0 ? 1 : -1));
                Debug.Log(-0.45f * (1 + (int)(j * 0.1f)));
                unit.Init(data.HalfData(), true);
                hero.Units.Add(unit);
            }

            EnemyHeroUniy.Add(hero);

            UnitList.Add(hero);
            UnitList.AddRange(hero.Units);
        }
    }

}
