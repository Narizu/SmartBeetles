using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneList {

    private static SceneList instance;
    private static Dictionary<string, int> scenes;

    private SceneList()
    {
        scenes = new Dictionary<string, int>();
        scenes.Add("MainScreen", 0);
        scenes.Add("LevelSelection", 1);
        scenes.Add("GameModeSelection", 2);
        scenes.Add("CheckpointsMode", 3);
        scenes.Add("Settings", 4);
    }

    public static SceneList getInstance()
    {
        if (instance == null)
            instance = new SceneList();
        return instance;
    }

    public int getSceneID(string name)
    {
        return scenes[name];
    }
}
