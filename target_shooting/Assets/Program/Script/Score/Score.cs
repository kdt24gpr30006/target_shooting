using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text Scoretext;

    private int m_maxScore = 5;
    private int m_score = 5;

    void Start()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        Scoretext.text = "残り的数 : " + m_score + "/" + m_maxScore;
    }

    // 的が破壊されたときに呼ぶ
    public void AddScore()
    {
        // スコア追加して反映
        m_score--;
        UpdateScoreText();
    }
}
