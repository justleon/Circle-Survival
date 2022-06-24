using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int fixedBoundary = Screen.width / 15;
    int guiDifference = Screen.height / 9;

    public int points = 0;
    public bool isRunning = true;

    [SerializeField] float spawnTime = 2.0f;
    [SerializeField] float spawnTimeLimit = .7f;
    [SerializeField] float timeElapsed = 0;
    [SerializeField] int spawnProbability = 10;
    [SerializeField] float spawnOffset = 50f;

    [SerializeField] float gameOverDelay = 1.0f;

    public GameObject circle = null;
    public GameObject bomb = null;
    public GameTimer timer = null;
    public GameOver GameOverScreen = null;

    public Text score;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(spawnTime > spawnTimeLimit)
        {
            float initialSpawnTime = 2.0f;
            float timeFactor = 0.072f;
            int modifier = Mathf.FloorToInt(timer.timeVal / 5);
            spawnTime = initialSpawnTime - (timeFactor * modifier);
        }

        if((timeElapsed >= spawnTime) && isRunning) 
        {
            CircleSpawner();
            timeElapsed = 0;
        }
        
        score.text = "Score:" + points;
    }

    void CircleSpawner()
    {
        GameObject[] spawnedCircles;
        spawnedCircles = GameObject.FindGameObjectsWithTag("Circle");

        //16, just to be sure that there will be no intersection
        int iterations = 16;
        bool isIntersection;
        Vector3 spawnLoc;
        do
        {
            isIntersection = false;
            spawnLoc = Camera.main.ScreenToWorldPoint(new Vector3(
                Random.Range(fixedBoundary, Screen.width - fixedBoundary),
                Random.Range(fixedBoundary, Screen.height - (fixedBoundary + guiDifference)),
                -1f));

            //distance between objects is measured in WorldPoints
            foreach(GameObject obj in spawnedCircles)
            {
                Vector2 diff = obj.transform.position - spawnLoc;
                if(diff.sqrMagnitude <= spawnOffset)
                {
                    isIntersection = true;
                }
            }
            iterations--;
        } while (iterations > 0 && isIntersection);

        if(iterations == 0)
            return;

        int randProbability = Mathf.FloorToInt(Random.Range(0, 50));
        if (randProbability > spawnProbability)
            Instantiate(circle, spawnLoc, Quaternion.identity);
        else
            Instantiate(bomb, spawnLoc, Quaternion.identity);
    }

    public void EndGame()
    {
        if(isRunning)
        {
            isRunning = false;
            StartCoroutine(ShowGameOver());
        }
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);

        Time.timeScale = 0;
        bool isNewHigh = CheckHiScore();
        GameOverScreen.ShowUp(points, isNewHigh);
    }

    bool CheckHiScore()
    {
        if(LoadHiScore() < points)
        {
            SaveHiScore(points);
            return true;
        }

        return false;
    }

    public void SaveHiScore(int highscore)
    {
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.Save();
    }

    public void ResetHiScore()
    {
        PlayerPrefs.DeleteKey("Highscore");
    }

    public static int LoadHiScore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            int highscore = PlayerPrefs.GetInt("Highscore");
            return highscore;
        }
        else
        {
            return 0;
        }
    }
}
