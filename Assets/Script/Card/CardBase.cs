using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public abstract class CardBase
{
    public string Image = string.Empty;
    public string Name = string.Empty;
    public string Description = string.Empty;
    public abstract void Apply();
}

[System.Serializable]
public class Card_BuffData : CardBase
{
    public BuffData Buff = null;
    public float Value = 0;

    public override void Apply()
    {
        GameManager.Instance.Buffs.Add(Buff);
    }
}

public class Card_EconomyData : CardBase
{
    public eEconomyCardType EconomyData = eEconomyCardType.AddGold;
    public float Value = 0;
    public override void Apply()
    {
        EconomyManager.Instance.AddEconomyData(EconomyData, Value);
    }
}

public class GoldenCollectorCard : Card_EconomyData
{
    public GoldenCollectorCard()
    {
        Image = "GoldenCollectorCard";
        Name = "황금 수집가";
        Value = 0.1f;
        Description = string.Format("골드 획득 시 추가로 {0}% 더 얻음", Value * 100);
        EconomyData = eEconomyCardType.GoldGainBonus;
    }
}

public class StageClearRewardCard : Card_EconomyData
{
    public StageClearRewardCard()
    {
        Image = "StageClearRewardCard";
        Name = "전투 보너스";
        Value = 0.2f;
        Description = string.Format("스테이지 클리어 시 추가 골드 획득량 {0}% 증가", Value * 100);
        EconomyData = eEconomyCardType.StageClearGold;
    }
}

public class ShopNegotiatorCard : Card_EconomyData
{
    public ShopNegotiatorCard()
    {
        Image = "ShopNegotiatorCard";
        Name = "상점 협상가";
        Value = 0.1f;
        Description = string.Format("상점 구매 비용 {0}% 감소", Value * 100);
        EconomyData = eEconomyCardType.ShopDiscount;
    }
}

public class StreamlinedTrainingCard : Card_EconomyData
{
    public StreamlinedTrainingCard()
    {
        Image = "StreamlinedTrainingCard";
        Name = "손쉬운 훈련";
        Value = 0.1f;
        Description = string.Format("업그레이드 비용이 {0}% 감소합니다.", Value * 100);
        EconomyData = eEconomyCardType.UpgradeCostDiscount;
    }
}

public class ReinforcementsCard : Card_BuffData
{
    public ReinforcementsCard()
    {
        Image = "ReinforcementsCard";
        Name = "병력 증강";
        Value = 1;
        Description = string.Format("부하 최대 수치가 {0} 증가", Value);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.AddUnitCount, Value));
    }
}
public class TrainedTroopsCard : Card_BuffData
{
    public TrainedTroopsCard()
    {
        Image = "TrainedTroopsCard";
        Name = "훈련된 병사들";
        Value = 0.2f;
        Description = string.Format("모든 부하의 공격력이 {0}% 증가", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AP, Value));
    }
}

public class ChargeFormationCard : Card_BuffData
{
    public ChargeFormationCard()
    {
        Image = "ChargeFormationCard";
        Name = "돌격대 포메이션";
        Value = 0.1f;
        Description = string.Format("근접형 부하의 이동속도가 {0}% 증가하지만 받는 피해가 {1}% 증가", Value * 600, Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.Command = (unit) =>
        {
            //Buff에 타겟에 대한 정보 추가하기
            if (unit.UnitData.Weapon == eWeaponType.Sword || unit.UnitData.Weapon == eWeaponType.Shield || unit.UnitData.Weapon == eWeaponType.Dagger)
            {
                BuffData buff = new BuffData();
                Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.MoveSpeed, Value * 4));
                Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.DamageReduction, -Value));

                unit.AddBuff(buff);
            }
        };
    }
}

public class DisciplineCard : Card_BuffData
{
    public DisciplineCard()
    {
        Image = "DisciplineCard";
        Name = "군기 강화";
        Value = 0.05f;
        Description = string.Format("부하의 모든 능력치가 {0} 증가", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AP, Value));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.HP, Value));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.MoveSpeed, Value));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AttackRange, Value));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AttackSpeed, Value));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.DamageReduction, Value));
    }
}

public class TacticalLinkCard_Rush : Card_BuffData
{
    public TacticalLinkCard_Rush()
    {
        Image = "TacticalLinkRushCard";
        Name = "전술 연계: 돌진";
        Value = 1.5f;
        Description = string.Format("이동 시작 시 2초 동안 부하가 이동속도 {0}% 증가", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Move;
        Buff.MaxDuration = 2f;
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.MoveSpeed, Value));
    }
}

public class FixedFormationCard : Card_BuffData
{
    public FixedFormationCard()
    {
        Image = "FixedFormationCard";
        Name = "고정된 진형";
        Value = 0.05f;
        Description = string.Format("모든 유닛이 넉백 면역을 얻고 최대 체력이 {0}% 증가", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        //Buff.BuffList.Add(eBuffType.KnockbackImmune, Value);
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.HP, Value));
    }
}

public class BattlefieldAuraCard : Card_BuffData
{
    public BattlefieldAuraCard()
    {
        Image = "BattlefieldAuraCard";
        Name = "전장의 기운";
        Value = 0.1f;
        Description = string.Format("모든 유닛이 공격속도가 {0}% 증가", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.AttackSpeed, Value));
    }
}

public class BerserkOrderCard : Card_BuffData
{
    public BerserkOrderCard()
    {
        Image = "BerserkOrderCard";
        Name = "광란의 명령";
        Value = 0.25f;
        Description = string.Format("부하들의 공격력이 {0}%증가하지만, 받는 피해가 {1}%증가.", Value * 300, Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AP, Value*2));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.DamageReduction, -Value));
    }
}

public class CostoftheHordeCard : Card_BuffData
{
    public CostoftheHordeCard()
    {
        Image = "CostoftheHordeCard";
        Name = "무리의 대가";
        Value = 0.25f;
        Description = string.Format("부하 최대 수 +{0}, 대신 영웅 최대 체력 {1}%", Value * 20, Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.AddUnitCount, Value * 20));
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.HP, -Value));
    }
}

public class UnyieldingWillCard : Card_BuffData
{
    public UnyieldingWillCard()
    {
        Image = "UnyieldingWillCard";
        Name = "불굴의 의지";
        Value = 0.2f;
        Description = string.Format("부하가 사망 시 {0}% 확률로 1초 후 부활 (1회)", Value * 100);
        Buff = new BuffData();
        Buff.BuffName = "UnyieldingWillCard";
        Buff.eTriggerType = eUnitStateType.Die;
        Buff.MaxStack = 0;
        Buff.Command = (unit) =>
        {
            if (Random.Range(0, 10) < (Value * 10))
            {
                unit.HP = unit.MaxHP;
                unit.Resurrection();
                unit.RemoveBuff(Buff);
            }
        };
    }
}
public class CombatReadyCard : Card_BuffData
{
    public CombatReadyCard()
    {
        Image = "CombatReadyCard";
        Name = "전투 준비";
        Value = 0.1f;
        Description = string.Format("모든 영웅의 시작 체력이 {0}% 증가합니다.", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.HP, Value));
    }
}

public class UnifiedMarchCard : Card_BuffData
{
    public UnifiedMarchCard()
    {
        Image = "UnifiedMarchCard";
        Name = "단결된 움직임";
        Value = 0.2f;
        Description = string.Format("영웅 및 부하 이동속도 {0}% 증가", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.MoveSpeed, Value));
    }
}
public class SpoilsOfWarCard : Card_BuffData
{
    public SpoilsOfWarCard()
    {
        Image = "SpoilsOfWarCard";
        Name = "전장의 보상";
        Value = 2;
        Description = string.Format("영웅가 적 처치 시 골드 {0}를 획득합니다.", Value);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Kill;
        Buff.MaxStack = 0;
        Buff.Command = (unit) =>
        {
            if(unit.UnitType == eTargetType.Hero)
            {
                EconomyManager.Instance.AddGold((int)Value);
            }
        };
    }
}

public class VengefulSpiritsCard : Card_BuffData
{
    public VengefulSpiritsCard()
    {
        Image = "VengefulSpiritsCard";
        Name = "복수의 망령";
        Value = 0.05f;
        Description = string.Format("부하가 사망할 때마다 영웅의 공격력이 {0}% 증가합니다.", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Die;
        Buff.Command = (unit) =>
        {
            if (unit.UnitType == eTargetType.Minion)
            {
                //추후 테이블화 하면서 BuffID 부여 하고 중첩 시 스텍 증가로 수정 진행 예정
                BuffData buff = new BuffData();
                buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.AP, Value));
                unit.Hero.AddBuff(buff);
            }
        };
    }
}

public class DefensiveInstinctCard : Card_BuffData
{
    public DefensiveInstinctCard()
    {
        Image = "DefensiveInstinctCard";
        Name = "방어 본능";
        Value = 0.5f;
        Description = string.Format("피격 시 3초간 이동속도가 {0}% 증가합니다.", Value * 100);
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.MoveSpeed, Value));
        Buff.eTriggerType = eUnitStateType.Hit;
        Buff.MaxDuration = 3;
    }
}
public class LastStandCard : Card_BuffData
{
    public LastStandCard()
    {
        Image = "LastStandCard";
        Name = "최후의 항전";
        Value = 0.3f;
        Description = string.Format("체력 30% 이하일 때 받는 피해 {0}% 감소", Value * 100);
        Buff = new BuffData();
        Buff.BuffName = "LastStandCard";
        Buff.eTriggerType = eUnitStateType.Hit;
        Buff.MaxStack = 0;
        Buff.Command = (unit) =>
        {
            if (unit.GetHpRatio <= 0.3f)
            {
                unit.RemoveBuff(Buff);
                //추후 테이블화 하면서 BuffID 부여 하고 중첩 시 스텍 증가로 수정 진행 예정
                BuffData buff = new BuffData();
                buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.DamageReduction, Value));
                unit.AddBuff(buff);
            }
        };
    }
}

public class BerserkerBlowCard : Card_BuffData
{
    public BerserkerBlowCard()
    {
        Image = "BerserkerBlowCard";
        Name = "광전사의 일격";
        Value = 0.5f;
        Description = string.Format("공격 시 10% 확률로 공격속도가 {0}% 증가합니다. (3초)", Value * 100);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Attack;
        Buff.MaxStack = 0;
        Buff.Command = (unit) =>
        {
            if (Random.Range(0, 20) < 2)
            {
                //추후 테이블화 하면서 BuffID 부여 하고 중첩 시 스텍 증가로 수정 진행 예정
                BuffData buff = new BuffData();
                buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.AttackSpeed, Value));
                buff.MaxDuration = 3;
                unit.AddBuff(buff);
            }
        };
    }
}


//public class TacticalRetreatCard : CardData
//{
//    public TacticalRetreatCard()
//    {
//        Image = "TacticalRetreatCard";
//        Name = "전략적 퇴각";
//        Description = "부하가 사망할 때, 주변 부하를 {0}% 확률로 체력 {1}% 회복시킵니다.";
//        int value1 = 30;
//        int value2 = 20;
//        Buff = new BuffData();
//        Buff.BuffList.Add(eBuffType.ChanceHealOnDeath, value1);
//    }
//}
