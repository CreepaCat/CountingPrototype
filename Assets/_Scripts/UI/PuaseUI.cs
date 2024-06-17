using UnityEngine;

namespace CountingPrototype
{
    public class PuaseUI : MonoBehaviour
    {

        [SerializeField] GameObject tipsUI;
        void Start()
        {

        }

        public void ShowTips()
        {
            bool isActive = tipsUI.activeSelf;
            Debug.Log("tipsUI.activeSelf" + tipsUI.activeSelf);
            tipsUI.SetActive(!isActive);
        }
    }
}


