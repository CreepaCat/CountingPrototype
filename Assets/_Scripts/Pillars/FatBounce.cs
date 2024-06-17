using UnityEngine;

namespace CountingPrototype
{
    public class FatBounce : MonoBehaviour
    {
        void Start()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Vector3 bounceDirect = (other.transform.position - transform.position).normalized;
                other.transform.GetComponent<Rigidbody>().AddForce(bounceDirect * 5, ForceMode.Impulse);
            }
        }
    }
}


