using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkUI : NetworkBehaviour
{
    public GameObject networkCanvas;
    GameObject networkCanvasObj;

    ScoreManager scoreManager;
    UIManager uiManager;

    public override void OnStartClient()
    {
        base.OnStartClient();

        scoreManager = FindObjectOfType<ScoreManager>();

        networkCanvasObj = Instantiate(networkCanvas);
        uiManager = FindObjectOfType<UIManager>();

        uiManager.scoreManager.OnRedCountChanged += uiManager.redCount.GetComponent<UIOnScoreChanged>().MoveText;
        uiManager.scoreManager.OnBlueCountChanged += uiManager.blueCount.GetComponent<UIOnScoreChanged>().MoveText;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        uiManager.scoreManager.OnRedCountChanged -= uiManager.redCount.GetComponent<UIOnScoreChanged>().MoveText;
        uiManager.scoreManager.OnBlueCountChanged -= uiManager.blueCount.GetComponent<UIOnScoreChanged>().MoveText;

        Destroy(networkCanvasObj);
    }
}
