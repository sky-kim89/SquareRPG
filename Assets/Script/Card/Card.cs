using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardData
{
    public string Image = string.Empty;
    public string Name = string.Empty;
    public string Description = string.Empty;

    public BuffData Buff = null;
}


public class Card : MonoBehaviour
{
    [SerializeField]
    private Image m_Icon = null;
    [SerializeField]
    private Text m_Title = null;
    [SerializeField]
    private Text m_Description = null;

    private CardData m_CardData = null;
    public void Init(CardData card)
    {
        m_Icon.sprite = InGameUI.Instance.GetSprite(card.Image);
        m_Title.text = card.Name;
        m_Description.text = card.Description;
    }

    public void OnClickCardButton()
    {
        GameManager.Instance.CardBuffs.Add(m_CardData);
        WindowManager.Instance.Close(WindowIds.CardSelect_Window);
        GameManager.Instance.eGameFlowState = eGameFlowState.InBattle;
    }
}

public class ReinforcementsCard : CardData
{
    public ReinforcementsCard()
    {
        Image = "ReinforcementsCard";
        Name = "병력 증강";
        Description = string.Format("부하 최대 수치가 {0} 증가", 1.ToString());
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.AddUnitCount, 1));
    }
}
public class TrainedTroopsCard : CardData
{
    public TrainedTroopsCard()
    {
        Image = "TrainedTroopsCard";
        Name = "훈련된 병사들";
        Description = string.Format("모든 부하의 공격력이 {0}% 증가", 20.ToString());
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AP, 0.2f));
    }
}

public class ChargeFormationCard : CardData
{
    public ChargeFormationCard()
    {
        Image = "ChargeFormationCard";
        Name = "돌격대 포메이션";
        Description = string.Format("근접형 부하의 이동속도가 {0}% 증가하지만 받는 피해가 {1}% 증가", 40.ToString(), 10.ToString());
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Start;
        Buff.Apply = (unit) =>
        {
            if(unit.UnitData.Weapon == eWeaponType.Sword || unit.UnitData.Weapon == eWeaponType.Shield)
            {
                BuffData buff = new BuffData();
                Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.MoveSpeed, 0.4f));
                Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.DamageReduction, -0.1f));

                unit.AddBuff(buff);
            }
        };
    }
}

public class DisciplineCard : CardData
{
    public DisciplineCard()
    {
        Image = "DisciplineCard";
        Name = "군기 강화";
        Description = string.Format("부하의 모든 능력치가 {0} 증가", 5.ToString());
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AP, 0.05f));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.HP, 0.05f));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.MoveSpeed, 0.05f));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AttackRange, 0.05f));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AttackSpeed, 0.05f));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.DamageReduction, 0.05f));
    }
}

public class TacticalLinkCard_Rush : CardData
{
    public TacticalLinkCard_Rush()
    {
        Image = "TacticalLink:RushCard";
        Name = "전술 연계: 돌진";
        int value = 100;
        Description = string.Format("스킬 사용 시 0.5초 동안 부하가 이동속도 {0}% 증가", value);
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Skilling;
        Buff.MaxCoolTime = 0.5f;
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.MoveSpeed, 1f));
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

public class FixedFormationCard : CardData
{
    public FixedFormationCard()
    {
        Image = "FixedFormationCard";
        Name = "고정된 진형";
        int value = 10;
        Description = string.Format("모든 유닛이 넉백 면역을 얻고 최대 체력이 {0}% 증가", value);
        Buff = new BuffData();
        //Buff.BuffList.Add(eBuffType.KnockbackImmune, Value);
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.HP, 0.1f));
    }
}

public class BattlefieldAuraCard : CardData
{
    public BattlefieldAuraCard()
    {
        Image = "BattlefieldAuraCard";
        Name = "전장의 기운";
        int value = 20;
        Description = string.Format("모든 유닛이 공격속도가 {0}% 증가", value);
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.AttackSpeed, 0.2f));
    }
}

public class BerserkOrderCard : CardData
{
    public BerserkOrderCard()
    {
        Image = "BerserkOrderCard";
        Name = "광란의 명령";
        int value = 50;
        int value1 = 25;
        Description = string.Format("부하들의 공격력이 {0}%증가하지만, 받는 피해가 {1}%증가.", value, value1);
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.AP, 0.5f));
        Buff.BuffList.Add(new Buff(eTargetType.Minion, eBuffType.DamageReduction, -0.25f));
    }
}

public class CostoftheHordeCard : CardData
{
    public CostoftheHordeCard()
    {
        Image = "CostoftheHordeCard";
        Name = "무리의 대가";
        int value = 5;
        int value1 = -30;
        Description = string.Format("부하 최대 수 +{0}, 대신 캐릭터 최대 체력 {1}%", value, value1);
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.AddUnitCount, 5));
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.HP, -0.3f));
    }
}

public class UnyieldingWillCard : CardData
{
    public UnyieldingWillCard()
    {
        Image = "UnyieldingWillCard";
        Name = "불굴의 의지";
        Description = string.Format("부하가 사망 시 20% 확률로 1초 후 부활 (1회)");
        Buff = new BuffData();
        Buff.eTriggerType = eUnitStateType.Die;
        Buff.Apply = (unit) =>
        {
            if (Random.Range(0, 10) > 3)
            {
                unit.HP = unit.MaxHP;
                unit.Resurrection();
                unit.RemoveBuff(Buff);
            }
        };
    }
}
public class CombatReadyCard : CardData
{
    public CombatReadyCard()
    {
        Image = "CombatReadyCard";
        Name = "전투 준비";
        int value = 30;
        Description = string.Format("모든 히어로의 시작 체력이 {0}% 증가합니다.", value);
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.Hero, eBuffType.HP, 0.3f));
    }
}

public class UnifiedMarchCard : CardData
{
    public UnifiedMarchCard()
    {
        Image = "UnifiedMarchCard";
        Name = "단결된 움직임";
        int value = 20;
        Description = string.Format("캐릭터 및 부하 이동속도 {0}% 증가", value);
        Buff = new BuffData();
        Buff.BuffList.Add(new Buff(eTargetType.All, eBuffType.MoveSpeed, 0.2f));
    }
}