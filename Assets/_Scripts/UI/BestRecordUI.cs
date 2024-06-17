using TMPro;
using UnityEngine;

namespace CountingPrototype
{
    public class BestRecordUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI bestPlayerName = null;
        [SerializeField] TextMeshProUGUI currentPlayerName = null;
        void Start()
        {
            currentPlayerName.text = PlayerSetting.Instance.playerName;
        }

    }
}


