using UnityEngine;
using System.Collections;
//using AngelScript;
using System;
using System.IO;
using System.Xml;
using NLua;
//using Progwars.Net;
public class ModInit : MonoBehaviour {
	private static string modpath_lua;
	private static string assetsbundles_path;
    private static string modpath_obj;
	private static string syspath;
    private static string datapath;
    public GameObject[] Spawned_GameObjects;
	public string[] WorkshopFileNames;
	public bool AllowCCSDLLS = false;
	public bool AllowCCSDLLSWorkshop = true;
    public bool obj_isactive;
    public Vector3 obj_spawnpos;
	public string[] SystemFiles;
    // Use this for initialization
    void Start () {
		  String currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        String dllPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins";
        if(currentPath.Contains(dllPath) == false)
        {
            Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
        }
    

		SystemFiles = new string[100];
       obj_isactive = true;
        obj_spawnpos = new Vector3(0, 0, 0);
		modpath_lua = Application.streamingAssetsPath + "/Mods/Scripting/Lua";
		syspath = Application.streamingAssetsPath + "/System";
		if (Directory.Exists(modpath_lua))
		{
			foreach (string dirname in Directory.GetDirectories(modpath_lua))
			{
				foreach (string filename in Directory.GetFiles(dirname))
				{
					if (filename.EndsWith(".lua"))
					{
						Lua _lua = new Lua();
						if(AllowCCSDLLS)
							_lua.LoadCLRPackage ();
						_lua.DoFile(filename);
					}
				}
			}
		}
		modpath_lua = Application.streamingAssetsPath + "/Workshop/Scripting/Lua";
		syspath = Application.streamingAssetsPath + "/System";
		if (Directory.Exists(modpath_lua))
		{
			foreach (string dirname in Directory.GetDirectories(modpath_lua))
			{
				foreach (string filename in Directory.GetFiles(dirname))
				{
					if (filename.EndsWith(".lua"))
					{
						Lua _lua = new Lua();
						if(AllowCCSDLLSWorkshop)
							_lua.LoadCLRPackage ();
						_lua.DoFile(filename);
					}
				}
			}
		}

		if (Directory.Exists(syspath + "/Lua"))
		{
			int i = 0;
			int __i = 0;
			foreach (string dirname in Directory.GetDirectories(syspath + "/Lua"))
			{
				
				foreach (string filename in Directory.GetFiles(dirname))
				{
					if (filename.EndsWith(".lua"))
					{
						SystemFiles [__i] = filename;
						__i++;
						Lua _lua = new Lua();
						_lua.LoadCLRPackage ();
						_lua.LoadFile(filename);
						_lua.DoFile(filename);
					}
				}
			}
		}
        else
        {
			Debug.LogError("System Lua Directory Not Found!");
            Directory.CreateDirectory(modpath_lua);
			Application.Quit();
        }
        modpath_obj = Application.streamingAssetsPath + "/Mods/Models/";
        if (Directory.Exists(modpath_obj))
        {
            foreach (string dirname in Directory.GetDirectories(modpath_obj))
            {
                foreach (string filename in Directory.GetFiles(dirname))
                {
                    if (filename.EndsWith(".obj"))
                    {
						GameObject me = OBJLoader.LoadOBJFile (filename);
						Instantiate (me);
                    }
                }
            }
            }
        datapath = Application.streamingAssetsPath + "/Mods/Data/";
		if (Directory.Exists(datapath))
		{
			foreach (string dirname in Directory.GetDirectories(modpath_lua))
			{
				foreach (string filename in Directory.GetFiles(dirname))
				{
					if (filename.EndsWith(".txt"))
					{

					}
				}
			}
		}
		else
		{
			Debug.LogWarning("Data Directory Not Found!");
			Directory.CreateDirectory(modpath_lua);
		}
	
	assetsbundles_path =  Application.streamingAssetsPath + "/System/Data/";
		if (Directory.Exists(assetsbundles_path))
	{
			foreach (string dirname in Directory.GetDirectories(assetsbundles_path))
			{
				foreach (string filename in Directory.GetFiles(dirname)) {
					if (filename.EndsWith (".unity3d")) {

					}
				}
		}
	}
	else
	{
		Debug.LogWarning("System Directory: Data Not Found!");
			Directory.CreateDirectory(assetsbundles_path);
	}
}

    // Update is called once per frame
    void Update () {
	    
	}
}
