using System.Collections.Generic;
using UnityEngine;


namespace MainScene
{
    
    /// <summary>
    /// 球のマネージャークラス 
    /// </summary>
    public class ShotManager
    {
        // リスト
        private List<PlayerShot> m_listShots;
        // プレハブ
        private GameObject m_shotObj;
        // スピード
        private float m_speed = 1.0f;
        // 発射位置
        Transform m_startPos;

        // メインカメラ
        Camera m_camera;

        // コンストラクタ
        public ShotManager(GameObject argPrefab, float argSpeed, Transform argStartPos)
        {
            m_listShots = new List<PlayerShot>();
            m_shotObj = argPrefab;
            m_speed = argSpeed;
            m_startPos = argStartPos;
            m_camera = Camera.main;
        }

        public void Update()
        {
            // クリックで球を生成
            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }

            // 球の更新処理を回す
            for (int i = 0; i < m_listShots.Count; ++i)
            {
                var playerShot = m_listShots[i];
                playerShot.Update();

                // 球がnullなら削除
                if (playerShot.IsDestroyed())
                {
                    m_listShots.RemoveAt(i);
                    i--;
                }
            }
        }

        // 球を生成
        private void Shot()
        {
            // マウス座標を取得
            var mousePos = Input.mousePosition;
            // 奥行きを適当に設定
            mousePos.z = 10.0f;
            // その座標を3D空間上に変換
            var targetPos = m_camera.ScreenToWorldPoint(mousePos);
            // 生成
            m_listShots.Add(new PlayerShot(m_shotObj, m_startPos.position, targetPos, m_speed));
        }

    } // class
} // namespace