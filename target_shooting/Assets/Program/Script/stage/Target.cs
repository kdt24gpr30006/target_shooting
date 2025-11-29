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
            UnityEngine.Debug.Log("あたった");
            // 相手がShotの場合消滅
            if (collision.gameObject.CompareTag("Shot"))
            {
                // 当たった球からShotDestroyerを取得し、削除をする
                var shotDestroyer = collision.gameObject.GetComponent<ShotDestroyer>();
                if (shotDestroyer != null)
                {
                    shotDestroyer.DestroyShot();
                }

                // 的側のあたった時の処理を呼ぶ
                owner.OnHit();
            }
        }
    }

    // 的クラス
    public class Target : Obstacle
    {
        private ScoreText m_text;
        // 削除フラグ
        private bool m_isDestroyed = false;

        public Target(GameObject prefab, Vector3 pos, ScoreText scoreText) : base(prefab, pos)
        {
            // 高さをランダムに
            float posY = UnityEngine.Random.Range(-30, 40);
            pos.y = posY / 10;

            // 初期座標, 角度設定
            instance.transform.position = pos;
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);

            m_text = scoreText;

            // 当たり判定用のコンポーネント追加
            var handler = instance.AddComponent<TargetCollisionHandler>();
            handler.SetOwner(this);
        }

        // 当たった時
        public void OnHit()
        {
            // スコアを増やす
            m_text.AddScore();

            DestroySelf();
        }

        // 自分を削除
        private void DestroySelf()
        {
            if (m_isDestroyed)
            {
                return;
            }

            if (instance != null)
            {
                UnityEngine.Object.Destroy(instance);
            }

            instance = null;
            m_isDestroyed = true;
        }

        public bool isDestroyed()
        {
            return m_isDestroyed;
        }


    } // class
}// namespace