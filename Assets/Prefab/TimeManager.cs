using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;

public class TimeManager : NetworkBehaviour
{
    public XRDirectInteractor leftHandGrab;
    public XRDirectInteractor rightHandGrab;

    public event Action OnTimeChanged;
    public event Action OnTimeOver;

    public double gameTotalTime = 120;

    [SyncVar(hook = nameof(ShowRemainTime))]
    public double remainSeconds = 0;

    void ShowRemainTime(double _, double newSeconds)
    {
        if ((int)_ != (int)newSeconds)
        {
            OnTimeChanged?.Invoke();
        }
            
        if (remainSeconds <= 0)
        {
            OnTimeOver?.Invoke();
            leftHandGrab.enabled = false;
            rightHandGrab.enabled = false;
        }
    }

    DateTime serverStartTime;

    public override void OnStartServer()
    {
        base.OnStartServer();

        remainSeconds = gameTotalTime;
        serverStartTime = DateTime.Now;

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        TimeSpan elapsedTime = TimeSpan.Zero;

        while (remainSeconds > 0)
        {
            elapsedTime = DateTime.Now - serverStartTime;
            remainSeconds = gameTotalTime - elapsedTime.TotalSeconds;
            yield return null;
        }
    }
}
