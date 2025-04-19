using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShopUnitView : MonoBehaviour
{
    [SerializeField]
    protected Image m_Boby = null;
    [SerializeField]
    protected Image m_Head = null;
    [SerializeField]
    protected Image m_Hair = null;
    [SerializeField]
    protected Image m_EyeL = null;
    [SerializeField]
    protected Image m_EyeR = null;

    [SerializeField]
    protected Image m_Grade = null;

    [SerializeField]
    protected Image m_Jop = null;

    [SerializeField]
    protected bool m_IsShop = false;

    [SerializeField]
    protected Text m_Gold = null;

    public UnitData m_Data = null;

    public void Init(UnitData data)
    {
        if (data != null)
        {
            gameObject.SetActive(true);
            m_Data = data;
            UIUpdate();
            m_Gold.text = data.GetRecruitCost().ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void UIUpdate()
    {
        m_Boby.color = m_Data.UnitColors[0];
        m_Head.color = m_Data.UnitColors[1];
        m_Hair.color = m_Data.UnitColors[2];
        m_EyeR.color = m_Data.UnitColors[3];
        m_EyeL.color = m_Data.UnitColors[4];

        m_Grade.sprite = UIManager.Instance.GetSprite("Grade_" + (int)m_Data.Grade);

        m_Jop.sprite = UIManager.Instance.GetSprite(m_Data.Weapon.ToString());
    }

    public void OnClickUnitInfoOpenButton()
    {
        UnitShop_Window win = WindowManager.Instance.GetWindow<UnitShop_Window>(WindowIds.UnitShop_Window);
        win.ViewUnitData(m_Data, m_IsShop);
    }
}
