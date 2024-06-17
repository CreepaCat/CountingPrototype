using System.IO;
using UnityEngine;

namespace CountingPrototype
{
    public class PlayerSetting : MonoBehaviour
    {

        public static PlayerSetting Instance = null;

        public string playerName = "";
        public int gameMode = 1;
        public bool is2DCamera = false;
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
            public string playerName;
            public int gameMode;
            public bool is2DCamera;
        }

        public void SavePlayerSettings()
        {

            PlayerSettingData data = new PlayerSettingData();
            data.playerName = playerName;
            data.gameMode = gameMode;
            data.is2DCamera = is2DCamera;
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
                this.playerName = data.playerName;
                this.gameMode = data.gameMode;
                this.is2DCamera = data.is2DCamera;
                Debug.Log("Load file " + savePath);

            }
            else
            {
                Debug.Log("不存在文件路径 " + savePath);

            }




        }

    }
}


