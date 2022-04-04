using Assets.Inventory.Scripts;
using Assets.Tower;
using System;
using UnityEngine;

namespace Assets.GameManagement
{
    public interface IGameManager
    {
        IInventoryBag InventoryBag { get; }
        IFloodLevel FloodLevel { get; }
        ITowerHeightDetector TowerHeightDetector { get; }

        event Action OnGameStart;
        event Action OnGameOver;

        void ChangeTowerHeight(Vector3 newTowerItemPosition);
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

        [SerializeField] FloodLevelManager floodLevel;
        public IFloodLevel FloodLevel => floodLevel;


        [SerializeField] TowerHeightDetector towerHeightDetector;
        public ITowerHeightDetector TowerHeightDetector => towerHeightDetector;


        private bool readyToStart;
        private bool gameOver;

        private void Start()
        {
            readyToStart = true;
            gameOver = false;
        }

        private void Update()
        {
            if (readyToStart)
            {
                readyToStart = false;
                StartGameplay();
            }

            if (!gameOver)
            {
                if (FloodLevel.FloodHeight > towerHeightDetector.TowerHeight)
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

        public void ChangeTowerHeight(Vector3 newTowerItemPosition)
        {
            TowerHeightDetector.RecalculateHeight(newTowerItemPosition);
        }
    }
}
