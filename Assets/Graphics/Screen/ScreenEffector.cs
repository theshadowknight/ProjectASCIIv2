
using UnityEngine;

[ExecuteInEditMode]
public class ScreenEffector : MonoBehaviour
{
    public Material mat;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
