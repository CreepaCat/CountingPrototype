using System;
using UnityEngine;

namespace CountingPrototype
{
    public class CountingManager : MonoBehaviour
    {

        [SerializeField] int currentScore = 0;

        public event Action OnScoreChange;
        SpawnManager spawnManager;
        void Start()
        {
            spawnManager = GameObject.FindObjectOfType<SpawnManager>();
            GameObject.FindObjectOfType<GameManager>().OnNextLevel += Reset;

        }
        public int GetCurrentScore() => currentScore;



        public void UpdateScore(int boxScore)
        {
            currentScore += boxScore;
            OnScoreChange?.Invoke();

        }

        public void Reset()
        {
            currentScore = 0;
        }
    }
}


