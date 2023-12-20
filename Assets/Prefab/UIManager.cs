using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ready;
    public TextMeshProUGUI start;
    public TextMeshProUGUI redCount;
    public TextMeshProUGUI blueCount;

    ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        redCount.text = $"{scoreManager.redCount:##}";
        blueCount.text = $"{scoreManager.blueCount:##}";
    }

    public void ShowReady()
    {
        ready.gameObject.SetActive(true);
    }

    public void ShowStart()
    {
        start.gameObject.SetActive(true);
    }

    public void ShowScore()
    {
        redCount.gameObject.SetActive(true);
        blueCount.gameObject.SetActive(true);
    }
}
