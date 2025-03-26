using MyProjeckt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float Ap = 2;
    public float Hp = 10;
    public float Sp = 1;
    public float Lp = 0.0125f;

    public float Level = 0.1f;

    public float LP_def = 0.25f;

    public List<Buff> Buffs = new List<Buff>();

    public int StageIndex = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (MyInfoManager.Instance.HeroSaveDatas.Count == 0)
        {
            MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
            MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
            MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
            MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
            MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
        }
        //테이블 관련 로드
        //유져 정보 로드
        //초기 화면 구성
    }

    public void GameStart()
    {
        UnitManager.Instance.Restore();
        GameStart(StageIndex);
    }

    private void GameStart(int stageIndex)
    {
        UnitManager.Instance.InitMyUnit();
        UnitManager.Instance.InitEnemyUnit(stageIndex);

        InGameUI.Instance.UnitViewListUpdate();
    }

    public UnitData Gacha()
    {
        return UnitRandomMachine.NewUnitData();
    }
}
