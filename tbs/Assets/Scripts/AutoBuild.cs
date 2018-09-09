using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Reporting;
using UnityEditor.Build;
using UnityEngine;

public class AutoBuild : MonoBehaviour
{
	
    static public string HasErrorOcurred;

    static public void BuildProject()
    {
#if UNITY_EDITOR

        HasErrorOcurred = string.Empty;
        UnityEditor.BuildPlayerOptions buildPlayerOptions = new UnityEditor.BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = "AutoBuildWin";
        buildPlayerOptions.target = UnityEditor.BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = UnityEditor.BuildOptions.None;
        //BuildReport result = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions);
        
		Debug.Log(UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions).ToString());
#endif
    }

}
