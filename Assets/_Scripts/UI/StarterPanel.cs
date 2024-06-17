using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace CountingPrototype
{
    public class StarterPanel : MonoBehaviour
    {
        [SerializeField] TMP_InputField playerNameInput = null;
        [SerializeField] Toggle camera2DMode = null;
        [SerializeField] Button startButton = null;
        [SerializeField] Button quitButton = null;


        [SerializeField] bool is2DCamera = false;
        void Start()
        {
            LoadPlyerSettings();

            camera2DMode.onValueChanged.AddListener((isOn) => { OnToggle2DCamera(isOn); });
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
            //is2DCamera =
        }



        void OnDisable()
        {
            camera2DMode.onValueChanged.RemoveAllListeners();
            startButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

        void Update()
        {
            if (string.IsNullOrWhiteSpace(playerNameInput.text))
            {
                startButton.interactable = false;
            }
            else
            {
                startButton.interactable = true;
            }
        }

        private void OnToggle2DCamera(bool isOn)
        {
            is2DCamera = isOn;
        }

        public void StartGame()
        {
            SavePlayerSettings();
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {

            //退出时自动保存
            SavePlayerSettings();
#if UNITY_EDITOR

            EditorApplication.ExitPlaymode(); //编辑器退出
#else

            Application.Quit();  //APP退出
#endif
        }

        private void SavePlayerSettings()
        {
            PlayerSetting.Instance.currentPlayerName = playerNameInput.text;
            PlayerSetting.Instance.is2DCamera = is2DCamera;
            PlayerSetting.Instance.SavePlayerSettings();
        }

        private void LoadPlyerSettings()
        {
            PlayerSetting.Instance.LoadPlayerSettings();
            playerNameInput.text = PlayerSetting.Instance.currentPlayerName;
            camera2DMode.isOn = PlayerSetting.Instance.is2DCamera;
        }
    }
}


