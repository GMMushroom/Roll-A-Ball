using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private bool isTiming;

    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public float GetTime()
    {
        return currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming == true)
        {
            currentTime += Time.deltaTime;
        }
    }
}
