using UnityEngine;
using System.Collections;
/// <summary>
/// 加载ab记录器，记录ab的信息和被依赖次数 主资源加载完毕的时候 如果发现被依赖的计数器是0，就卸载这个被依赖的包
/// author lgr
/// </summary>
public class AssetsBundleData {
    public string name;//资源名
    public int depIndex = 0;//被依赖索引计数器,卸载资源的时候，查找资源依赖的资源，依赖的会进行计数器-1，到0就就卸载这个依赖的资源
    public AssetBundle ab = null;//ab包
}
