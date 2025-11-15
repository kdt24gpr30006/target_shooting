using UnityEngine;

namespace MainScene
{
    public class Wall
    {
        // 自動生成の範囲
        private const float RANGE_X_MIN = 10.0f;
        private const float RANGE_X_MAX = -10.0f;
        private const float RANGE_Z_MIN = 10.0f;
        private const float RANGE_Z_MAX = 50.0f;

        // 壁オブジェクト
        GameObject m_obj;

        public Wall(GameObject obj)
        {
            // 生成
            m_obj = Object.Instantiate(obj);

            // 座標をランダムに設定
            float pos_x = Random.Range(RANGE_X_MIN, RANGE_X_MAX);
            float pos_z = Random.Range(RANGE_Z_MIN, RANGE_Z_MAX);
            m_obj.transform.position = new Vector3(pos_x, 0.0f, pos_z);
        }
    } // class
}// namespace