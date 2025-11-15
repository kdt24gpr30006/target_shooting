
using System.Collections.Generic;
using UnityEngine;

namespace MainScene
{
    // 当たり判定用のものビヘイビアのclass
    public class CollisionHandler : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            // 衝突したときの処理
            
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

        // ShotManagerの参照
        private ShotManager m_shotManager;  

        // 削除フラグ
        bool m_isDestroyed = false;

        public PlayerShot(GameObject prefab, Vector3 argStartPos, Vector3 argTargetPos, float argSpeed, ShotManager shotManager)
        {
            // 生成
            m_shotObj = Object.Instantiate(prefab);
            // 初期位置
            m_shotObj.transform.position = argStartPos;
            // 初期位置を覚える
            m_startPos = m_shotObj.transform.position;

            // 発射座標から狙う位置への方向ベクトル
            var dir = (argTargetPos - m_startPos).normalized;
            // その方向に力を加える
            var rb = m_shotObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
               rb.AddForce(dir * argSpeed);
            }

            m_shotManager = shotManager;
        }

        public void Update()
        {

            // 一定距離で削除
            float dist = Vector3.Distance(m_startPos, m_shotObj.transform.position);
            if (dist > m_maxDistance)
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