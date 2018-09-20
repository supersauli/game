using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class Allocator  
{
    public static Vector2 CreateVector2()
    {
        return Vector2.zero;
    }
    public static Vector2 CreateVector2(float x)
    {
        Vector2 vTemp = Vector2.zero;
        vTemp.x = x;
        return vTemp;
    }
    public static Vector2 CreateVector2(float x , float y)
	{
		Vector2 vTemp = Vector2.zero;
		vTemp.x = x;
		vTemp.y = y;
		return vTemp;
	}
    public static Vector3 CreateVector3()
    {
        return Vector3.zero;
    }
    public static Vector3 CreateVector3(float x )
    {
        Vector3 vTemp = Vector3.zero;
        vTemp.x = x;

        return vTemp;
    }
    public static Vector3 CreateVector3(float x , float y )
    {
        Vector3 vTemp = Vector3.zero;
        vTemp.x = x;
        vTemp.y = y;
        return vTemp;
    }
    public static Vector3 CreateVector3(float x , float y , float z)
	{
		Vector3 vTemp = Vector3.zero;
		vTemp.x = x;
		vTemp.y = y;
		vTemp.z = z;
		return vTemp;
	}
    public static Vector4 CreateVector4()
    {
        return Vector4.zero;
    }
    public static Vector4 CreateVector4(float x )
    {
        Vector4 vTemp = Vector4.zero;
        vTemp.x = x;
        return vTemp;
    }
    public static Vector4 CreateVector4(float x , float y )
    {
        Vector4 vTemp = Vector4.zero;
        vTemp.x = x;
        vTemp.y = y;
        return vTemp;
    }
    public static Vector4 CreateVector4(float x , float y , float z )
    {
        Vector4 vTemp = Vector4.zero;
        vTemp.x = x;
        vTemp.y = y;
        vTemp.z = z;

        return vTemp;
    }
    public static Vector4 CreateVector4(float x , float y , float z , float w )
	{
		Vector4 vTemp = Vector4.zero;
		vTemp.x = x;
		vTemp.y = y;
		vTemp.z = z;
		vTemp.w = w;
		return vTemp;
	}
    public static Color CreateColor()
    {
        return Color.clear;
    }
    public static Color CreateColor(float r , float g , float b)
    {
        Color cColor = Color.clear;
        cColor.r = r;
        cColor.g = g;
        cColor.b = b;
        cColor.a = 1.0f;
        return cColor;
    }
    public static Color CreateColor(float r , float g , float b , float a )
    {
        Color cColor = Color.clear;
        cColor.r = r;
        cColor.g = g;
        cColor.b = b;
        cColor.a = a;
        return cColor;
    }
    public static T CreateVar<T>()
    {
        return System.Activator.CreateInstance<T>();
    }
    public static T[] CreateArray<T>(int iSize)
    {
        return new T[iSize];
    }
    public static T[] CreateArray<T>(uint iSize)
    {
        return new T[iSize];
    }
    public static T[] CreateArray<T>(long iSize)
    {
        return new T[iSize];
    }

    public static T[,] CreateArray2D<T>(int iwidth,int iheight)
    {
        return new T[iwidth, iheight];
    }
    public static T[,] CreateArray2D<T>(uint iwidth, uint iheight)
    {
        return new T[iwidth, iheight];
    }
    public static T[,] CreateArray2D<T>(long iwidth, long iheight)
    {
        return new T[iwidth, iheight];
    }
    public static T[,,] CreateArray3D<T>(int iwidth, int iheight,int depth)
    {
        return new T[iwidth, iheight, depth];
    }
    public static List<T> CreateList<T>(int capacity=0)
    {
        if (capacity > 0)
        {
            return new List<T>(capacity);
        }
        return Allocator.CreateVar<List<T>>();
    }
    public static List<T> CreateList<T>(IEnumerable<T> collection)
    {
        return new List<T>(collection);
    }
    public static Queue<T> CreateQueue<T>(int capacity = 0)
    {
        if (capacity > 0)
        {
            return new Queue<T>(capacity);
        }
        return Allocator.CreateVar<Queue<T>>();
    }

    public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(int capacity = 0)
    {
        if (capacity > 0)
        {
            return new Dictionary<TKey, TValue>(capacity);
        }
        return Allocator.CreateVar<Dictionary<TKey, TValue>>();
    }

    public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(IEqualityComparer<TKey> comparer)
    {
       return new Dictionary<TKey, TValue>(comparer);
    }
    public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
    {
        return new Dictionary<TKey, TValue>(dictionary);
    }
}