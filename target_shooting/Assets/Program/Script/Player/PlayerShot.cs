using System.Collections.Generic;
using UnityEngine;

namespace MainScene
{
    // 当たり判定用のMonoBehaviourのclass
    public class ShotCollisionHandler : MonoBehaviour
    {
        // 対応するPlayerShotを設定
        private PlayerShot owner;    
        public void SetOwner(PlayerShot shot)
        {
            owner = shot;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 相手がTargetの場合消滅
            if (collision.gameObject.CompareTag("Target"))
            {
                // PlayerShot側にあたったことを伝える
                owner.OnHit();
            }
        }
    }

    // 玉のクラス
    public class PlayerShot
    {
        // 玉のプレハブ
        private GameObject m_shotObj;

        // これ以上離れたら消す
        private const float m_maxDistance = 50.0f;
        // 初期位置
        private Vector3 m_startPos = Vector3.zero;

        // 削除フラグ
        private bool m_isDestroyed = false;

        public PlayerShot(GameObject prefab, Vector3 argStartPos, Vector3 argTargetPos, float argSpeed)
        {
            // 生成
            m_shotObj = Object.Instantiate(prefab);
            // 初期位置
            m_shotObj.transform.position = argStartPos;
            // 初期位置を覚える
            m_startPos = m_shotObj.transform.position;


            // 当たり判定用のコンポーネント追加
            var handler = m_shotObj.AddComponent<ShotCollisionHandler>();
            handler.SetOwner(this);


            // 発射座標から狙う位置への方向ベクトル
            var dir = (argTargetPos - m_startPos).normalized;
            // その方向に力を加える
            var rb = m_shotObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
               rb.AddForce(dir * argSpeed);
            }
        }

        public void Update()
        {
            // 一定距離で削除
            float dist = Vector3.Distance(m_startPos, m_shotObj.transform.position);
            if (dist > m_maxDistance)
            {
                DestroySelf();
            }
        }

        // 当たった時
        public void OnHit()
        {
            DestroySelf();           
        }

        // 自分を削除
        private void DestroySelf()
        {
            if (m_isDestroyed == false)
            {
                Object.Destroy(m_shotObj);
                m_isDestroyed = true;
            }
        }

        public bool isDestroyed()
        {
            return m_isDestroyed;
        }

    } // class
} // namespace