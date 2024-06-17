using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CountingPrototype
{
    public class ModeUI : MonoBehaviour
    {
        [SerializeField] Button buttonLeft = null;
        [SerializeField] Button buttonRight = null;
        [SerializeField] TextMeshProUGUI modeText = null;
        public int currentMode = 1;

        void Start()
        {

            buttonLeft.onClick.AddListener(() => { ChangeMode(-1); });
            buttonRight.onClick.AddListener(() => { ChangeMode(1); });

            currentMode = PlayerSetting.Instance.gameMode;
            UpdateModeText();
        }

        void ChangeMode(int valueToChange)
        {
            currentMode += valueToChange;
            if (currentMode > 3)
            {
                currentMode -= 3;
            }
            else if (currentMode < 1)
            {
                currentMode += 3;
            }

            UpdateModeText();

            PlayerSetting.Instance.gameMode = currentMode;
        }

        private void UpdateModeText()
        {
            switch (currentMode)
            {
                case 1:
                    modeText.text = "Normal";
                    break;
                case 2:
                    modeText.text = "Disturbance";
                    break;
                case 3:
                    modeText.text = "Crazy";
                    break;
                default:
                    break;
            }
        }
    }
}


