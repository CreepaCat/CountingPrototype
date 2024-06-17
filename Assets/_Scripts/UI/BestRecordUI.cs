using TMPro;
using UnityEngine;

namespace CountingPrototype
{
    public class BestRecordUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI bestPlayerName = null;
        [SerializeField] TextMeshProUGUI bestScore = null;
        [SerializeField] TextMeshProUGUI currentPlayerName = null;
        void Start()
        {
            UpdateUI();

            GameObject.FindObjectOfType<GameManager>().OnRecordChanged += UpdateUI;
        }

        private void UpdateUI()
        {
            currentPlayerName.text = PlayerSetting.Instance.currentPlayerName;

            bestPlayerName.text = PlayerSetting.Instance.bestPlayerName + ":";
            bestScore.text = PlayerSetting.Instance.bestScore.ToString();
        }
    }
}


