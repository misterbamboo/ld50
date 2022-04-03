using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagement
{

    public interface IFloodLevel
    {
        float FloodHeight { get; }
    }

    public class FloodLevelManager : MonoBehaviour, IFloodLevel
    {
        [SerializeField] float floodRaiseSpeed = 0.1f;
        public float FloodHeight { get; private set; }
        private bool floodRaise;

        void Start()
        {
            floodRaise = false;
            GameManager.Instance.OnGameStart += Instance_OnGameStart;
        }

        private void Instance_OnGameStart()
        {
            floodRaise = true;
        }

        void Update()
        {
            if (floodRaise)
            {
                FloodHeight += floodRaiseSpeed * Time.deltaTime;
            }
        }
    }
}
