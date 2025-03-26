using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InGameUI : Singleton<InGameUI>
{
    [SerializeField]
    private SpriteAtlas UIAtlas = null;

    [SerializeField]
    private List<UnitView> m_UnitViews = new List<UnitView>();

    public void UnitViewListUpdate()
    {
        for(int i = 0; i < m_UnitViews.Count; i++)
        {
            if(UnitManager.Instance.MyHeroUniy.Count > i)
                m_UnitViews[i].Init(UnitManager.Instance.MyHeroUniy[i]);
            else
                m_UnitViews[i].Init(null);

        }
    }

    public Sprite GetSprite(string name)
    {
        return UIAtlas.GetSprite(name);
    }
}
