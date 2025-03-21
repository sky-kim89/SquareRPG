using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPool : MonoBehaviour
{
    public float DelayTime = 1.0f;

    private void OnEnable()
    {
        Invoke("Restore", DelayTime);
    }

    private void Restore()
    {
        ObjectPool.Instance.Restore(gameObject);
    }
}
