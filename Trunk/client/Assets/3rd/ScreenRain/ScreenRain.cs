using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRain : MonoBehaviour
{
    [Range(0,1)]
    public float blend = 1;

	private Material mtrl = null;

    private int srcTexPropId = 0;
    private int blendPropId = 0;

    private void Awake()
    {
        mtrl = new Material(Shader.Find("Hidden/ScreenRain"));

        srcTexPropId = Shader.PropertyToID("_SrcTex");
        blendPropId = Shader.PropertyToID("_Blend");
    }

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(mtrl == null || mtrl.shader == null || !mtrl.shader.isSupported)
        {
            enabled = false;
            return;
        }

        mtrl.SetTexture(srcTexPropId, src);
        mtrl.SetFloat(blendPropId, blend);
        int rtSizeScale = 1;
#if UNITY_EDITOR
        rtSizeScale = 2;
#else
        rtSizeScale = 3; // 性能更好
#endif
        RenderTexture srcRT = RenderTexture.GetTemporary(src.width / rtSizeScale, src.height / rtSizeScale, 0, src.format);
        RenderTexture destRT = RenderTexture.GetTemporary(srcRT.width, srcRT.height, 0, srcRT.format);
        Graphics.Blit(src, srcRT);
        Graphics.Blit(srcRT, destRT, mtrl, 0);
        Graphics.Blit(destRT, dest, mtrl, 1);
        RenderTexture.ReleaseTemporary(srcRT);
        RenderTexture.ReleaseTemporary(destRT);
    }

    private void OnDestroy()
    {
        if(mtrl != null)
        {
            DestroyImmediate(mtrl);
            mtrl = null;
        }
    }
}
