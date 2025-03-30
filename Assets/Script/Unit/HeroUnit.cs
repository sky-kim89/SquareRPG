using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUnit : Unit
{
    public List<Unit> Units = new List<Unit>();

    public int TatalUnitCount { get { return Units.Count; } }
    public int LifeUnitCount { get { return Units.Count - DieCount; } }
    public int DieCount { get { return Units.FindAll((temp) => temp.UnitState == eUnitStateType.Die || temp.UnitState == eUnitStateType.Dieing).Count; } }

    [SerializeField]
    public bool IsAllDie { get { return LifeUnitCount == 0 && base.IsDie; } }
    public override void Init(UnitData data, bool enemy)
    {
        Units.Clear();
        base.Init(data, enemy);
    }

    public override void Hit(Damage damage)
    {
        base.Hit(damage);
    }

    public override void Die(Unit unit)
    {
        base.Die(unit);
    }
}
