using UnityEngine;


namespace MainScene
{

    public class Wall
    {
        
        public float range_x = 8.0f;
        public float range_z1 = 10.0f;
        public float range_z2 = 50.0f;

        GameObject m_obj;

        public void Update() {
            // “Á‚É‰½‚à‚µ‚È‚¢
        }

        public Wall(GameObject obj)
        {
            m_obj = obj;

            // À•W‚ğƒ‰ƒ“ƒ_ƒ€‚Éİ’è
            float pos_x = Random.Range(range_x * -1, range_x);
            float pos_z = Random.Range(range_z1, range_z2);
            m_obj.transform.position = new Vector3(pos_x, 0.0f, pos_z);

        }

    } // class


}// namespace