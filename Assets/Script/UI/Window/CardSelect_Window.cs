using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect_Window : BackBaseWindow
{
    [SerializeField]
    private List<Card> m_CardList = new List<Card>();

    private List<Type> m_CardDatas = new List<Type>();

    private void Awake()
    {
        //카드 테이블화 작업 필요.
        m_CardDatas.Add(typeof(ReinforcementsCard));
        m_CardDatas.Add(typeof(TrainedTroopsCard));
        m_CardDatas.Add(typeof(ChargeFormationCard));
        m_CardDatas.Add(typeof(DisciplineCard));
        m_CardDatas.Add(typeof(TacticalLinkCard_Rush));
        m_CardDatas.Add(typeof(FixedFormationCard));
        m_CardDatas.Add(typeof(BattlefieldAuraCard));
        m_CardDatas.Add(typeof(BerserkOrderCard));
        m_CardDatas.Add(typeof(CostoftheHordeCard));
        m_CardDatas.Add(typeof(UnyieldingWillCard));
        m_CardDatas.Add(typeof(CombatReadyCard));
        m_CardDatas.Add(typeof(UnifiedMarchCard));
        m_CardDatas.Add(typeof(SpoilsOfWarCard));
        m_CardDatas.Add(typeof(VengefulSpiritsCard));
        m_CardDatas.Add(typeof(DefensiveInstinctCard));
        m_CardDatas.Add(typeof(LastStandCard));
        m_CardDatas.Add(typeof(BerserkerBlowCard));


        m_CardDatas.Add(typeof(StreamlinedTrainingCard));
        m_CardDatas.Add(typeof(ShopNegotiatorCard));
        m_CardDatas.Add(typeof(StageClearRewardCard));
        m_CardDatas.Add(typeof(GoldenCollectorCard));
    }

    public override void OnInit()
    {
        //WindowManager.Instance.CloseAll();
        List<CardData> datas = GetRandomCard();
        for (int i = 0; i < datas.Count; i++)
        {
            m_CardList[i].Init(datas[i]);
        }
    }

    public List<Type> PickRandomCards(int count)
    {
        UnityEngine.Random.InitState(UnityEngine.Random.Range(0, (int)(System.DateTime.Now.Ticks)));
        // 카드 개수보다 적게 요청되었는지 체크
        if (count > m_CardDatas.Count)
            throw new System.Exception("Not enough cards to pick from.");

        // 리스트 섞고 앞에서 count개 추출
        return m_CardDatas.OrderBy(c => UnityEngine.Random.value).Take(count).ToList();
    }

    public List<CardData> GetRandomCard()
    {
        List<Type> types = PickRandomCards(3);
        List<CardData> data = new List<CardData>();
        for (int i = 0; i < types.Count; i++)
        {
            data.Add((CardData)Activator.CreateInstance(types[i]));
        }

        return data;
    }

    public override void BackButtonClick()
    {
        //Close();
    }

}
