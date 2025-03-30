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

        UnitManager.Instance.InitMyUnit();
        GameStart(StageIndex);
        //���̺� ���� �ε�
        //���� ���� �ε�
        //�ʱ� ȭ�� ����
    }

    public void GameStart()
    {
        UnitManager.Instance.Restore();
        GameStart(StageIndex);
    }

    private void GameStart(int stageIndex)
    {
        UnitManager.Instance.RegisterMyUnit();
        UnitManager.Instance.InitEnemyUnit(stageIndex);

        InGameUI.Instance.InitUnitView();
    }

    //�������� �¸�
    public void GameWin()
    {
        UnitManager.Instance.Restore();
        StageIndex++;
        GameStart(StageIndex);
        //GameStart(StageIndex);
    }

    //�������� �й�
    public void GameOver()
    {
        UnitManager.Instance.Restore();
    }

    public UnitData Gacha()
    {
        return UnitRandomMachine.NewUnitData();
    }

    public void X2()
    {
        Time.timeScale = Time.timeScale == 1 ? 2 : 1;
    }
}
