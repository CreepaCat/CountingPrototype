using UnityEngine;

namespace CountingPrototype
{
    public class Teleport : MonoBehaviour
    {
        PillarManager pillarManager;
        void Start()
        {
            pillarManager = GameObject.FindObjectOfType<PillarManager>();
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                if (other.gameObject.GetComponent<Ball>().CanBeTeleported())
                {
                    Vector3 telePos = pillarManager.GetRandomTelePos(this);
                    other.transform.position = telePos;
                    other.gameObject.GetComponent<Ball>().ResetTeleTimer();
                }

            }
        }
    }
}


