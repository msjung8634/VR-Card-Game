using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType {
	None = 0,
	Red = 1,
	Blue = 2,
}

[RequireComponent(typeof(MeshRenderer))]
[ExecuteAlways]
public class ColorController : MonoBehaviour
{
	public ColorType colorType = ColorType.None;
    public Material front;
    public Material back;
	MeshRenderer meshRenderer;

	private void Awake()
	{
		TryGetComponent(out meshRenderer);
	}

	private void Update()
	{
		bool isFrontSide = Vector3.Dot(transform.up, Vector3.up) >= 0;
		meshRenderer.material = isFrontSide ? front : back;
		colorType = isFrontSide ? ColorType.Red : ColorType.Blue;
	}
}
