using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUnit : Unit
{
    public List<Unit> Units = new List<Unit>();

    public int TatalUnitCount { get { return Units.Count; } }
    public int LifeUnitCount { get { return UnitData.UnitCount + UnitData.AddUnitCount; } }
    public override void Init(UnitData data, bool enemy)
    {
        base.Init(data, enemy);
    }

    public virtual void UnitDie()
    {

    }
}
