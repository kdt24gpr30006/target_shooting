using UnityEngine;

namespace MainScene
{
    /// <summary>
    /// ‹…‚ğÁ‚·‚½‚ß‚ÌƒNƒ‰ƒX
    /// </summary>
    public class ShotDestroyer : MonoBehaviour
    {
        // “I‚Æ“–‚½‚Á‚½
        public void DestroyShot()
        {
            UnityEngine.Object.Destroy(gameObject);
        }

        // ‹…‚ªÅ‘å‹——£‚ğ’´‚¦‚½
        public void SelfDestroy()
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}