using System.IO;
using UnityEngine;

namespace CountingPrototype
{
    public class PlayerSetting : MonoBehaviour
    {

        public static PlayerSetting Instance = null;

        public string currentPlayerName = "";
        public int gameMode = 1;
        public bool is2DCamera = false;

        //best record
        public int bestScore = 300;
        public string bestPlayerName = "Allen";


        string savePath;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/saveFile.json";

            //初始时加载
            LoadPlayerSettings();
        }

        [System.Serializable]
        class PlayerSettingData
        {
            public string currentPlayerName;
            public int gameMode;
            public bool is2DCamera;

            public int bestScore = 300;
            public string bestPlayerName = "Allen";
        }

        public void SavePlayerSettings()
        {

            PlayerSettingData data = new PlayerSettingData();
            data.currentPlayerName = currentPlayerName;
            data.gameMode = gameMode;
            data.is2DCamera = is2DCamera;

            data.bestPlayerName = bestPlayerName;
            data.bestScore = bestScore;
            // string path = Application.persistentDataPath + "saveFile.json";

            File.WriteAllText(savePath, JsonUtility.ToJson(data));

            Debug.Log("Save to " + savePath);
        }

        public void LoadPlayerSettings()
        {
            //先检查文件是否存在
            if (File.Exists(savePath))
            {
                PlayerSettingData data = JsonUtility.FromJson<PlayerSettingData>(File.ReadAllText(savePath));
                this.currentPlayerName = data.currentPlayerName;
                this.gameMode = data.gameMode;
                this.is2DCamera = data.is2DCamera;

                this.bestScore = data.bestScore;
                this.bestPlayerName = data.bestPlayerName;

                Debug.Log("Load file " + savePath);

            }
            else
            {
                Debug.Log("不存在文件路径 " + savePath);

            }

        }

        public void UpdateRecord(int score)
        {
            bestPlayerName = currentPlayerName;
            bestScore = score;

            SavePlayerSettings();
        }

    }
}


