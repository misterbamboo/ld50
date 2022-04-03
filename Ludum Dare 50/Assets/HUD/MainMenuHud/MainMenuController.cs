using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("MainCredits");
    }

    public void Exit()
    {
        AppHelper.Quit();
    }
}
