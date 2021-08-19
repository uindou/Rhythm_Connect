using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingMoving : OmankoManager
{
    public void SceneSet()
    {
        SceneMovingSelecter.ChangeSceneSetting(SceneManager.GetActiveScene().name);
    }
}
