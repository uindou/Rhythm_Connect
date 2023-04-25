using UnityEngine;

/* トロフィー動作 */
public class NotifyController : MonoBehaviour
{
    private static readonly Vector3 initPosition = new Vector3(-400f, -25f, 0f);
    
    private static readonly float moveRight = 1.0f;          /* 移動時間 */
    private static readonly float intervalTime = 3.0f;      /* フルで表示されてる時間 */
    private static readonly float fadeTime = 0.7f;          /* さよならするのにかかる時間 */


    [SerializeField] private CanvasGroup canGr;

    void Update(){
        /* space押したら出る4 */
        if (Input.GetKeyDown(KeyCode.Space)){
            PlayNotification();
        }
        else {
            /* 何もないのでガシマンをする */
        }
    }

    /* トロフィー通知の動きを管理しちゃう4 */
    private void PlayNotification(){
        
    }
}
