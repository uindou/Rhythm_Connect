using UnityEngine;
using DG.Tweening;

/* トロフィー動作 */
public class NotifyController : MonoBehaviour
{
    private static readonly Vector3 initPosition = new Vector3(0f, -25f, 0f);
    
    private static readonly float moveTime = 0.5f;          /* 移動時間 */
    private static readonly float intervalTime = 2.5f;      /* フルで表示されてる時間 */
    private static readonly float fadeTime = 0.7f;          /* さよならするのにかかる時間 */


    [SerializeField] private CanvasGroup canGr;
    [SerializeField] private Sequence seq;

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
        seq?.Kill();

        seq = DOTween.Sequence().OnStart(() => {
            canGr.transform.localPosition = initPosition;
            /* デカワンコの移動 */
            canGr.alpha = 1f;
        })
        .Append(canGr.transform.DOLocalMoveX(0, moveTime).SetEase(Ease.OutQuart))  /* 左から出るようにする(Mから出すようにしてもいいよ) */
        .AppendInterval(intervalTime)                                               /* フル表示の時間 */
        .Append(canGr.DOFade(0f, fadeTime));                                        /* さよなら タイム */

        seq.Play();
    }
}
