using UnityEngine;

namespace MainScene
{
    public class MainScene : MonoBehaviour
    {
        // 玉のプレハブ
        [SerializeField]
        private GameObject m_ballObj;
        // 玉のスピード
        [SerializeField]
        private float m_speed = 1.0f;
        // 玉の発射位置
        [SerializeField]
        private Transform m_shotStartPos;
        // 玉の管理クラス
        private ShotManager m_shotManager;

        // 壁のプレハブ
        [SerializeField]
        private GameObject m_wallObj;
        // 壁の管理クラス
        private WallManager m_wallManager;

        // 的のプレハブ
        [SerializeField]
        private GameObject m_targetObj;
        // 的の管理クラス
        private WallManager m_targetManager;

        void Start()
        {
            m_shotManager =  new ShotManager(m_ballObj, m_speed, m_shotStartPos);
            m_wallManager = new WallManager(m_wallObj);
        }

        void Update()
        {
            m_shotManager.Update();

        }

    } // class

} // namespace