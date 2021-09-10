using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SongDetailKettei : UIBehaviour
{
    [SerializeField] TextMeshProUGUI music_name_button;
    [SerializeField] TextMeshProUGUI easy_level_button;
    [SerializeField] TextMeshProUGUI normal_level_button;
    [SerializeField] TextMeshProUGUI hard_level_button;

    [SerializeField] TextMeshProUGUI UI_low_level;
    [SerializeField] TextMeshProUGUI UI_mid_level;
    [SerializeField] TextMeshProUGUI UI_high_level;

    [SerializeField] TextMeshProUGUI UI_MusicInfo;
    [SerializeField] TextMeshProUGUI UI_Artist;
    [SerializeField] TextMeshProUGUI UI_Genre;
    [SerializeField] TextMeshProUGUI UI_Arranger;

    [SerializeField] TextMeshProUGUI UI_HighScore;
    [SerializeField] TextMeshProUGUI UI_MaxCombo;
    [SerializeField] TextMeshProUGUI UI_PlayCount;
    [SerializeField] TextMeshProUGUI UI_ClearRank;

    [SerializeField] TextMeshProUGUI UI_BPM;
    [SerializeField] TextMeshProUGUI UI_Notes;

    private string low,mid,high;
    private string music_name,artist,genre,arranger;
    private string high_score, max_combo, play_count, clear_rank;
    private string bpm, notes;

    public void UpdateItem(int count)
    {
        Debug.Log(count);


        low = count.ToString();
        mid = (count + 1).ToString();
        high = (count + 2).ToString();

        music_name = "SUSURU_TV ver" + count.ToString();
        artist = "SUSURU";
        genre = "Internet meme";
        arranger = "Moriyama Naotaro";

        high_score = (100*count).ToString();
        max_combo = "0";
        play_count = count.ToString();
        clear_rank = "A";

        bpm = (100 + count).ToString();
        notes = (300 + count).ToString();


        //countの値→曲名、easy/normal/hardの難易度を決定、easy,normal,hardに値を入れる　その他いろいろ
        music_name_button.text = music_name;
        easy_level_button.text = low;
        normal_level_button.text = mid;
        hard_level_button.text = high;


    }
    public void OnClick()
    {
        //曲のボタンが押された時の処理。左側の画面に曲の情報をいろいろ表示する。
        UI_low_level.text = low;
        UI_mid_level.text = mid;
        UI_high_level.text = high;

        UI_MusicInfo.text = "Title:"+music_name;
        UI_Artist.text = "Artist:"+artist;
        UI_Genre.text = "Genre:"+genre;
        UI_Arranger.text = "Arranger:"+arranger;

        UI_HighScore.text = "High Score:"+high_score;
        UI_MaxCombo.text = "Max Combo:"+max_combo;
        UI_PlayCount.text = "Play Count:"+play_count;
        UI_ClearRank.text = "Clear Rank:"+clear_rank;

        UI_BPM.text = "BPM:"+bpm;
        UI_Notes.text = "Notes:"+notes;
    }
}
