using Assets.Audio;
using Assets.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic(KnownedMusics.village_sad_acoustic);
    }

    public void Rejouer()
    {
        AudioManager.instance.PlayMusic(KnownedMusics.night_tense_acoustic);
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        AppHelper.Quit();
    }
}
