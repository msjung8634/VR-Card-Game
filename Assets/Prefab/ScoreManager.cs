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

    [SyncVar]
    public uint redCount = 0;
    void ChangeRedCountUI() => OnRedCountChanged?.Invoke();

    [SyncVar]
    public uint blueCount = 0;
    void ChangeBlueCountUI() => OnBlueCountChanged?.Invoke();

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

        if (redCount != red) ChangeRedCountUI();
        if (blueCount != blue) ChangeBlueCountUI();

        totalCount = total;
        redCount = red;
        blueCount = blue;
    }
}
