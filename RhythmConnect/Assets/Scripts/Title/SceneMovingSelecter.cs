using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMovingSelecter : MonoBehaviour
{
    public static string scene;
    public static void ChangeSceneSetting(string set_scene)
    {
        scene = set_scene;
    }
    public void SceneMoving()
    {
        SceneManager.LoadScene(scene);
    }

}
