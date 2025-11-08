
using System.Collections.Generic;
using UnityEngine;

namespace MainScene
{
    // 当たり判定用のものビヘイビアのclass


    // 玉のクラス
    public class PlayerShot
    {
        GameObject m_shotObj;

        public PlayerShot(GameObject obj)
        {
            m_shotObj = obj;
        }

        public void Update()
        {
        }
    }

    // 玉のマネージャークラス
    public class PlayerShotManager
    {
        // リストにする
        List<PlayerShot> m_listShots;

        // コンストラクタ
        public PlayerShotManager(GameObject obj)
        {
            // サイズ分生成
            m_listShots = new List<PlayerShot>();

            ////// 後でクリックしたらに帰る

            // インスタンス生成
            m_listShots.Add(new PlayerShot(obj));
            // インスタンス削除
            m_listShots.Remove(m_listShots[0]);
        }

        public void Update()
        {
            // 玉の更新処理を回す
            foreach (var playerShot in m_listShots)
            {
                playerShot.Update();
            }
        }
    }
}