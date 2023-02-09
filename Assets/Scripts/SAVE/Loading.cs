using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loading
{
    public static void LoadScene(string scene){
        SaveSystem.save.currentLevel = scene;
        SceneManager.LoadScene(scene);
        SaveSystem.Save();
    }
}
