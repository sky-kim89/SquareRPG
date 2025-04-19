using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Image m_Icon = null;
    [SerializeField]
    private Text m_Title = null;
    [SerializeField]
    private Text m_Description = null;

    private CardBase m_CardData = null;
    public void Init(CardBase card)
    {
        m_CardData = card;
        m_Icon.sprite = UIManager.Instance.GetSprite(card.Image);
        m_Title.text = card.Name;
        m_Description.text = card.Description;
    }

    public void OnClickCardButton()
    {
        GameManager.Instance.Cards.Add(m_CardData);
        WindowManager.Instance.Close(WindowIds.CardSelect_Window);
        GameManager.Instance.eGameFlowState = eGameFlowState.InBattle;
    }
}