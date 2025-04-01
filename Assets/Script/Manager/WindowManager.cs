using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum WindowIds
{
    UnitInfo_Window,
}

public class WindowManager : MonoBehaviour
{
    static WindowManager instance;
    public static WindowManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private GameObject m_WindowRoot;
    [SerializeField]
    private GameObject m_Background;
    public BackBaseWindow Current
    {
        get
        {
            if (m_BackWindowList.Count == 0)
                return null;
            else
                return m_BackWindowList[m_BackWindowList.Count - 1];
        }
    }

    private List<BackBaseWindow> m_BackWindowList = new List<BackBaseWindow>();

    private Dictionary<WindowIds, BaseWindow> m_WindowList = new Dictionary<WindowIds, BaseWindow>();

    public int WindowCount { get { return m_BackWindowList.Count; } }

    private void Awake()
    {
        instance = this;
    }

    public int GetPopupCount()
    {
        return m_BackWindowList.Count;
    }

    void BackgroundSetting(bool isClose)
    {
        if (0 >= m_BackWindowList.Count)
        {
            m_Background.SetActive(false);
        }
        else
        {
            BackBaseWindow backWindow = m_BackWindowList[m_BackWindowList.Count - 1];
            if (isClose)
            {
                if (m_BackWindowList.Count == 1)
                    m_Background.transform.SetSiblingIndex(0);
                else
                    m_Background.transform.SetSiblingIndex(backWindow.transform.GetSiblingIndex());
            }
            else
                m_Background.transform.SetSiblingIndex(transform.childCount - 2);
            m_Background.SetActive(true);

            if (null == backWindow) return;
        }
    }

    public BaseWindow GetWindow(WindowIds id)
    {
        if (null == m_WindowList)
            return null;

        if (!m_WindowList.ContainsKey(id))
            return null;

        return m_WindowList[id];
    }

    public T GetWindow<T>(WindowIds id) where T : class
    {
        BaseWindow win = GetWindow(id);
        if (null != win)
            return win.GetComponent<T>();

        return default(T);
    }

    private GameObject Load(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        if (prefab != null)
        {
            //Remark
            Transform parent = m_WindowRoot.transform;
            GameObject go = GameObject.Instantiate(prefab.gameObject, parent);

            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);

            return go;
        }
        return null;
    }

    public T Open<T>(WindowIds id, bool refresh = false, bool back = false) where T : class
    {
        GameObject go = null;
        BaseWindow win = GetWindow(id);

        if (null == win)
        {
            go = Load(string.Format("PopupUI/{0}", id.ToString()));
            if (null == go) return null;

            win = go.GetComponent<BaseWindow>();
            if (null == win) return null;

            if (m_WindowList.ContainsKey(id))
                m_WindowList[id] = win;
            else
                m_WindowList.Add(id, win);
        }


        RectTransform rt = win.GetComponent<RectTransform>();
        rt.SetAsLastSibling();
        win.gameObject.SetActive(true);
        //SoundManager.Instance.PlayEffect("popup");

        if (win is BackBaseWindow)
        {
            m_BackWindowList.Remove(win as BackBaseWindow);

            int depth = 0;
            if (m_BackWindowList.Count > 0)
                depth = m_BackWindowList.Max(x => x.Depth);

            int oldDepth = win.Depth;
            win.Depth = depth + 10;

            m_BackWindowList.Add(win as BackBaseWindow);
        }
        if (!refresh)
            win.OnInit();

        BackgroundSetting(false);

        m_Background.SetActive(!back);

        return win as T;
    }

    public void RefreshWin(WindowIds id)
    {
        Open<BaseWindow>(id, true);
    }
    
    public void Open(WindowIds id, bool back = false)
    {
        Open<BaseWindow>(id, back : back);
    }

    public void Close(WindowIds id)
    {
        BaseWindow win = GetWindow(id);
        Close(win);
    }

    public void Close(BaseWindow win)
    {
        if (null == win) return;
        win.gameObject.SetActive(false);
        //SoundManager.Instance.PlayEffect("popup");

        if (win is BackBaseWindow)
            m_BackWindowList.Remove(win as BackBaseWindow);

        BackgroundSetting(true);

    }

    public void Close()
    {
        Close(m_BackWindowList[m_BackWindowList.Count - 1]);
    }

    public void CloseAll()
    {
        var IE = m_WindowList.GetEnumerator();
        while (IE.MoveNext())
        {
            BaseWindow win = IE.Current.Value;
            if (null == win) continue;

            if (win && win.gameObject && win.gameObject.activeSelf)
            {
                win.gameObject.SetActive(false);

                if (win is BackBaseWindow)
                    m_BackWindowList.Remove(win as BackBaseWindow);
            }
        }

        BackgroundSetting(true);
    }

    public bool IsActive(WindowIds id)
    {
        BaseWindow win = GetWindow(id);
        if (null != win)
            return win.gameObject.activeSelf;

        return false;
    }

    bool isPause = false;
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            isPause = true;
        }
        else if (isPause)
        {
        }
    }
    void Update()
    {
        //#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (0 < m_BackWindowList.Count)
            {
                bool isLoop = true;
                while (isLoop)
                {
                    int idx = m_BackWindowList.Count - 1;
                    if (idx < 0) break;

                    BackBaseWindow win = m_BackWindowList[idx];
                    if (null != win && win.gameObject && win.gameObject.activeSelf)
                    {
                        win.BackButtonClick();
                        isLoop = false;
                        break;
                    }
                    else
                    {
                        m_BackWindowList.RemoveAt(idx);
                    }
                }
            }
        }
    }
}