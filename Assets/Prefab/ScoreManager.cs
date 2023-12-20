using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScoreManager : NetworkBehaviour
{
    public event Action OnRedCountChanged;
    public event Action OnBlueCountChanged;

    [SyncVar]
    public uint totalCount = 0;

    [SyncVar(hook = "ChangeRedCountUI")]
    public uint redCount = 0;

    [SyncVar(hook = "ChangeBlueCountUI")]
    public uint blueCount = 0;

    private void ChangeRedCountUI(uint _, uint newCount)
    {
        OnRedCountChanged?.Invoke();
    }

    private void ChangeBlueCountUI(uint _, uint newCount)
    {
        OnBlueCountChanged?.Invoke();
    }

    [Server]
    private void Update()
    {
        var cards = FindObjectsOfType<ColorController>();

        uint total = 0;
        uint red = 0;
        uint blue = 0;

        foreach (var card in cards)
        {
            total++;
            switch (card.colorType)
            {
                case ColorType.Red:
                    red++;
                    break;
                case ColorType.Blue:
                    blue++;
                    break;
            }
        }

        totalCount = total;
        redCount = red;
        blueCount = blue;
    }
}
