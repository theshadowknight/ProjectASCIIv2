using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class test1 : MonoBehaviour
{

    public float scanlineIntensity = 100;
    public int scanlineWidth = 1;
    //  public Color scanlineColor = Color.black;
    public float offset = 0;
    //public bool tVBulge = true;
    //public Material material_Displacement;
    public Material material_Scanlines;
    public bool move;
    public float speedadd;
    void Awake()
    {
        material_Scanlines = new Material(Shader.Find("Hidden/Scanlines"));
        if (move)
        {
            // StartCoroutine("v");
        }
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }
    IEnumerator v()
    {
        while (move)
        {
            offset += speedadd;
            if (offset >= 400)
            {
                offset = 0;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void Update()
    {
        /*  offset += speedadd;
          if (offset >= 400)
          {
              offset = 0;
          }*/
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        offset += speedadd;
        if (offset >= scanlineWidth * 2)
        {
            offset = 0;
        }
        material_Scanlines.SetFloat("_Intensity", scanlineIntensity * 0.01f);
        material_Scanlines.SetFloat("_ValueX", scanlineWidth);
        material_Scanlines.SetFloat("_Offset", offset);

        Graphics.Blit(source, destination, material_Scanlines);

    }
}
