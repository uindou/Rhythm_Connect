using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDisplay : MonoBehaviour
{
    int song_num;
    List<GameObject> song_datas;
    // Start is called before the first frame update
    void Start()
    {
        LoadSongs();
        MakeSongBox();

    }

    void UpSongs()
    {

    }

    void DownSongs()
    {

    }

    void LoadSongs()
    {

    }

    void MakeSongBox()
    {
        for(int i = 0; i < song_num; i++)
        {
            //GameObject gobj = Instantiate(song_button_prefab);
            //gobj.なんとか = なんとか　をここでやっとく
            //song_datas.Add(gobj);
        }
    }

    void Update()
    {
        
    }
}
