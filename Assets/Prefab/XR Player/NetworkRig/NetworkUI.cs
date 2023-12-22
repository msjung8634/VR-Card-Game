using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkUI : NetworkBehaviour
{
    public GameObject networkCanvas;
    GameObject networkCanvasObj;

    UIManager uiManager;

    public override void OnStartClient()
    {
        base.OnStartClient();

        networkCanvasObj = Instantiate(networkCanvas);
        uiManager = FindObjectOfType<UIManager>();

        uiManager.scoreManager.OnRedCountChanged += uiManager.redCount.GetComponent<UIOnScoreChanged>().MoveText;
        uiManager.scoreManager.OnBlueCountChanged += uiManager.blueCount.GetComponent<UIOnScoreChanged>().MoveText;
        uiManager.timeManager.OnTimeChanged += uiManager.remainTime.GetComponent<UIOnTimeChanged>().MoveText;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        uiManager.scoreManager.OnRedCountChanged -= uiManager.redCount.GetComponent<UIOnScoreChanged>().MoveText;
        uiManager.scoreManager.OnBlueCountChanged -= uiManager.blueCount.GetComponent<UIOnScoreChanged>().MoveText;
        uiManager.timeManager.OnTimeChanged -= uiManager.remainTime.GetComponent<UIOnTimeChanged>().MoveText;

        Destroy(networkCanvasObj);
    }
}
