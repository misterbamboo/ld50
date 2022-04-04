using Assets.Audio;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic(KnownedMusics.adventure_serious_cinematic);
    }

    public void Play()
    {
        SceneManager.LoadScene("MainTuto");
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
