using UnityEngine;

namespace MainScene
{
    public class MainScene : MonoBehaviour
    {
        [SerializeField]
        GameObject m_ballObj;

        PlayerShotManager playerShotManager;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerShotManager = GetComponent<PlayerShotManager>();
        }

        // Update is called once per frame
        void Update()
        {
            playerShotManager.Update();
        }

    } // class

} // namespace