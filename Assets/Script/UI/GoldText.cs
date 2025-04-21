using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    private static List<GoldText> m_GoldTexts = new List<GoldText>();
    private Text m_GoldText = null;

    public void Start()
    {
        m_GoldText = GetComponentInChildren<Text>();
        m_GoldTexts.Add(this);
        UIUpdate();
    }

    public static void GoldUIUpdate()
    {
        for(int i = 0; i < m_GoldTexts.Count; i++)
        {
            m_GoldTexts[i].UIUpdate();
        }
    }
    public void UIUpdate()
    {
        m_GoldText.text = EconomyManager.Instance.Gold.ToString();
    }
}
