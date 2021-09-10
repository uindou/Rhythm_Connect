using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameConfig gc = new GameConfig();
    private SystemConfig sc = new SystemConfig();
    private SongList sl = new SongList();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gc.LoadGameConfig(myConstants.GameConfigFilePath);
        sc.LoadSystemConfig(myConstants.SystemConfigFilePath);
        sl.LoadSongList();

        Debug.Log(gc.GaugeMode);
        Debug.Log(sc.KeyConfig[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
