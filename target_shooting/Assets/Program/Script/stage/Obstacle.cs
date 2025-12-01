using UnityEngine;

namespace MainScene
{
    // 障害物の基底クラス
    public abstract class Obstacle
    {
        // 自オブジェクト
        public GameObject m_object;

        public Obstacle(GameObject prefab, Vector3 pos)
        {
            m_object = Object.Instantiate(prefab, pos, Quaternion.identity);
        }

        // オブジェクトを破棄する
        public void DestroyObject()
        {
            if (m_object != null)
            {
                UnityEngine.Object.Destroy(m_object);
                m_object = null;
            }
        }
    } // class
} // namespace