using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject levelSelectPanel;

    void Start()
    {
        levelSelectPanel.SetActive(false);
        previousPanel.SetActive(true);
    }

    public void ToLevelSelect()
    {
        previousPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void BackToPreviousPanel()
    {
        levelSelectPanel.SetActive(false);
        previousPanel.SetActive(true);
    }
}
