using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapTimer : MonoBehaviour
{

    private int currentMinute;
    private int currentSecond;
    private float currentMilli;

    private int bestMinute;
    private int bestSecond;
    private float bestMilli;

    int fullNumber;
    int bestNumber = 999999;

    public string milliDisplay;

    public TextMeshProUGUI minuteBox;
    public TextMeshProUGUI secondBox;
    public TextMeshProUGUI milliBox;
    public TextMeshProUGUI bestminuteBox;
    public TextMeshProUGUI bestsecondBox;
    public TextMeshProUGUI bestmilliBox;

    private bool startTimer;

    private void Update()
    {
        if (startTimer)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {

        currentMilli += Time.deltaTime * 10;
        milliDisplay = currentMilli.ToString("F0");
        milliBox.text = currentMilli < 10 ? "0" + milliDisplay : "" + milliDisplay;

        if (currentMilli >= 9)
        {
            currentMilli = 0;
            currentSecond += 1;
        }

        if (currentSecond <= 9)
        {
            secondBox.text = "0" + currentSecond + ":";
        }
        else
        {
            secondBox.text = "" + currentSecond + ":";
        }

        if (currentSecond >= 60)
        {
            currentSecond = 0;
            currentMinute += 1;
        }

        if (currentMinute <= 9)
        {
            minuteBox.text = "0" + currentMinute + ":";
        }
        else
        {
            minuteBox.text = "" + currentMinute + ":";
        }
    }

    void ResetTimer()
    {
        currentMinute = 0;
        currentSecond = 0;
        currentMilli = 0;
    }

    void CheckBestTime()
    {
        fullNumber = currentMinute + currentSecond + (int)currentMilli;
        //bestNumber = bestMinute + bestSecond + (int)bestMilli;
        if (fullNumber < bestNumber && fullNumber != 0)
        {
            bestNumber = fullNumber;
            bestminuteBox.text = currentMinute < 10 ? "0" + currentMinute + ":" : currentMinute + ":";
            bestsecondBox.text = currentSecond < 10 ? "0" + currentSecond + ":" : currentSecond + ":";
            bestmilliBox.text = currentMilli < 10 ? "0" + (int)currentMilli : "" + (int)currentMilli;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckBestTime();
        ResetTimer();
        startTimer = true;
    }
}

