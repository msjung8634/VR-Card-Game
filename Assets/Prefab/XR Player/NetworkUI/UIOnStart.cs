using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOnStart : MonoBehaviour
{
    RectTransform rectTransform;
    TextMeshProUGUI text;

    [Header("Move")]
    public bool moveOnStart = false;
    public float distancePerSecond = 5f;
    public float moveTime = 1f;
    public AnimationCurve moveSpeedOverTime;

    [Header("Rotate")]
    public bool rotateOnStart = false;
    public float anglePerSecond = 90f;
    public float rotateTime = 1f;
    public AnimationCurve rotateSpeedOverTime;

    [Header("Transparent")]
    public bool transparentOnStart = false;
    public float ratePerSecond = .2f;
    public float transparentTime = 1f;
    public AnimationCurve transparentSpeedOverTime;

    IEnumerator lastMove = null;
    IEnumerator lastRotate = null;
    IEnumerator lastTransparent = null;

    private void Awake()
    {
        TryGetComponent(out rectTransform);
        TryGetComponent(out text);
    }

    private void Start()
    {
        if (moveOnStart) StartMove(distancePerSecond);
        if (rotateOnStart) StartRotate(anglePerSecond);
        if (transparentOnStart) StartTransparent(ratePerSecond);
    }

    private void StartMove(float distancePerSecond)
    {
        if (lastMove != null) StopCoroutine(lastMove);

        lastMove = Move(distancePerSecond);
        StartCoroutine(lastMove);
    }
    private void StartRotate(float anglePerSecond)
    {
        if (lastRotate != null) StopCoroutine(lastRotate);

        lastRotate = Rotate(anglePerSecond);
        StartCoroutine(lastRotate);
    }
    private void StartTransparent(float ratePerSecond)
    {
        if (lastTransparent != null) StopCoroutine(lastTransparent);

        lastTransparent = Transparent(ratePerSecond);
        StartCoroutine(lastTransparent);
    }

    public void StopAll() => StopAllCoroutines();
    public void StopMove() => StopCoroutine(lastMove);
    public void StopRotate() => StopCoroutine(lastRotate);
    public void StopTransparent() => StopCoroutine(lastTransparent);

    private IEnumerator Move(float distancePerSecond)
    {
        var origin = rectTransform.anchoredPosition3D;
        var target = origin + Vector3.forward * distancePerSecond * moveTime;

        float elapsedTime = 0f;
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            var progress = moveSpeedOverTime.Evaluate(elapsedTime / moveTime) * elapsedTime / moveTime;
            rectTransform.anchoredPosition3D = Vector3.Lerp(origin, target, progress);
            yield return null;
        }

        rectTransform.anchoredPosition3D = target;
    }

    private IEnumerator Rotate(float anglePerSecond)
    {
        yield return null;
    }

    private IEnumerator Transparent(float ratePerSecond)
    {
        yield return null;
    }
}
