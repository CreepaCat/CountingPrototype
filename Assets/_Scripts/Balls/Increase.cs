using UnityEngine;

namespace CountingPrototype
{
    public class Increase : MonoBehaviour
    {

        [SerializeField] int collisionTimesToIncrease = 5;

        int collisionCounter = 0;

        [SerializeField] int increaseValue = 1;
        [SerializeField] GameObject increaseUIPrefab = null;

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Pillar"))
            {
                collisionCounter++;
                if (collisionCounter >= collisionTimesToIncrease)
                {
                    GetComponent<Ball>().IncreaseBaseScore(increaseValue);
                    Instantiate(increaseUIPrefab, transform.position, Quaternion.identity);
                    collisionCounter = 0;
                }

            }
        }
    }
}


