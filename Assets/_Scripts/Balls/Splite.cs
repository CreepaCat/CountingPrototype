using UnityEngine;

namespace CountingPrototype
{
    public class Splite : MonoBehaviour
    {

        [SerializeField] int collisionTimesToSplit = 3;
        [SerializeField] int collisionCounter = 0;
        [SerializeField]

        public void Reset()
        {
            collisionCounter = 0;
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Pillar"))
            {
                collisionCounter++;
                if (collisionCounter == collisionTimesToSplit)
                {
                    Debug.Log("产生分裂球体");
                    Reset();
                    Splite newSplit = Instantiate(this, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                    newSplit.Reset();

                    // collisionCounter = 0;
                }

            }
        }
    }
}


