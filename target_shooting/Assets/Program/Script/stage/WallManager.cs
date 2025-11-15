using UnityEngine;

namespace MainScene 
{ 

    public class WallManager
    {
        // 壁の配列
        private Wall[] m_wall;
        // 壁の数
        private const int WALL_MAX = 10;

        // コンストラクタ
        public WallManager(GameObject wall_obj)
        {
            // サイズ確保
            m_wall = new Wall[WALL_MAX];

            // 生成
            for (int i = 0; i < WALL_MAX; i++) {
                m_wall[i] = new Wall(wall_obj);
            }
        }
    } // class
} // namespace