using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CountingPrototype
{
    public class CountingBox : MonoBehaviour
    {
        [SerializeField] string boxName = "";
        [SerializeField] int boxScoreMultiplier = 1;
        [SerializeField] int boxTotalScore = 0;
        // [SerializeField] TextMeshProUGUI ballNumText = null;
        [SerializeField] TextMeshProUGUI scoreText = null;
        [SerializeField] TextMeshProUGUI ballText = null;
        [SerializeField] TextMeshProUGUI multiplierText = null;
        CountingManager countingManager = null;
        int ballNum = 0;

        List<GameObject> ballsInBox;
        void Start()
        {
            countingManager = GameObject.FindObjectOfType<CountingManager>();

            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.OnNextLevel += Reset;

            multiplierText.text = "X " + boxScoreMultiplier;

            // ballNumText.text = boxName + ":" + ballNum;
            UpdateUI();
            ballsInBox = new List<GameObject>();


        }

        private void UpdateUI()
        {
            ballText.text = ballNum.ToString();
            scoreText.text = boxTotalScore.ToString();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {

                int ballScore = other.GetComponent<Ball>().GetBaseScore() * boxScoreMultiplier;
                countingManager.UpdateScore(ballScore);
                other.GetComponent<Collider>().material.bounciness = 0.1f;
                other.GetComponent<Rigidbody>().mass = 100;
                other.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;


                ballNum++;
                boxTotalScore += ballScore;

                ballsInBox.Add(other.gameObject);

                UpdateUI();

            }
        }

        public void Reset()
        {
            boxTotalScore = 0;
            ballNum = 0;

            foreach (GameObject ball in ballsInBox)
            {
                if (ball != null)
                {
                    Destroy(ball);
                }
            }
            ballsInBox = new List<GameObject>();
            UpdateUI();
        }
    }
}


