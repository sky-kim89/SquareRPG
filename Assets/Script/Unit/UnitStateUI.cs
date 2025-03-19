using DG.Tweening;
using UnityEngine;

public class UnitStateUI : MonoBehaviour
{
    public GameObject m_HPObj = null;
    public GameObject m_CoolObj = null;

    public Renderer Unit = null;
    public Material EnemyMaterial = null;
    public Material AllyMaterial = null;

    public void ReSet()
    {
        m_HPObj.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        m_CoolObj.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        //m_HPObj.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void SetHP(float hp)
    {
        if (hp <= 0)
            hp = 0;
        m_HPObj.transform.DOScaleZ(hp, 0.2f);
    }

    public void SetCool(float cool)
    {
        m_CoolObj.transform.DOScaleZ(cool, 0.1f);
    }

    public void SetEnemy(bool isEnemy)
    {
        Unit.material = isEnemy ? EnemyMaterial : AllyMaterial;
        Unit.gameObject.SetActive(!isEnemy);
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }
}
