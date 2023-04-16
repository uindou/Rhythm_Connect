using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// 曲選択画面の各曲のボタンに割り当てるスクリプト
/// 正確にはスクロールバーが持ってる。
/// </summary>
public class SongButton : UIBehaviour
{
    private SongSelecter SSelecter;
    private SongInfo ThisSong;
    private int myCount;

    //以下の変数はUnity上で各コンポーネントにアタッチする必要があるので注意
    [SerializeField] private TextMeshProUGUI uiDiff;
    [SerializeField] private TextMeshProUGUI uiSongName;
    [SerializeField] private Button uiMyButton;

    void Awake()
    {
        SSelecter = GetComponentInParent<SongSelecter>();
    }

    /// <summary>
    /// InfiniteScrollさんの方から呼ばれるメソッド。
    /// 別に毎フレーム呼ばれるわけではなく、恐らく生成時に呼ばれるだけなので注意。
    /// </summary>
    /// <param name="count">このスクリプトが担当しているオブジェクトのインデックス</param>
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
        //現在選択中の難易度に合わせて色と表示を変える。
        uiDiff.text = ThisSong.PlayLevel[SSelecter.difficult].ToString();
        uiDiff.color = myConstants.DiffColor[SSelecter.difficult];

        //カーソルが自分のインデックスと等しければ選択中の色になる。
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


