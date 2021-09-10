using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongShuflle : MonoBehaviour
{
    private List<SongData> SongDatas;
    public List<Sprite> images;
    [SerializeField] List<Image> SongButtons;
    [SerializeField] TextMeshProUGUI song_name;
    [SerializeField] TextMeshProUGUI song_credit;
    [SerializeField] TextMeshProUGUI song_score;
    [SerializeField] TextMeshProUGUI song_notes;

    private int song_num;
    private int now_song;

    void Awake()
    {
        now_song = 0;
        SongDatas = new List<SongData>();
        LoadSongDatas();
        SetChange();
    }
    // Start is called before the first frame update


    public void LeftTurn()
    {
        if (now_song == 0)
        {
            now_song = song_num - 1;
        }
        else
        {
            now_song -= 1;
        }
        SetChange();
    }

    public void RightTurn()
    {
        now_song = (now_song + 1) % song_num;
        SetChange();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftTurn();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightTurn();
        }
    }
    void SetChange()
    {
        InfoSet();
        for(int i = 0; i < 5; i++)
        {
            //SongButtons[i].GetComponent<Image>().sprite = images[(now_song + i) % song_num];
            SongButtons[i].overrideSprite = images[(now_song + i) % song_num];
        }
    }
    void InfoSet()
    {
        for (int i = 0; i < 5; i++)
        {
            int j = (now_song + i) % song_num;
            song_name.text = SongDatas[j].song_name;
            song_credit.text = SongDatas[j].credit;
            song_score.text = SongDatas[j].score.ToString();
            song_notes.text = SongDatas[j].notes.ToString();
        }
    }

    private void LoadSongDatas()
    {
        for(var a = 0; a < 5; a++)
        {
            SongData song_data = new SongData();
            song_data.image = images[a];
            song_data.song_name = "susuru"+a.ToString();
            song_data.notes = 100*a;
            song_data.score = 1000*a;
            song_data.credit = "susuru_tv"+ a.ToString();
            SongDatas.Add(song_data);
        }
        song_num = 5;
    }
}
public struct SongData
{
    public Sprite image;
    public string song_name;
    public int notes;
    public int score;
    public string credit;
}