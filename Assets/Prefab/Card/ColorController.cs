using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteAlways]
public class ColorController : MonoBehaviour
{
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
	}
}
