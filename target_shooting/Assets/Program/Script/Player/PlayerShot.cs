using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainScene
{
    // 球のクラス
    public class PlayerShot
    {
        // 球のプレハブ
        private GameObject m_shotObj;

        // これ以上離れたら消す
        private const float m_maxDistance = 50.0f;
        // 初期位置
        private Vector3 m_startPos = Vector3.zero;

        public PlayerShot(GameObject prefab, Vector3 argStartPos, Vector3 argTargetPos, float argSpeed)
        {
            // 生成
            m_shotObj = UnityEngine.Object.Instantiate(prefab);
            // 初期位置
            m_shotObj.transform.position = argStartPos;
            // 初期位置を覚える
            m_startPos = m_shotObj.transform.position;

            // 削除用のコンポーネント
            m_shotObj.AddComponent<ShotDestroyer>();

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
            // 削除済みなら処理しない
            if (m_shotObj == null) return;

            // 最大距離チェック
            float dist = Vector3.Distance(m_startPos, m_shotObj.transform.position);
            if (dist > m_maxDistance)
            {
                // ShotDestroyerを通じて削除
                m_shotObj.GetComponent<ShotDestroyer>()?.SelfDestroy();
            }
        }

        // PlayerShot側から削除状態を見れるように
        public bool IsDestroyed()
        {
            // m_shotObjがnullなら破棄済みとみなす
            return m_shotObj == null;
        }
    } // class
} // namespace 