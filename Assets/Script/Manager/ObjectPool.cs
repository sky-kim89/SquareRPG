using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public sealed class ObjectPool : Singleton<ObjectPool>
{
    //***********
    //Variable
    //***********
    private Dictionary<GameObject, List<GameObject>> objectPools = new Dictionary<GameObject, List<GameObject>>();
    [SerializeField]
    private Dictionary<Color, Material> materials = new Dictionary<Color, Material>();

    private List<GameObject> tempPool;

    int count = 0;
    public Material GetMaterials(Color color)
    {
        if (!materials.ContainsKey(color))
        {
            Material mat = new Material(Shader.Find("Specular"));
            mat.color = color;
            materials.Add(color, mat);
        }
        return materials[color];
    }

    //********
    //풀 생성
    //********
    public bool CreatePool(GameObject objToPool, int initialPoolSize)
    {
        if (objToPool == null)
            return false;

        if (objectPools.ContainsKey(objToPool))
        {
            return false;
        }
        else
        {
            List<GameObject> nPool = new List<GameObject>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject nObj = GameObject.Instantiate(objToPool, Vector3.zero, Quaternion.identity) as GameObject;

                nObj.SetActive(false);

                nObj.transform.parent = transform;

                nPool.Add(nObj);
            }

            objectPools.Add(objToPool, nPool);

            return true;
        }
    }

    //********
    //풀 사용
    //********
    public GameObject GetObject(GameObject objToPool, Transform parent)
    {

        if (objectPools.ContainsKey(objToPool) == false)
        {
            //if (CreatePool(objToPool, 1))
            //    tempPool = objectPools[objToPool];
            //else
            //    return null; //오브젝트풀을 생성 못함!
            CreatePool(objToPool, 1);
        }

        tempPool = objectPools[objToPool];

        for (int i = 0; i < tempPool.Count; i++)
        {
            if (tempPool[i] != null)
            {
                if (tempPool[i].activeSelf == false)
                {
                    tempPool[i].transform.parent = parent;
                    tempPool[i].transform.localPosition = Vector3.zero;
                    tempPool[i].SetActive(true);
                    return tempPool[i];
                }
            }
            else
            {
                tempPool.Remove(null);
            }
        }

        GameObject nObj = GameObject.Instantiate(objToPool, Vector3.zero, Quaternion.identity) as GameObject;

        nObj.transform.parent = parent;
        nObj.transform.localPosition = Vector3.zero;
        nObj.SetActive(true);
        tempPool.Add(nObj);

        //Debug.LogError(nObj.name + " : " + nObj.transform.localScale);
        return nObj;
    }

    public T GetObject<T>(GameObject objToPool, Transform parent)
    {
        return GetObject(objToPool, parent).GetComponent<T>();
    }

    public void Restore(GameObject objToPool)
    {
        objToPool.SetActive(false);
        objToPool.transform.parent = transform;
        objToPool.transform.localScale = Vector3.one;
    }


    //***************
    //오브젝트 초기화
    //***************
    public void Restore_Obj(GameObject objToPool)
    {
        if (objectPools.ContainsKey(objToPool) == false)
            return;

        List<GameObject> listobj = objectPools[objToPool];

        for (int i = 0; i < listobj.Count; i++)
        {
            listobj[i].SetActive(false);
            listobj[i].transform.parent = transform;
        }
    }
}
