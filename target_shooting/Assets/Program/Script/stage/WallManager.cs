using UnityEngine;

namespace MainScene 
{ 

    public class WallManager
    {
        // メンバ変数
        Wall[] m_wall;
        GameObject m_object;
        int m_index = 10;

        // コンストラクタ
        public WallManager(GameObject wall_obj)
        {
            // サイズ確保
            m_wall = new Wall[m_index];

            // 動的生成
            for (int i = 0; i < m_index; i++) {
                m_wall[i] = new Wall(wall_obj);
            }
        }

        // 更新処理
        public void Update()
        {
            // 更新処理
            for (int i = 0; i < m_index; i++) { 
                m_wall[i].Update();
            }
        }

    } // class

} // namespace