using System.Collections.Generic;
using UnityEngine;


namespace MainScene
{
    // 玉のマネージャークラス
    public class ShotManager
    {
        // リストにする
        private List<PlayerShot> m_listShots;

        // 玉のプレハブ
        private GameObject m_shotObj;

        // 玉のスピード
        private float m_speed = 1.0f;

        // 玉の発射位置
        Transform m_startPos;

        // コンストラクタ
        public ShotManager(GameObject argPrefab, float argSpeed, Transform argStartPos)
        {
            // リストを生成
            m_listShots = new List<PlayerShot>();
            
            m_shotObj = argPrefab;

            m_speed = argSpeed;

            m_startPos = argStartPos;
        }

        public void Update()
        {
            // クリックで玉を生成
            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }

            // 玉の更新処理を回す
            foreach (var playerShot in m_listShots)
            {
                playerShot.Update();
            }
        }

        // 玉を生成
        private void Shot()
        {
            m_listShots.Add(new PlayerShot(m_shotObj, m_speed, m_startPos.position));
        }

    } // class
} // namespace