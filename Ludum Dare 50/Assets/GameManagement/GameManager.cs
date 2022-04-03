using Assets.Inventory.Scripts;
using System;
using UnityEngine;

namespace Assets.GameManagement
{
    public interface IGameManager
    {
        IInventoryBag InventoryBag { get; }
        event Action OnGameStart;
        event Action OnGameOver;
    }

    public class GameManager : MonoBehaviour, IGameManager
    {
        public static IGameManager Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }

        public event Action OnGameStart;
        public event Action OnGameOver;

        [SerializeField] InventoryBag inventoryBag;
        public IInventoryBag InventoryBag => inventoryBag;

        private bool readyToStart;
        private bool gameOver;

        private void Start()
        {
            readyToStart = true;
            gameOver = false;
        }

        private float temp_count_down = 5;
        private void Update()
        {
            if (readyToStart)
            {
                readyToStart = false;
                StartGameplay();
            }

            if (!gameOver)
            {
                temp_count_down -= Time.deltaTime;
                if (temp_count_down < 0)
                {
                    gameOver = true;
                    TriggerGameOver();
                }
            }
        }

        private void StartGameplay()
        {
            OnGameStart?.Invoke();
        }

        private void TriggerGameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}
