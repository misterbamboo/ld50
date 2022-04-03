using Assets.Audio;
using Assets.GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioManager AudioManager { get;  set; }

    void Start()
    {
        AudioManager = AudioManager.instance;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
    }

    private void Instance_OnGameStart()
    {
        AudioManager.PlayMusic(KnownedMusics.water_waves_1);
    }
}
