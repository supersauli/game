using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
class Table
{
    public int f1;
    public int f2;
}

//[CSharpCallLua]
public interface LuaFunc
{
   int f1{ get; set; }
    int f2{ get; set; }
    int add(int num1,int num2);
}


//[CSharpCallLua]
public interface LuaFunc2
{
    int f1 { get; set; }
    int f2 { get; set; }
    int add(int num1, int num2);
}
public interface LuaFunc3
{
    int f1 { get; set; }
    int f2 { get; set; }
    int add(int num1, int num2);
}


[CSharpCallLua]
public delegate int AddFunc(int a, int b);

public class XluaLoad : MonoBehaviour {
    [CSharpCallLua]
    private delegate int AddFunc1(int a, int b);
    private LuaEnv _luaenv;
    
	// Use this for initialization
	void Start ()
	{
         _luaenv = new LuaEnv();
        //LoadLuaStringTest();
        // LoadLuaFileTest();
	    LoadLuaFileExtern();
	    //CToLuaData();
	    transform.localScale = new Vector3(-1,0,0);

	}

    void CToLuaData()
    {
        {
            // lua 全局数据
            Debug.Log("global =" + _luaenv.Global.Get<int>("_global"));
        }

        {
            var table = _luaenv.Global.Get<Table>("scene");
            Debug.Log("table.f1=" + table.f1);
            Debug.Log("table.f2=" + table.f2);

            var dict = _luaenv.Global.Get<Dictionary<string, int>>("scene");
            Debug.Log("dict.f1=" +dict["f1"] );
            Debug.Log("dict.f2=" +dict["f2"] );
            //较慢
            var luaTable = _luaenv.Global.Get<LuaTable>("scene");
            Debug.Log("luaTable.f1 = " + luaTable.Get<int>("f1"));
            Debug.Log("luaTable.f2 = " + luaTable.Get<int>("f2"));

            // 读取数值的
            var list = _luaenv.Global.Get<List<int>>("table");
            Debug.Log("list[0] = "+list[0]);
            Debug.Log("list[1] = "+list[1]);

            var luaFunc = _luaenv.Global.Get<LuaFunc3>("luaAdd");
            Debug.Log("func.f1 = " + luaFunc.f1);
            Debug.Log("func.f1 = " + luaFunc.f2);
            Debug.Log("func.f1 = " + luaFunc.add(1,2));

        }


        {
            var addFunc = _luaenv.Global.Get<AddFunc>("AddFunc");
            Debug.Log("addFunc"+ addFunc(1,100));
        }


    }


    private void LoadLuaStringTest()
    {
        //LuaTable table = new LuaTable(1,_luaenv);
        var str = _luaenv.DoString(
            @" 
            print('load lua by string.')
            return 1,'hello'
            "
           ,"LoadLuaStringError"
          // ,table
        );
        Debug.Log("str value :" + str[0] + " " +str[1] );
    }

    private void LoadLuaFileTest()
    {
        //require 'my_lua'
        var str = _luaenv.DoString(
            @" 
            require 'event_lua'
            "
        );
 
    }

    private void LoadLuaFileExtern()
    {
       _luaenv.AddLoader((ref string fileName) =>
        {
            var script = Resources.Load("lua/"+fileName + ".lua") as TextAsset;
            if (script != null)
            {
                Debug.Log("script = "+script.bytes);
                return script.bytes;
            }

            //Debug.Log("error");
            //var ret = System.Text.Encoding.Default.GetBytes("lua/" + fileName);
            return null;
        });

        var str = _luaenv.DoString(
            @" 
            require 'event_lua'
            "
                );

    }

    void Update () {
	    if (_luaenv == null){
	        return;
	    }

	    _luaenv.Tick();

	}
    private void OnDestroy()
    {
	    if (_luaenv == null){
	        return;
	    }
        _luaenv.Dispose();
    }
}
