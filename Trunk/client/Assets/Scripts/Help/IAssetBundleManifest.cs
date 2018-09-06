using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Linq;
using System.Text;

public sealed class IAssetBundleManifest
{
    private Dictionary<string, string[]> assetDpNames;

    public IAssetBundleManifest()
    {
        assetDpNames = new Dictionary<string, string[]>();
    }

    // 摘要:
    //     ///
    //     Get all the AssetBundles in the manifest.
    //     ///
    //
    // 返回结果:
    //     ///
    //     An array of asset bundle names.
    //     ///
    public string[] GetAllAssetBundles()
    {
        return assetDpNames.Keys.ToArray();
    }
    //
    // 摘要:
    //     ///
    //     Get all the dependent AssetBundles for the given AssetBundle.
    //     ///
    //
    // 参数:
    //   assetBundleName:
    //     Name of the asset bundle.
    public string[] GetAllDependencies(string assetBundleName)
    {
        string[] depends = null;
        if (assetDpNames.TryGetValue(assetBundleName, out depends))
        {
            return depends;
        }
        return depends;
    }

    public void SetAsstDpNames(Dictionary<string, string[]> dictionary)
    {
        assetDpNames = dictionary;
    }

    public void Serializate(string path, string name)
    {
        StringBuilder sb = new StringBuilder();
        sb.Remove(0, sb.Length);

        sb.Append("<root >");
        if (assetDpNames != null)
        {
            foreach (var abName in assetDpNames.Keys)
            {
                var dpNames = assetDpNames[abName];
                sb.Append("<assetbundle name='");
                sb.Append(abName);
                sb.Append("'>"); 

                foreach (var dp in dpNames)
                {
                    sb.Append("<dpbundle name='");
                    sb.Append(dp);
                    sb.Append("'/>");
                }
                sb.Append("</assetbundle>");
            }
        }
        sb.Append("</root>");

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(sb.ToString());
        xml.Save(path + "/" + name + ".xml");
    }

    public static IAssetBundleManifest DeSerializate(string text)
    {
        IAssetBundleManifest manifest = new IAssetBundleManifest();

        //TextAsset text = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(text);
        var nodes = xml.SelectNodes("root/assetbundle");
        //Debug.Log("AssetBundleCache " + nodes.Count);
        foreach (XmlNode abNode in nodes)
        {
            var abName = abNode.Attributes.GetNamedItem("name").Value;
            var dplist = new string[abNode.ChildNodes.Count];
            var count = 0;
            foreach (XmlNode dpNode in abNode.ChildNodes)
            {
                dplist[count] = dpNode.Attributes.GetNamedItem("name").Value;
                count++;
            }
            manifest.assetDpNames[abName] = dplist;
        }
        return manifest;
    }
}