using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameConfig gc = new GameConfig();
    private SystemConfig sc = new SystemConfig();
    private SongList sl = new SongList();

    // Start is called before the first frame update
    void Start()
    {
        gc.LoadGameConfig(myConstants.GameConfigFilePath);
        sc.LoadSystemConfig(myConstants.SystemConfigFilePath);

        Debug.Log(gc.GaugeMode);
        Debug.Log(sc.SongDataFolderPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
