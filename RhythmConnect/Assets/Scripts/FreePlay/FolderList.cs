using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using static myConstants;
using TMPro;

/// <summary>
/// FreePlayでオリジナル曲フォルダを探して、ボタンに表示するクラス。
/// </summary>
public class FolderList : MonoBehaviour
{
    private List<string> folders;

    // TODO : フォルダ数が不定なので、InfiniteScrollみたいな感じのを実装しなきゃ
    // まずはUI固めるところから
    [SerializeField] private TextMeshProUGUI uiText1;
    [SerializeField] private TextMeshProUGUI uiText2;
    [SerializeField] private TextMeshProUGUI uiText3;

    private void Awake()
    {
        string fp = SongDataFolderPath + '\\' + ModeString[Rc];
        if(LoadFolderList(fp) == false)
        {
            Debug.Log("Failure Folder Load");
        }

        uiText1.text = TrimFolderPath(folders[0]);
        uiText2.text = TrimFolderPath(folders[1]);
        uiText3.text = TrimFolderPath(folders[2]);
    }

    /// <summary>
    /// 引数に受け取ったフォルダパス配下のサブディレクトリをfoldersにロードする。
    /// </summary>
    /// <param name="folderpath"></param>
    /// <returns></returns>
    private bool LoadFolderList(string folderpath)
    {
        folders = LoadSubFolderToList(folderpath);
        if(folders == null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 引数に受け取ったフォルダパスから末尾のフォルダ名だけを切り出す。
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string TrimFolderPath(string path)
    {
        string[] foldernames = path.Split('\\');
        string endfoldername = foldernames[foldernames.Length - 1];

        return endfoldername;
    }
}