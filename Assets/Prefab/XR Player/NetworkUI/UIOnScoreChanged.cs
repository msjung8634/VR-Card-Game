using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class UIOnScoreChanged : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 originPos;

    [Header("Move Back And Forth")]
    public float distance = 100f;
    public float moveTime = 1f;
    public AnimationCurve speedOverTime;
    
    private void Awake()
    {
        TryGetComponent(out rectTransform);
    }

    private void Start()
    {
        originPos = rectTransform.anchoredPosition3D;
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
