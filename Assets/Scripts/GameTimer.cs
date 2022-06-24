using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeVal;
    public Text timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        timeVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeVal += Time.deltaTime;

        //double t = System.Math.Round(timeVal, 2);
        DisplayTime(timeVal);
    }

    void DisplayTime(float timeToShow)
    {
        float min = Mathf.FloorToInt(timeToShow / 60);
        float sec = Mathf.FloorToInt(timeToShow % 60);

        timeElapsed.text = "Time:" + string.Format("{0:00}:{1:00}", min, sec);
    }
}
