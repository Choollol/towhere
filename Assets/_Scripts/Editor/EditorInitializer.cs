using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/* Allows faster testing of scenes.
   Since scripts in Test rely on scripts in Main, this script will load Main first, then load other scenes */
[InitializeOnLoad]
public class EditorInitializer
{
    // your first scene path:
    const string firstSceneToLoad = "Assets/Scenes/Main.unity";
    // Editor pref save name, no need to change
    const string activeEditorScene = "PreviousScenePath";
    const string isEditorInitialization = "EditorIntialization";

    // Editor iniitialization blacklist
    static List<string> invalidScenes = new List<string>
    {
    };
    // The scenes names that you want to load in addition to the first scene. Loaded in the list order.
    static List<string> extraScenesToLoad = new List<string>
    {
        "Game"
    };

    static EditorInitializer()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode && IsValidScene(invalidScenes, out string sceneName))
        {
            EditorPrefs.SetString(activeEditorScene, sceneName);
            EditorPrefs.SetBool(isEditorInitialization, true);
            SetStartScene(firstSceneToLoad);
        }
        if (state == PlayModeStateChange.EnteredPlayMode && EditorPrefs.GetBool(isEditorInitialization))
        {
            LoadExtraScenes();
        }
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            EditorPrefs.SetBool(isEditorInitialization, false);
        }
    }
    static void SetStartScene(string scenePath)
    {
        SceneAsset firstSceneToLoad = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        if (firstSceneToLoad != null)
        {
            EditorSceneManager.playModeStartScene = firstSceneToLoad;
        }
        else
        {
            Debug.Log("Could not find Scene " + scenePath);
        }
    }
    static void LoadExtraScenes()
    {
        // extra scenes to load
        foreach (string scenePath in extraScenesToLoad)
        {
            SceneManager.LoadScene(scenePath, LoadSceneMode.Additive);
        }
        // the original scene loading
        var prevScene = EditorPrefs.GetString(activeEditorScene);
        //SceneManager.LoadScene(prevScene, LoadSceneMode.Additive);
    }
    static bool IsValidScene(List<string> scenesToCheck, out string sceneName)
    {
        sceneName = SceneManager.GetActiveScene().name;
        return !scenesToCheck.Contains(sceneName);
    }
}