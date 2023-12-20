using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ScoreType {
    None = 0,
    Red = 1,
    Blue = 2,
}

public class UIOnScoreChanged : MonoBehaviour
{
    RectTransform rectTransform;
    ScoreManager scoreManager;
    public ScoreType scoreType = ScoreType.None;
    Vector3 originPos;

    [Header("Move Back And Forth")]
    public float distance = 100f;
    public float moveTime = 1f;
    public AnimationCurve speedOverTime;
    
    private void Awake()
    {
        TryGetComponent(out rectTransform);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Start()
    {
        originPos = rectTransform.anchoredPosition3D;
    }

    private void OnEnable()
    {
        if (scoreType == ScoreType.Red)
            scoreManager.OnRedCountChanged += MoveText;

        if (scoreType == ScoreType.Blue)
            scoreManager.OnBlueCountChanged += MoveText;
    }

    private void OnDisable()
    {
        if (scoreType == ScoreType.Red)
            scoreManager.OnRedCountChanged -= MoveText;

        if (scoreType == ScoreType.Blue)
            scoreManager.OnBlueCountChanged -= MoveText;
    }

    
    private IEnumerator lastMove = null;
    public void MoveText()
    {
        if (lastMove != null)
            StopCoroutine(lastMove);

        lastMove = MoveBackAndForth();
        StartCoroutine(lastMove);
    }

    private IEnumerator MoveBackAndForth()
    {
        var backPos = originPos + Vector3.forward * distance;
        var halfTime = moveTime / 2f;

        // move back
        float elapsedTime = 0f;
        while (elapsedTime < halfTime)
        {
            elapsedTime += Time.deltaTime;
            var progress = speedOverTime.Evaluate(elapsedTime / halfTime) * elapsedTime / halfTime;
            rectTransform.anchoredPosition3D = Vector3.Lerp(originPos, backPos, progress);
            yield return null;
        }
        rectTransform.anchoredPosition3D = backPos;

        // move forth
        elapsedTime = 0;
        while (elapsedTime < halfTime)
        {
            elapsedTime += Time.deltaTime;
            var progress = speedOverTime.Evaluate((elapsedTime + halfTime) / moveTime) * elapsedTime / halfTime;
            rectTransform.anchoredPosition3D = Vector3.Lerp(backPos, originPos, progress);
            yield return null;
        }
        rectTransform.anchoredPosition3D = originPos;
    }
}
