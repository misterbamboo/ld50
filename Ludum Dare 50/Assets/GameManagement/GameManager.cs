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
        object InventoryBag { get; }
    }

    public class GameManager : MonoBehaviour, IGameManager
    {
        public static IGameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        public object InventoryBag => throw new NotImplementedException();
    }
}
