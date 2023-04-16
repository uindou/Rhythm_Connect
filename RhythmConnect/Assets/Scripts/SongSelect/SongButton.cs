using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class SongButton : UIBehaviour
{
    private SongSelecter SSelecter;
    private SongInfo ThisSong;
    private int myCount;
    [SerializeField] private TextMeshProUGUI uiDiff;
    [SerializeField] private TextMeshProUGUI uiSongName;
    [SerializeField] private Button uiMyButton;

    void Awake()
    {
        SSelecter = GetComponentInParent<SongSelecter>();
    }

    public void UpdateItem(int count)
    {
        myCount = count % SSelecter.sl.Songs.Count;
        ThisSong = SSelecter.sl.Songs[myCount];
        uiDiff.text = ThisSong.PlayLevel[SSelecter.difficult].ToString();
        uiDiff.color = myConstants.DiffColor[SSelecter.difficult];
        uiSongName.text = ThisSong.SongName;
    }

    private void Update() 
    {
        uiDiff.text = ThisSong.PlayLevel[SSelecter.difficult].ToString();
        uiDiff.color = myConstants.DiffColor[SSelecter.difficult];

        if(myCount == SSelecter.cursol)
        {
            Color clr;
            ColorUtility.TryParseHtmlString("#88F1FF", out clr);
            uiMyButton.image.color = clr;
        }
        else
        {
            uiMyButton.image.color = Color.white;
        }
    }
    public void OnClick()
    {
        SSelecter.cursol = myCount;
    }
}


