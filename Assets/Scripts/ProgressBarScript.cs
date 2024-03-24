using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    public Image ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        ProgressBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressBar.fillAmount <= PlayerController.currentPickUpCount/PlayerController.maxPickUpCount)
        {
            ProgressBar.fillAmount += 0.01f;
        }
    }
}