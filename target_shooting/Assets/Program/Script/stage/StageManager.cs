using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MainScene
{
    public class StageManager
    {
        // 柱の数
        private const int WALL_MAX = 5;
        // 的の数
        private const int TARGET_MAX = 5;
        // 合わせた数
        private const int TOTAL_MAX = TARGET_MAX + WALL_MAX;

        // 横幅
        // 最小値
        private const float RANGE_X_MIN = -8.0f;
        // 最大値
        private const float RANGE_X_MAX = 8.0f;
        // 合計幅
        private const float RANGE_X_WIDTH = RANGE_X_MAX - RANGE_X_MIN;
        // ずらす幅
        private const float SHIFT_X_WIDTH = RANGE_X_WIDTH / TOTAL_MAX;

        // 奥行のランダム制限
        private const float RANGE_Z_MIN = 10.0f;
        private const float RANGE_Z_MAX = 30.0f;

        // 最小距離
        private const float MIN_DISTANCE = 1.0f;

        // 生成するオブジェクトのデータ
        private struct SpawnData
        {
            // プレハブ
            public GameObject prefab;
            // 座標
            public Vector3 position;

            public SpawnData(GameObject prefab, Vector3 position)
            {
                this.prefab = prefab;
                this.position = position;
            }
        }

        private List<SpawnData> m_spawnList;

        private List<Wall> m_wallObj;
        private List<Target> m_targetObj;

        // 生成座標の記録
        private List<Vector3> m_spawnedPositions;

        // コンストラクタ
        public StageManager(GameObject wallPrefab, GameObject targetPrefab)
        {
            m_spawnList = new List<SpawnData>();
            m_wallObj = new List<Wall>();
            m_targetObj = new List<Target>();
            m_spawnedPositions = new List<Vector3>();


            GenerateStage(wallPrefab, targetPrefab);
            GenerateObjects();
        }

        public void Update()
        {
            // 的の削除処理を回す
            for (int i = 0; i < m_targetObj.Count; ++i)
            {
                // 削除
                if (m_targetObj[i].isDestroyed())
                {
                    m_targetObj.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// ステージを作る
        /// </summary>
        private void GenerateStage(GameObject wallPrefab, GameObject targetPrefab)
        {
            // 各オブジェクトを追加する
            List<GameObject> objectsToSpawn = new List<GameObject>();
            for (int i = 0; i < WALL_MAX; i++)
            {
                objectsToSpawn.Add(wallPrefab);
            }
            for (int i = 0; i < TARGET_MAX; i++)
            {
                objectsToSpawn.Add(targetPrefab);
            }

            // 追加したものの順番を混ぜる
            Shuffle(objectsToSpawn);

            // 割り当て用のX座標
            float x = RANGE_X_MIN;

            // 順番に座標を設定していく
            foreach (var prefab in objectsToSpawn)
            {
                // ランダムな座標取得
                Vector3? pos = RandomPosition(x);

                // ずらす
                x += SHIFT_X_WIDTH;

                // 座標が割り当てられていたら
                if (pos.HasValue)
                {
                    // オブジェクト追加
                    m_spawnList.Add(new SpawnData(prefab, pos.Value));
                }
            }
        }

        /// <summary>
        /// SpawnListを元に障害物を生成する
        /// </summary>
        private void GenerateObjects()
        {
            // タグで生成するprefabを分ける
            foreach (var data in m_spawnList)
            {
                if (data.prefab.CompareTag("Wall"))
                {
                    m_wallObj.Add(new Wall(data.prefab, data.position));
                }
                else if (data.prefab.CompareTag("Target"))
                {
                    m_targetObj.Add(new Target(data.prefab, data.position));
                }
                else
                {
                    Debug.LogWarning($"未対応のタグです: {data.prefab.tag}");
                }
            }
        }

        /// <summary>
        /// ランダム位置取得
        /// </summary>
        private Vector3? RandomPosition(float x)
        {
            int missCount = 0;

            while (true)
            {
                // ランダム座標設定
                float posX = x;
                float posZ = Random.Range(RANGE_Z_MIN, RANGE_Z_MAX);
                Vector3 newPos = new Vector3(posX, -5.0f, posZ);
     
                // その位置がほかの障害物と重なってないか
                if (CheckPosition(newPos))
                {
                    m_spawnedPositions.Add(newPos);
                    return newPos;
                }

                // 50回以上重なっていたら
                missCount++;
                if (missCount > 50)
                {
                    // あきらめる
                    Debug.LogWarning("適切な位置が見つかりませんでした");
                    return null;
                }
            }
        }

        /// <summary>
        /// 既存の座標と近すぎないかチェック
        /// </summary>
        private bool CheckPosition(Vector3 newPos)
        {
            foreach (Vector3 existingPos in m_spawnedPositions)
            {
                if (Vector3.Distance(newPos, existingPos) < MIN_DISTANCE)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// リストをシャッフルする
        /// </summary>
        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    } // class
} // namespace