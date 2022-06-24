using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highscore;

    void Start()
    {
        SetHighscore();
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ResetButton()
    {
        GameManager.instance.ResetHiScore();
        SetHighscore();
    }

    void SetHighscore()
    {
        highscore.text = "Highscore: " + GameManager.LoadHiScore();
    }
}
