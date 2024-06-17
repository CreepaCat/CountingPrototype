using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CountingPrototype
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject[] ballPrefabs = null;
        // [SerializeField] Button ballSpawnButton = null;
        [SerializeField] Transform ballSpawnPos = null;
        [SerializeField] int playerBallNum = 10;

        int ballAmountRemain = 10;
        [SerializeField] float ballTorqueRange = 100;
        [SerializeField] TextMeshProUGUI ballsRemainText = null;

        List<GameObject> ballList;
        GameManager gameManager;

        //每过一关，添加一个特殊小球
        int specialBallNum = 0;
        int normalBallNum = 0;
        void OnEnable()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.OnNextLevel += Reset;
        }
        void Start()
        {
            // ballSpawnButton.onClick.AddListener(SpawnBall);
            // ballAmountRemain = playerBallNum;

        }

        void Update()
        {
            // SpawnBall();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!gameManager.IsGameActive) return;
                if (gameManager.IsGamePaused) return;
                if (ballAmountRemain <= 0) return;
                SpawnBall();
            }

            ballsRemainText.text = "BallsRemain: " + ballAmountRemain;
        }

        public void Reset()
        {
            ballAmountRemain = playerBallNum;
            specialBallNum = gameManager.CurrentLevel * gameManager.CurrentMode - 1;
            // if(playerBallNum > specialBallNum)
            normalBallNum = playerBallNum;

            ballAmountRemain = specialBallNum + normalBallNum;

            if (ballList == null) return;

            foreach (var ball in ballList)
            {
                Destroy(ball.gameObject);
            }
            ballList = null;
        }

        public int BallRemain()
        {
            return ballAmountRemain;
        }

        public void SpawnBall()
        {
            if (ballList == null)
            {
                ballList = new List<GameObject>();
            }

            //每过一关加入一个特殊小球

            int index = GetRandomIndex();

            GameObject ball = Instantiate(ballPrefabs[index], ballSpawnPos.position, Quaternion.identity);
            ballList.Add(ball);


            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            // ballRB.AddForce(Vector3.down * 10f, ForceMode.Impulse);
            ballRB.AddTorque(new Vector3(Random.Range(-ballTorqueRange, ballTorqueRange),
                                                  Random.Range(-ballTorqueRange, ballTorqueRange),
                                                  Random.Range(-ballTorqueRange, ballTorqueRange)),
                                                  ForceMode.Impulse);
            //     ballRB.AddTorque(new Vector3(0,
            //    0,
            //    Random.Range(-ballTorqueRange, ballTorqueRange)),
            //    ForceMode.Impulse);

            ballAmountRemain--;
        }

        private int GetRandomIndex()
        {
            int index = 0;
            if (specialBallNum > 0 && normalBallNum > 0)
            {
                index = Random.Range(0, ballPrefabs.Length);
                if (index == 0)
                {

                    normalBallNum--;
                }
                else
                {
                    specialBallNum--;
                }
            }
            else if (specialBallNum == 0)
            {
                index = 0;
            }
            else if (normalBallNum == 0)
            {
                index = Random.Range(1, ballPrefabs.Length);
            }

            return index;
        }
    }
}


