using Assets.GameManagement;
using UnityEngine;

public class GameHudManager : MonoBehaviour
{
    [SerializeField] GameObject itemCanvas;
    [SerializeField] GameObject gameOverHud;

    void Start()
    {
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
    }

    private void Instance_OnGameStart()
    {
        itemCanvas.SetActive(true);
        gameOverHud.SetActive(false);
    }

    private void Instance_OnGameOver()
    {
        itemCanvas.SetActive(false);
        gameOverHud.SetActive(true);
    }
}
