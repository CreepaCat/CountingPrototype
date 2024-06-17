using UnityEngine;

namespace CountingPrototype
{

    public class Pillar : MonoBehaviour
    {

        [SerializeField] bool isSpecial = true;


        [SerializeField] float bounceForce = 1;
        public bool IsSpecial => isSpecial;

        public void SetPillar(float bounceForce)
        {
            this.bounceForce = bounceForce;
        }
        public void SetBounceForce(float bounceForce)
        {
            this.bounceForce = bounceForce;
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Vector3 bounceDirect = (other.transform.position - transform.position).normalized;
                other.transform.GetComponent<Rigidbody>().AddForce(bounceDirect * bounceForce, ForceMode.Impulse);
            }
        }


    }
}


