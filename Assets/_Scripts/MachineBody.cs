using UnityEngine;

namespace CountingPrototype
{
    public class MachineBody : MonoBehaviour
    {

        int ballInBody = 0;

        public int BallInBody => ballInBody;


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                ballInBody++;
                GameObject.FindObjectOfType<GameManager>().UpdateBallInMachineBody(ballInBody);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                ballInBody--;

                GameObject.FindObjectOfType<GameManager>().UpdateBallInMachineBody(ballInBody);

            }
        }
    }
}


