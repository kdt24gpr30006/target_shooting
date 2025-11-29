using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene
{
    public class Title
    {
        private Text m_titleText;

        public Title(Text text)
        {
            m_titleText = text;
            m_titleText.text = "たいとるがめんだよー";
        }

        // タイトル画面を抜けるか
        public bool TitleExit()
        {
            // クリックしたら抜ける
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }

        // 描画するか切り替える
        public void ChangeActive(bool arg_active)
        {
            m_titleText.gameObject.SetActive(arg_active);
        }
    }
}
