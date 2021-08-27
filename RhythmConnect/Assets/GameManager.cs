using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameConfigManager gcm = new GameConfigManager();
    private SystemConfigManager scm = new SystemConfigManager();
    private SongListManager slm = new SongListManager();

    // Start is called before the first frame update
    void Start()
    {
        gcm.LoadGameConfig("gameconfig.rccfg");
        scm.LoadSystemConfig("systemconfig.rccfg");

        Debug.Log(gcm.GaugeMode);
        Debug.Log(scm.SongDataFolderPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
