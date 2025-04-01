using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class BaseWindow : MonoBehaviour
{
    public int Depth { set; get; }

    public Action OpenCall { get; set; }
    public Action OpenAinCall { get; set; }
    public Action CloseCall { get; set; }

    public abstract void OnInit();

    protected Animator m_Animator = null;
    protected Animator Animator 
    { 
        get 
        {
            if (m_Animator == null)
                m_Animator = GetComponent<Animator>();
            return m_Animator;
        }
    }

    protected virtual void OnEnable()
    {
        if (Animator == null)
        {
            if (OpenCall != null)
                OpenCall();
            OpenCall = null;
        }
        else
        {
            Animator.Play("Opne");
        }
    }

    protected virtual void OnDisable()
    {
        if (CloseCall != null)
            CloseCall();
        CloseCall = null;
    }

    public virtual void AniOpneCall()
    {
        if (OpenCall != null)
            OpenCall();
        OpenCall = null;
    }

    public virtual void AniCloseCall()
    {
        WindowManager.Instance.Close(this);
    }

    public virtual void Close(Action closeCall = null)
    {
        if(closeCall != null)
            CloseCall = closeCall;

        if (Animator == null)
        {
            WindowManager.Instance.Close(this);
        }
        else
        {
            //고민 필요
            Animator.Play("Close");
            WindowManager.Instance.Close(this);
        }
    }

    public virtual void OpenAniEnd()
    {
        if (OpenAinCall != null)
            OpenAinCall();
        OpenAinCall = null;
    }
}

public abstract class BackBaseWindow : BaseWindow
{
   public abstract void BackButtonClick();
}