using UnityEngine;
using UnityEngine.UI;

namespace MainScene
{
    public class ScoreText
    {
        // テキスト
        private Text m_scoreText;

        // 最大数
        private int m_maxScore = 5;
        // 現在
        private int m_score = 5;

        public ScoreText(Text arg_scoreText)
        {
            m_scoreText = arg_scoreText;
            // 最初は表示しない
            m_scoreText.gameObject.SetActive(false);
            Init();
        }

        // 文字列を更新する
        private void UpdateScoreText()
        {
            m_scoreText.text = "残り的数 : " + m_score + "/" + m_maxScore;
        }

        // 的が破壊されたときスコアを増やす
        public void AddScore()
        {
            // スコア追加して反映
            m_score--;
            UpdateScoreText();
        }

        // 描画するか切り替える
        public void ChangeActive(bool arg_active)
        {
            m_scoreText.gameObject.SetActive(arg_active);
        }

        // 初期化
        public void Init()
        {
            m_score = 5;
            UpdateScoreText();
        }
    }
}
