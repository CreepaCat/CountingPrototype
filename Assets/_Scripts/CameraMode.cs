using UnityEngine;

namespace CountingPrototype
{
    public class CameraMode : MonoBehaviour
    {
        [SerializeField] bool isPerspective = false;
        [SerializeField] Renderer background;

        public void ChangeMode()
        {

            Camera.main.orthographic = !Camera.main.orthographic;

            if (Camera.main.orthographic)
            {
                background.material.SetTextureScale("_MainTex", new Vector2(100, 100));

            }
            else
            {
                background.material.SetTextureScale("_MainTex", new Vector2(10, 10));
            }


        }


    }
}


