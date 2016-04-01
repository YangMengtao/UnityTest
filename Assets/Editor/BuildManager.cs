//———————————————————————————————
//创建人：小强
//功能：一键打包工具
//时间：2016.2.23
//———————————————————————————————
using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour {

	[MenuItem("File/一键打包工具/Build IOS")]
	static void BuildIOS()
	{
	//	BuildBeginToDo();
		//清空一下缓存
		Caching.CleanCache();

		string targetPath = Application.dataPath.Replace("Assets", "BuildIOS");

		if(Directory.Exists(targetPath))
		{
//			Directory.Delete(targetPath);
			DirectoryInfo di = new DirectoryInfo(targetPath);
			di.Delete(true);
		}
		Directory.CreateDirectory(targetPath);

		//目前屏蔽路径选择
		//targetPath = EditorUtility.OpenFolderPanel("Build IOS target Folder", Application.dataPath, "");
		if(targetPath == "")
		{
			return;
		}
		var sceneArr = EditorBuildSettings.scenes;
		List<string> lvList = new List<string>();
		for(int i = 0; i < sceneArr.Length; i++)
		{
			if(sceneArr[i].enabled)
			{
				lvList.Add(sceneArr[i].path);
			}
		}
		string[] lvArr = new string[lvList.Count];
		for(int i = 0; i < lvList.Count; i++)
		{
			lvArr[i] = lvList[i];
		}
		BuildPipeline.BuildPlayer(lvArr, targetPath, BuildTarget.iOS, BuildOptions.None);
		AssetDatabase.Refresh();
		Debug.Log("Build is OK !");
	//	OpenFolder(targetPath);
	}


}
