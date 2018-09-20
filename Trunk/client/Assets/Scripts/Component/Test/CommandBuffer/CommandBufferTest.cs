//Command Buffer测试
//by: puppet_master
//2017.5.26

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CommandBufferTest : MonoBehaviour
{

    private CommandBuffer commandBuffer = null;
    private RenderTexture renderTexture = null;
    private Renderer targetRenderer = null;
    public GameObject targetObject = null;
    public GameObject targetObject2 = null;
    public Material replaceMaterial = null;
    private Camera _mainCamera = null;
    void OnEnable()
    {
        _mainCamera = Camera.main;
        targetRenderer = targetObject.GetComponentInChildren<Renderer>();
        //申请RT
        renderTexture = RenderTexture.GetTemporary(512, 512, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 4);
        commandBuffer = new CommandBuffer();
        //设置Command Buffer渲染目标为申请的RT
        commandBuffer.SetRenderTarget(renderTexture);
        //初始颜色设置为灰色
        commandBuffer.ClearRenderTarget(true, true, Color.gray);
        //绘制目标对象，如果没有替换材质，就用自己的材质
        Material mat = replaceMaterial == null ? targetRenderer.sharedMaterial : replaceMaterial;
        commandBuffer.DrawRenderer(targetRenderer, mat);
        //然后接受物体的材质使用这张RT作为主纹理
        targetObject2.GetComponent<Renderer>().sharedMaterial.mainTexture = renderTexture;
        //直接加入相机的CommandBuffer事件队列中
        _mainCamera.AddCommandBuffer(CameraEvent.AfterForwardOpaque, commandBuffer);
    }

    void OnDisable()
    {
        //移除事件，清理资源
        _mainCamera.RemoveCommandBuffer(CameraEvent.AfterForwardOpaque, commandBuffer);
        commandBuffer.Dispose();
        renderTexture.Release();
     }

    //也可以在OnPreRender中直接通过Graphics执行Command Buffer，不过OnPreRender和OnPostRender只在挂在相机的脚本上才有作用！！！
    //void OnPreRender()
    //{
    //    //在正式渲染前执行Command Buffer
    //    Graphics.ExecuteCommandBuffer(commandBuffer);
    //}
}
