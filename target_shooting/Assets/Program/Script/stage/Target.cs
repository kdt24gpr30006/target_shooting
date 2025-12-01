using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene
{
    // 当たり判定用のclass
    public class TargetCollisionHandler : MonoBehaviour
    {
        // 対応するTargetを設定
        private Target owner;
        public void SetOwner(Target target)
        {
            owner = target;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 相手がShotの場合消滅
            if (collision.gameObject.CompareTag("Shot"))
            {
                // 当たった球を削除
                collision.gameObject.GetComponent<ShotDestroyer>()?.DestroyShot();

                // 的側のあたった時の処理を呼ぶ
                owner.OnHit();
            }
        }
    }

    // 的クラス
    public class Target : Obstacle
    {
        private const float Y_MIN = -3;
        private const float Y_MAX = 4;

        // スコア表示用
        private ScoreText m_text;

        // 削除フラグ
        private bool m_isDestroyed = false;

        public Target(GameObject prefab, Vector3 pos, ScoreText scoreText) : base(prefab, pos)
        {
            // 高さをランダムに
            pos.y = UnityEngine.Random.Range(Y_MIN, Y_MAX);

            // 初期座標, 角度設定
            m_object.transform.position = pos;
            m_object.transform.rotation = Quaternion.Euler(0, 180, 0);

            // テキスト取得
            m_text = scoreText;

            // 当たり判定用のコンポーネント追加
            var handler = m_object.AddComponent<TargetCollisionHandler>();
            handler.SetOwner(this);
        }

        // 当たった時
        public void OnHit()
        {
            m_text.AddScore();
            DestroySelf();
        }

        // 自分を削除
        private void DestroySelf()
        {
            if (m_isDestroyed == true && m_object == null)
            {
                return;
            }

            // オブジェクトを削除
            UnityEngine.Object.Destroy(m_object);

            m_object = null;
            m_isDestroyed = true;
        }

        // 削除されたか
        public bool isDestroyed()
        {
            return m_isDestroyed;
        }

    } // class
}// namespace