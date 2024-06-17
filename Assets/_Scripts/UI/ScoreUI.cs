using TMPro;
using UnityEngine;

namespace CountingPrototype.UI
{
    public class ScoreUI : MonoBehaviour
    {
        CountingManager countingManager;
        [SerializeField] TextMeshProUGUI yourScoreText = null;
        [SerializeField] TextMeshProUGUI targetScoreText = null;

        GameManager gameManager;

        void OnEnable()
        {
            countingManager = GameObject.FindObjectOfType<CountingManager>();
            gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.OnNextLevel += ResetUI;

            countingManager.OnScoreChange += UpdateUI;
        }
        void Start()
        {

            ResetUI();
        }

        private void ResetUI()
        {
            targetScoreText.text = "Target Score:" + gameManager.CurrentTargetScore;
            yourScoreText.text = "Your Score:" + 0;
        }

        void UpdateUI()
        {
            yourScoreText.text = "Your Score:" + countingManager.GetCurrentScore();
        }
    }
}


