using UnityEngine;

namespace CountingPrototype
{
    public class TopCub : MonoBehaviour
    {
        [SerializeField] float force = 2f;
        void Start()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Rigidbody ballRB = other.gameObject.GetComponent<Rigidbody>();
                ballRB.AddForce(transform.right * force, ForceMode.Impulse);
            }
        }
    }
}


