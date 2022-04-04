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
        [SerializeField] public float FloodHeight { get; private set; }
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
                FloodHeight  += GetFactor() * floodRaiseSpeed * Time.deltaTime;
            }
        }

        private float GetFactor() =>
            FloodHeight switch
            {
                var h when h < 2.0f => 0.1f,
                var h when h >= 2.0f && h < 20.0f => 1.0f,
                var h when h > 20.0f => h - 20.0f,
                _ => 1.0f
            };
    }
}
