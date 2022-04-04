using Assets.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTutoController : MonoBehaviour
{
    public void PlayButton()
    {
        AudioManager.instance.PlayMusic(KnownedMusics.night_tense_acoustic);
        SceneManager.LoadScene("MainScene");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
