using UnityEngine;

namespace MainScene
{
    // 的クラス
    public class Target : Obstacle
    {
        public Target(GameObject prefab, Vector3 pos)
            : base(prefab, pos)
        {
            // 高さをランダムに
            float posY = Random.Range(-30, 40);
            pos.y = posY / 10;

            instance.transform.position = pos;
        }

    } // class
}// namespace