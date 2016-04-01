using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
 
class PerformBuild
{
    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();
 
        foreach(EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if(e==null)
                continue;
 
            if(e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }
 
    static string GetBuildPath()
    {
		string dirPath = Application.dataPath.Replace("Assets", "BuildIOS");
        if(!System.IO.Directory.Exists(dirPath)){
            System.IO.Directory.CreateDirectory(dirPath);
        }
        return dirPath;
    }
 
    [UnityEditor.MenuItem("Tools/PerformBuild/Test Command Line Build iPhone Step")]
    static void CommandLineBuild ()
    {
		Debug.Log("Command line build\n------------------\n------------------");
 
        string[] scenes = GetBuildScenes();
        string path = GetBuildPath();
        if(scenes == null || scenes.Length==0 || path == null)
		{
			Debug.Log("not have scens");
            return;
		}
 
        Debug.Log(string.Format("Path: \"{0}\"", path));
        for(int i=0; i<scenes.Length; ++i)
        {
            Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
        }
 
		Debug.Log("Starting Build!");
        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.iOS, BuildOptions.None);
    }
 
    static string GetBuildPathAndroid()
    {
        string dirPath = Application.dataPath +"/build/android";
        if(!System.IO.Directory.Exists(dirPath)){
            System.IO.Directory.CreateDirectory(dirPath);
        }
        return dirPath;
    }
 
    [UnityEditor.MenuItem("Tools/PerformBuild/Test Command Line Build Step Android")]
    static void CommandLineBuildAndroid ()
    {
		Debug.Log("Command line build android version\n------------------\n------------------");
 
        string[] scenes = GetBuildScenes();
        string path = GetBuildPathAndroid();
        if(scenes == null || scenes.Length==0 || path == null)
            return;
 
        Debug.Log(string.Format("Path: \"{0}\"", path));
        for(int i=0; i<scenes.Length; ++i)
        {
            Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
        }
 
		Debug.Log("Starting Android Build!");
        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None);
    }

    static void MoveFolderTo(string p, string p_2)
    {
        string[] files = Directory.GetFiles("Assets/StreamingAssets/", "*.*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            string name = Path.GetFileName(files[i]).ToString();
            int c = name.IndexOf("Configuration");
            if (c != -1) {
                File.Delete(files[i]);
                Debug.Log( name );
            }

        }
        //判断文件是不是存在
        if(File.Exists(p_2))
        {
            //如果存在则删除
            File.Delete(p_2);
        }
        
        System.IO.File.Copy(p, p_2);
    }
    
    [UnityEditor.MenuItem("Tools/PerformBuild/TapStar_Test")]
    static void TapStar_Test ()
    {
        MoveFolderTo("Assets/BuildConfig/BuildConfig_TapStar_Test.txt", "Assets/StreamingAssets/Configuration.txt");
        Debug.Log("TD OK!");
    }

    [UnityEditor.MenuItem("Tools/PerformBuild/TapStar_QA")]
    static void TapStar_QA ()
    {
        MoveFolderTo("Assets/BuildConfig/BuildConfig_TapStar_QA.txt", "Assets/StreamingAssets/Configuration.txt");
        Debug.Log("TD OK!");
    }

    [UnityEditor.MenuItem("Tools/PerformBuild/TapStar_Prod")]
    static void TapStar_Prod ()
    {
        MoveFolderTo("Assets/BuildConfig/BuildConfig_TapStar_Prod.txt", "Assets/StreamingAssets/Configuration.txt");
        Debug.Log("TD OK!");
    }

    [UnityEditor.MenuItem("Tools/PerformBuild/SB_QA")]
    static void SB_QA ()
    {
        MoveFolderTo("Assets/BuildConfig/BuildConfig_SB_QA.txt", "Assets/StreamingAssets/Configuration.txt");
        Debug.Log("TD OK!");
    }

}