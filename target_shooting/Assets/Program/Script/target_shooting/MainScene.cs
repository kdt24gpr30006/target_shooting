using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene
{
    public class MainScene : MonoBehaviour
    {
        // シーンの種類
        private enum SceneState
        {
            SCENE_TITLE,
            SCENE_PLAY,
            SCENE_CLEAR,
        }

        // 現在のシーン状態
        [SerializeField]
        private SceneState m_nowState = SceneState.SCENE_TITLE;

        // 球のプレハブ
        [SerializeField]
        private GameObject m_ballObj;

        // 球のスピード
        [SerializeField]
        private float m_speed;

        // 球の発射位置
        [SerializeField]
        private Transform m_shotStartPos;

        // 壁のプレハブ
        [SerializeField]
        private GameObject m_wallObj;

        // 的のプレハブ
        [SerializeField]
        private GameObject m_targetObj;

        // タイトルの表示用のテキストオブジェクト
        [SerializeField]
        private Text m_titleTextObj;

        // スコアの表示用のテキストオブジェクト
        [SerializeField]
        private Text m_scoreTextObj;


        // 球の管理クラス
        private ShotManager m_shotManager;
        // ステージの管理クラス
        private StageManager m_stageManager;

        // タイトル用のクラス
        private Title m_title;

        // スコア表示用のテキスト管理クラス
        private ScoreText m_scoreText;

        void Start()
        {
            m_scoreText = new ScoreText(m_scoreTextObj);
            m_shotManager = new ShotManager(m_ballObj, m_speed, m_shotStartPos);
            m_stageManager = new StageManager(m_wallObj, m_targetObj, m_scoreText);
            m_title = new Title(m_titleTextObj);
        }

        void Update()
        {
            switch (m_nowState)
            {
                case SceneState.SCENE_TITLE:
                    if (m_title.TitleExit() == true)
                    {
                        m_stageManager.CreateStage();
                        m_title.ChangeActive(false);
                        m_scoreText.ChangeActive(true);
                        m_nowState = SceneState.SCENE_PLAY;
                        UnityEngine.Debug.Log("げーむすたーと");
                    }
                    break;
                case SceneState.SCENE_PLAY:

                    m_shotManager.Update();
                    m_stageManager.Update();

                    // クリア判定
                    if(m_stageManager.IsClear() == true)
                    {
                        m_scoreText.ChangeActive(false);
                        m_nowState = SceneState.SCENE_CLEAR;
                        UnityEngine.Debug.Log("げーむえんど");
                    }
                    break;
                case SceneState.SCENE_CLEAR:
                    UnityEngine.Debug.Log("くりあ");
                    m_title.ChangeActive(true);
                    m_nowState = SceneState.SCENE_TITLE;
                    break;
            }
        }
    } // class
} // namespace