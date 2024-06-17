using UnityEngine;

namespace CountingPrototype
{
    public class DestroyInTime : MonoBehaviour
    {
        [SerializeField] float timeToDestroy = 1;
        void Start()
        {
            Destroy(gameObject, timeToDestroy);
        }


    }
}


