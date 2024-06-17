using UnityEngine;

namespace CountingPrototype
{
    public class TickTockGameObject : MonoBehaviour
    {
        [SerializeField] float tickTockRate = 0.5f;
        //[SerializeField] float lastingTime = 0.5f;

        float tickTockTimer = 0;

        Collider goCollider;
        MeshRenderer meshRenderer;
        void Start()
        {
            goCollider = GetComponent<Collider>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            if (tickTockTimer > tickTockRate)
            {
                goCollider.enabled = !goCollider.enabled;
                meshRenderer.enabled = !meshRenderer.enabled;

                tickTockTimer = 0;
            }
            tickTockTimer += Time.deltaTime;
        }
    }
}


