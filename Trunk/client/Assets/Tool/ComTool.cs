using System.IO;

/// <summary>
/// 常用方法
/// lgr
/// </summary>
public class ComTool
{
    /// <summary>
    /// 根据路径获取ab名字
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetABNameByPath(string path)
    {
        int index = path.LastIndexOf("/");
        string name = path.Substring(index + 1);
        return name;
    }

    /// <summary>
    /// 读取文件字节流
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] ReadFile(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        string labor = path;
        if (!System.IO.File.Exists(labor))
        {
            return null;
        }
        try
        {
            FileStream readStream = System.IO.File.OpenRead(labor);
            int nFileLength = (int)readStream.Length;
            if (0 >= nFileLength)
            {
                readStream.Close();
                return null;
            }

            byte[] result = new byte[nFileLength];
            readStream.Read(result, 0, nFileLength);

            readStream.Close();
            readStream = null;

            return result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 写入本地文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="bytes"></param>
    public static void WriteFile(string path,byte[] bytes)
    {
        if (File.Exists(path))
        {
            
            File.Delete(path);
        }

        FileInfo t = new FileInfo(path);
        if (!t.Exists)
        {
            Directory.CreateDirectory(t.DirectoryName);
        }
        File.WriteAllBytes(path, bytes);
    }

    public static string FormatTickUrl(string url)
    {
        return GameTools.StringBuilder(url, "?t=", GameTools.GetTimestamp(300));
    }
}
