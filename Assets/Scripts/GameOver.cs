using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text highscore;
    public Text newHighscore;

    public AudioSource oofSFX;

    public void ShowUp(int points, bool isNew)
    {
        gameObject.SetActive(true);
        oofSFX.Play();
        newHighscore.enabled = isNew ? true : false;
        highscore.text = "Highscore: " + points;
    }

    public void TryAgainButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Mainmenu");
    }
}
