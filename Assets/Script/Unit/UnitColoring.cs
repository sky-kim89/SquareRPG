using UnityEngine;

public class UnitColoring : MonoBehaviour
{
    public Renderer Boby = null;
    public Renderer HandL = null;
    public Renderer HandR = null;
    public Renderer Head = null;
    public Renderer Hair = null;
    public Renderer EyeL = null;
    public Renderer EyeR = null;

    public void ChangeColor(Color[] colors)
    {
        Boby.material = ObjectPool.Instance.GetMaterials(colors[0]);
        HandL.material = ObjectPool.Instance.GetMaterials(colors[0]);
        HandR.material = ObjectPool.Instance.GetMaterials(colors[0]);
        Head.material = ObjectPool.Instance.GetMaterials(colors[1]);
        Hair.material = ObjectPool.Instance.GetMaterials(colors[2]);
        EyeL.material = ObjectPool.Instance.GetMaterials(colors[3]);
        EyeR.material = ObjectPool.Instance.GetMaterials(colors[4]);
    }
}