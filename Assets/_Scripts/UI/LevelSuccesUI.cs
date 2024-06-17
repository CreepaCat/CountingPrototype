using TMPro;
using UnityEngine;

namespace CountingPrototype
{
    public class LevelSuccesUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI msgText;
        // void Start()
        // {
        //     GameManager gameManager = GameObject.FindObjectOfType<GameManager>();

        //     msgText.text = gameManager.GetBallNumToAdd() + "special balls added to you!";
        // }

        void OnEnable()
        {
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();

            int ballNumToAdd = gameManager.CurrentMode;

            msgText.text = ballNumToAdd + " special balls added to you!";
        }


    }
}


