using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CountingPrototype
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int currentTargetScore = 0;
        [SerializeField] int levelScoreIncrease = 100;
        [SerializeField] int ballRemain = 0;

        [SerializeField] GameObject distributeMachine;


        [SerializeField] GameObject startPanel;
        [SerializeField] GameObject gamingPanel;
        [SerializeField] GameObject gamePausePanel;

        [SerializeField] GameObject levelSuccess;
        [SerializeField] GameObject levelFailure;

        SpawnManager spawnManager;
        CountingManager countingManager;

        int currentLevel = 1;
        int currentMode = 1;
        int ballInMachineBody = 0;

        public int CurrentTargetScore => currentTargetScore;
        public int CurrentLevel => currentLevel;
        public int CurrentMode => currentMode;

        public event Action OnNextLevel;

        bool isGamePaused = false;
        bool isGameActive = false;

        public bool IsGamePaused => isGamePaused;
        public bool IsGameActive => isGameActive;



        // [SerializeField]
        void Start()
        {
            spawnManager = GameObject.FindObjectOfType<SpawnManager>();

            countingManager = GameObject.FindObjectOfType<CountingManager>();

            countingManager.OnScoreChange += OnScoreChange;
            ballRemain = spawnManager.BallRemain();

            levelSuccess.SetActive(false);
            levelFailure.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //暂停
                if (!isGamePaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }


            }
        }

        public void SetMode(int modeIndex)
        {
            currentMode = modeIndex;
            currentTargetScore = 100; //初始目标分

            startPanel.SetActive(false);
            gamingPanel.SetActive(true);

            distributeMachine.SetActive(true);
            isGameActive = true;
            OnNextLevel?.Invoke();
        }


        public void NextLevel()
        {
            currentLevel++;
            currentTargetScore += levelScoreIncrease * currentMode;

            levelSuccess.SetActive(false);

            OnNextLevel?.Invoke();
        }

        public void UpdateBallInMachineBody(int ballNum)
        {
            ballInMachineBody = ballNum;
            CheckLevelEnd();

        }

        public void PauseGame()
        {
            isGamePaused = true;
            Time.timeScale = 0;
            gamePausePanel.SetActive(true);


        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1;
            gamePausePanel.SetActive(false);
        }

        public void Restart()
        {
            isGamePaused = false;
            Time.timeScale = 1;//恢复时间
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }


        void OnScoreChange()
        {
            ballRemain = spawnManager.BallRemain();
            //当无剩余小球时，计算是否过关
            CheckLevelEnd();
        }

        private void CheckLevelEnd()
        {
            if (ballRemain > 0 || ballInMachineBody > 0) return;

            int finalScore = countingManager.GetCurrentScore();
            if (finalScore >= currentTargetScore)
            {
                //下一关
                levelSuccess.SetActive(true);
            }
            else
            {
                //Game Over
                levelFailure.SetActive(true);
            }
        }
    }
}


