
using System.Collections.Generic;
using UnityEngine;

namespace MainScene
{
    // 当たり判定用のものビヘイビアのclass


    // 玉のクラス
    public class PlayerShot
    {
        // 玉のプレハブ
        private GameObject m_shotObj;

        // 玉のスピード
        private float m_speed = 1.0f;

        // これ以上離れたら消す
        private const float m_maxDistance = 50.0f;

        // 初期位置
        private Vector3 m_startPos = Vector3.zero;

        public PlayerShot(GameObject prefab, float argSpeed, Vector3 argStartPos)
        {
            // 生成
            m_shotObj = Object.Instantiate(prefab);
            // 初期位置
            m_shotObj.transform.position = argStartPos;

            // 初期位置を覚える
            m_startPos = m_shotObj.transform.position;

            // スピードを適応
            m_speed = argSpeed;

        }

        public void Update()
        {
            // nullチェック
            if (m_shotObj == null) return;

            // 玉の移動
            m_shotObj.transform.position += Vector3.forward * m_speed * Time.deltaTime;

            // 一定距離で削除
            float dist = Vector3.Distance(m_startPos, m_shotObj.transform.position);
            if (dist > m_maxDistance)
            {
                Object.Destroy(m_shotObj);
            }
        }

    } // class
} // namespace