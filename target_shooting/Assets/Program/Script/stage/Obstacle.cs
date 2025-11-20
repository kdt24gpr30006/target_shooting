using UnityEngine;

namespace MainScene
{
    // 障害物の基底クラス
    public abstract class Obstacle
    {
        // 自オブジェクト
        public GameObject instance;

        public Obstacle(GameObject prefab, Vector3 pos)
        {
            instance = Object.Instantiate(prefab, pos, Quaternion.identity);
        }

    } // class
} // namespace