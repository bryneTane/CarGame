using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;

    public GameObject MinuteBox;
    public GameObject SecondBox;
    public GameObject MilliBox;

    public static float RawTime;


    void Update()
    {
        MilliCount += Time.deltaTime * 10;
        RawTime += Time.deltaTime;
        MilliBox.GetComponent<Text>().text = "" + MilliCount.ToString("F0");

        if(MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;
        }

        if(SecondCount < 10) SecondBox.GetComponent<Text>().text = "0" + SecondCount + ".";
        else SecondBox.GetComponent<Text>().text = "" + SecondCount + ".";

        if(SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        if (SecondCount < 10) MinuteBox.GetComponent<Text>().text = "0" + MinuteCount + ":";
        else MinuteBox.GetComponent<Text>().text = "" + MinuteCount + ":";
    }
}
