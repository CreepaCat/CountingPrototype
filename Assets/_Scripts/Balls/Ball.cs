using UnityEngine;

namespace CountingPrototype
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] AudioClip hitSound;
        [SerializeField] int baseScore = 5;
        [SerializeField] float awayForce = 2f;
        AudioSource audioSource;
        float teleCoolDown = 1;
        float teleTimer = 0;



        void Start()
        {
            audioSource = Camera.main.GetComponent<AudioSource>();
        }

        void Update()
        {
            teleTimer += Time.deltaTime;
        }

        public void ResetTeleTimer()
        {
            teleTimer = 0;
        }

        public bool CanBeTeleported()
        {
            return teleTimer > teleCoolDown;
        }

        public void IncreaseBaseScore(int value)
        {
            baseScore += value;
        }

        public int GetBaseScore()
        {
            return baseScore;
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
                Vector3 awayDir = (otherRB.transform.position - transform.position).normalized;
                otherRB.AddForce(awayDir * awayForce, ForceMode.Impulse);
            }
        }
    }
}


