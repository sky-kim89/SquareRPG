using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemLine : MonoBehaviour
{
    [SerializeField]
    private List<UIItem> m_Items = new List<UIItem>();

    public int Count { get { return m_Items.Count; } }

    public virtual void Init(List<UIData> _ItemInfos)
    {
        if (_ItemInfos != null)
        {
            gameObject.SetActive(true);
            for (int i = 0; i < m_Items.Count; i++)
            {
                if (i < _ItemInfos.Count)
                {
                    m_Items[i].Init(_ItemInfos[i]);
                }
                else
                {
                    m_Items[i].Init(null);
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
