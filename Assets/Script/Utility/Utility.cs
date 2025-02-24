using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    public static void EnableCollider(this GameObject gameObject)
    {
        if (gameObject != null)
        {
            Collider[] colliders = gameObject.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public static void DisableCollider(this GameObject gameObject)
    {
        if (gameObject != null)
        {
            Collider[] colliders = gameObject.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
