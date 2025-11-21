using UnityEngine;

namespace MainScene
{
    // 当たり判定用のMonoBehaviourのclass
    public class TargetCollisionHandler : MonoBehaviour
    {
        // 対応するTargetを設定
        private Target owner;
        public void SetOwner(Target target)
        {
            owner = target;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 相手がShotの場合消滅
            if (collision.gameObject.CompareTag("Shot"))
            {
                // PlayerShot側に衝突を送る
                owner.OnHit();
            }
        }
    }

    // 的クラス
    public class Target : Obstacle
    {
        // 削除フラグ
        private bool m_isDestroyed = false;

        public Target(GameObject prefab, Vector3 pos) : base(prefab, pos)
        {
            // 高さをランダムに
            float posY = Random.Range(-30, 40);
            pos.y = posY / 10;

            // 初期座標, 角度設定
            instance.transform.position = pos;
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);


            // 当たり判定用のコンポーネント追加
            var handler = instance.AddComponent<TargetCollisionHandler>();
            handler.SetOwner(this);
        }


        // 当たった時
        public void OnHit()
        {
            DestroySelf();
        }

        // 自分を削除
        private void DestroySelf()
        {
            if (m_isDestroyed == false)
            {
                Object.Destroy(instance);
                m_isDestroyed = true;
            }
        }

        public bool isDestroyed()
        {
            return m_isDestroyed;
        }


    } // class
}// namespace