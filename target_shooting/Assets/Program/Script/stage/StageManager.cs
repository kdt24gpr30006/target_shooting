using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MainScene
{
    public class StageManager
    {
        private const int WALL_MAX = 5;   // 柱の数
        private const int TARGET_MAX = 5; // 的の数

        private const float RANGE_X_MIN = -8.0f;
        private const float RANGE_X_MAX = 8.0f;

        private const float RANGE_Z_MIN = 10.0f;
        private const float RANGE_Z_MAX = 35.0f;

        private const float MIN_DISTANCE = 2.0f; // 最小距離

        // 生成するオブジェクトのデータ
        private struct SpawnData
        {
            public GameObject prefab;
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

        /// <summary>
        /// 1. 置くものをシャッフル  
        /// 2. 各オブジェクトに位置を割り当て  
        /// → 生成予約リストに入れる
        /// </summary>
        private void GenerateStage(GameObject wallPrefab, GameObject targetPrefab)
        {
            List<GameObject> objectsToSpawn = new List<GameObject>();

            for (int i = 0; i < WALL_MAX; i++)
            {
                objectsToSpawn.Add(wallPrefab);
            }
            for (int i = 0; i < TARGET_MAX; i++)
            {
                objectsToSpawn.Add(targetPrefab);
            }

            Shuffle(objectsToSpawn);

            foreach (var prefab in objectsToSpawn)
            {
                Vector3? pos = RandomPosition();

                if (pos.HasValue)
                {
                    m_spawnList.Add(new SpawnData(prefab, pos.Value));
                }
            }
        }

        /// <summary>
        /// SpawnList を元に Wall/Target を生成する
        /// </summary>
        private void GenerateObjects()
        {
            foreach (var data in m_spawnList)
            {
                // タグで生成するprefabを分ける
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
                    Debug.LogWarning(
                        $"未対応のタグで生成しようとしました: {data.prefab.tag}"
                    );
                }
            }
        }

        /// <summary>
        /// ランダム位置取得
        /// </summary>
        private Vector3? RandomPosition()
        {
            float posX = Random.Range(RANGE_X_MIN, RANGE_X_MAX);
            float posZ = Random.Range(RANGE_Z_MIN, RANGE_Z_MAX);

            Vector3 newPos = new Vector3(posX, -5.0f, posZ);

            int a = 0;

            while (true)
            {
                if (CheckPosition(newPos))
                {
                    m_spawnedPositions.Add(newPos);
                    return newPos;
                }

                a++;
                if(a > 50)
                {
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