using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UICardItem : UIItem
{
    [SerializeField]
    private Image m_Icon = null;

    [SerializeField]
    private CardData m_CardData = null;

    public override void Init(UIData data)
    {
        m_CardData = (CardData)data;
        if (data != null)
        {
            m_Icon.sprite = UIManager.Instance.GetSprite(data.Image);
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        UnitPlacement_Window win = WindowManager.Instance.GetWindow<UnitPlacement_Window>(WindowIds.UnitPlacement_Window);
        win.OnClickCard(m_CardData);
        //선택된 카드 정보를 볼 수 있게 해주기.
    }
}
