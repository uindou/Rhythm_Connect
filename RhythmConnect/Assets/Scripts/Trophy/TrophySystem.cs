using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophySystem : MonoBehaviour
{
    /* トロフィーPanelの管理を行う関数 */
    void Start()
    {   
        int i;
        int j;
        /* 0：トロフィー名、1：グレード */
        string[,] showTrophyList = new string[128, 2];
        
        showTrophyList = new string[128, 2];
        /* 初期化 */
        for (i = 0; i < 128; i++){
            for (j = 0; j < 2; j++){
                showTrophyList[i, j] = "0";
            }
        }

        /* トロフィーを獲得しているか確認 */
        showTrophyList = GetTrophyCheck(showTrophyList);

        /* 何も獲得していない場合は表示しない */
        if (showTrophyList[0, 0].Equals("0")){
            /* 終わり */
        }
        else {
            i = 0;

            /* トロフィーの表示を始める */
            while (true){
                /* elseに来た時点で必ず一個はリストに登録されてる */
                ShowTrophy(showTrophyList[i, 0], showTrophyList[i, 1], 0);

                /* 中断判定 */
                if (showTrophyList[i, 0].Equals("0")){
                    break;
                }
                else {
                    /* 次のリストへ */
                    i++;
                }
            }
        }
    }

    void Update()
    {
        /* この関数はとりあえず使わない */
    }

    /* 獲得したトロフィーの確認と管理 */
    string[,] GetTrophyCheck(string[,] showTrophyList){
        int i;
        int j;
        string[] trophyInfo;

        /* 1トロフィーあたりの情報 */ /* とりあえず7固定 */
        trophyInfo = new string[7];
        /* csvファイルを上の行から読み込む */ /* orデータベースでもいいです */
        while (true){
            /* ファイルパス */

            /* トロフィー名の取得 */

            /* グレードの取得 */

            /* ここで連結までして配列に入れた方がいいか？ */
        }

        return showTrophyList;
    }

    /* トロフィー表示 */
    void ShowTrophy(string trophyName, string grade, int trophyIndex){
        float moveRight;
        float moveLeft; 

        /* 移動量設定 */
        moveRight = 10f;
        moveLeft = -10f;

        /* 右移動 */
        while (true){
            transform.position += new Vector3(moveRight, 0f, 0f);

            /* 全部表示されたら抜ける */
            if (transform.position.x >= 250f){
                break;
            }
        }

        /* 5秒待機 */
        interruptTime(5000);

        /* 左移動 */
        while (true){
            transform.position += new Vector3(moveLeft, 0f, 0f);;

            /* 全部表示されたら抜ける */
            if (transform.position.x <= -250f){
                break;
            }
        }
    }

    /* 指定時間プログラム中断 */
    void interruptTime(int time){
        Thread.Sleep(time);
    }
}
