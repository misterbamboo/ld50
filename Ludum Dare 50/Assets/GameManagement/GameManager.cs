using Assets.Inventory.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GameManagement
{
    public interface IGameManager
    {
        IInventoryBag InventoryBag { get; }
    }

    public class GameManager : MonoBehaviour, IGameManager
    {
        public static IGameManager Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] InventoryBag inventoryBag;
        public IInventoryBag InventoryBag => inventoryBag;
    }
}
