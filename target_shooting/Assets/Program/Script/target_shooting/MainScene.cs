using UnityEngine;

namespace MainScene
{
    public class MainScene : MonoBehaviour
    {
        // 球のプレハブ
        [SerializeField]
        private GameObject m_ballObj;
        // 球のスピード
        [SerializeField]
        private float m_speed = 1.0f;
        // 球の発射位置
        [SerializeField]
        private Transform m_shotStartPos;
        // 球の管理クラス
        private ShotManager m_shotManager;

        // 壁のプレハブ
        [SerializeField]
        private GameObject m_wallObj;
        // 的のプレハブ
        [SerializeField]
        private GameObject m_targetObj;

        // ステージの管理クラス
        private StageManager m_stageManager;

        void Start()
        {
            m_shotManager =  new ShotManager(m_ballObj, m_speed, m_shotStartPos);
            m_stageManager = new StageManager(m_wallObj, m_targetObj);
        }

        void Update()
        {
            m_shotManager.Update();
            
        }

    } // class

} // namespace