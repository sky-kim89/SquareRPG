using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUnit : Unit
{
    public List<Unit> Units = new List<Unit>();

    public override void Init(UnitData data, bool enemy)
    {
        base.Init(data, enemy);
    }
}
