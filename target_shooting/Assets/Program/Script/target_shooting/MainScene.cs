using UnityEngine;

namespace MainScene
{
    public class MainScene : MonoBehaviour
    {
        // 玉のプレハブ
        [SerializeField]
        GameObject m_ballObj;

        // 玉のスピード
        [SerializeField]
        private float m_speed = 1.0f;

        // 玉の発射位置
        [SerializeField]
        private Transform m_shotStartPos;

        // 玉の管理クラス
        ShotManager m_shotManager;

        void Start()
        {
            m_shotManager =  new ShotManager(m_ballObj, m_speed, m_shotStartPos);
        }

        void Update()
        {
            m_shotManager.Update();
        }

    } // class

} // namespace