using MyProjeckt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameFlowState
{
    MainMenu,
    StageStart,
    InBattle,
    BattleResult,
    CardSelect,
    StageClear,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    //AP 가 적용되는 수치 비율
    public float Ap = 2;

    //HP 가 적용되는 수치 비율
    public float Hp = 10;

    //SP 가 적용되는 수치 비율
    public float Sp = 1;

    //LP 가 적용되는 수치 비율
    public float Lp = 0.0125f;
    //LP가 적용 되는 최소 수치
    public float LP_def = 0.25f;

    //레벨업 스텟 증가율
    public float Level = 0.1f;

    //특성의로 인한 버프 리스트
    public List<BuffData> Buffs = new List<BuffData>();
    public List<CardData> CardBuffs = new List<CardData>();

    private eGameFlowState m_eGameFlowState = eGameFlowState.MainMenu;
    public eGameFlowState eGameFlowState
    {
        get
        {
            return m_eGameFlowState;
        }

        set
        {
            switch(value)
            {
                case eGameFlowState.MainMenu:
                    Buffs.Clear();
                    //특성에 의한 버프
                    //Buffs.AddRange(MyInfoManager.Instance.BuffList);
                    break;
                case eGameFlowState.StageStart:
                    UnitManager.Instance.InitMyUnit();
                    eGameFlowState = eGameFlowState.InBattle;
                    return;
                case eGameFlowState.InBattle:
                    GameStart(StageIndex);
                    break;
                case eGameFlowState.BattleResult:
                    UnitManager.Instance.Restore();
                    StageIndex++;
                    //승리 팝업 + 히어로 별 딜/탱킹/힐 정산 window
                    break;
                case eGameFlowState.CardSelect:
                    WindowManager.Instance.Open(WindowIds.CardSelect_Window);
                    break;
                case eGameFlowState.StageClear:
                    break;
                case eGameFlowState.GameOver:
                    WindowManager.Instance.Open(WindowIds.GameOver_Window);
                    
                    break;
            }
            m_eGameFlowState = value;
        }
    }

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

        //m_eGameFlowState = eGameFlowState.MainMenu;
        eGameFlowState = eGameFlowState.StageStart;
        //테이블 관련 로드
        //유져 정보 로드
        //초기 화면 구성
    }

    public void GameStart()
    {
        eGameFlowState = eGameFlowState.StageStart;
    }

    private void GameStart(int stageIndex)
    {
        UnitManager.Instance.RegisterMyUnit();
        UnitManager.Instance.InitEnemyUnit(stageIndex);

        InGameUI.Instance.InitUnitView();
    }

    //스테이지 승리
    public void GameWin()
    {
        eGameFlowState = eGameFlowState.BattleResult;
        eGameFlowState = eGameFlowState.CardSelect;
        //GameStart(StageIndex);
        //GameStart(StageIndex);
    }

    //스테이지 패배
    public void GameOver()
    {
        UnitManager.Instance.Restore();
    }

    #region 치트성 함수
    public UnitData Gacha()
    {
        return UnitRandomMachine.NewUnitData();
    }

    public void ChangeUnit()
    {
        int index = Random.Range(0, Table.NameTables.Length);
        MyInfoManager.Instance.HeroSaveDatas.Clear();

        MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
        MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
        MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
        MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());
        MyInfoManager.Instance.HeroSaveDatas.Add(Gacha().GetSaveData());

        UnitManager.Instance.InitMyUnit();
        GameStart();
    }

    public void X2()
    {
        Time.timeScale = Time.timeScale == 1 ? 10 : 1;
    }
    #endregion
}
