using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI time;

    public float timer;
    public string timePlayingStr;

    private TimeSpan timePlaying;
    public bool startTimer;

    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        time.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    
    public void UpdateTime()
    {
        //Start timer
        if(startTimer == true)
        {
            timer += Time.deltaTime;

            timePlaying = TimeSpan.FromSeconds(timer);
            timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
            time.text = timePlayingStr;
        }

        //Stop timer
        else
        {
            
            time.text = timePlayingStr;
        }
    }
}
