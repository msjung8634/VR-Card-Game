using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TimeManager timeManager;

    public GameObject ready;
    public GameObject start;
    public GameObject redCount;
    public GameObject blueCount;
    public GameObject remainTime;

    TextMeshProUGUI txtRedCountText;
    TextMeshProUGUI txtBlueCountText;
    TextMeshProUGUI txtRemainTimeText;

    Canvas canvas;

    private void Awake()
    {
        TryGetComponent(out canvas);
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();

        txtRedCountText = redCount.GetComponent<TextMeshProUGUI>();
        txtBlueCountText = blueCount.GetComponent<TextMeshProUGUI>();
        txtRemainTimeText = remainTime.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        txtRedCountText.text = $"{scoreManager.redCount:#0}";
        txtBlueCountText.text = $"{scoreManager.blueCount:#0}";

        if (timeManager.remainSeconds > 0)
        {
            if (timeManager.remainSeconds <= 30)
                txtRemainTimeText.color = Color.magenta;

            txtRemainTimeText.text = $"{timeManager.remainSeconds:#0}";
        }
        else
        {
            txtRemainTimeText.text = $"STOP";
        }
    }

    public void ShowReady()
    {
        ready.SetActive(true);
    }

    public void ShowStart()
    {
        start.SetActive(true);
    }

    public void ShowScore()
    {
        redCount.SetActive(true);
        blueCount.SetActive(true);
    }
}
