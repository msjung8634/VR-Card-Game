using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var collisionLayer = collision.gameObject.layer;
        var targetLayer = LayerMask.NameToLayer("Card") | LayerMask.NameToLayer("Ground");

        if ((collisionLayer & targetLayer) > 0)
            Debug.Log($"{collision.gameObject.name}°ú Ãæµ¹!");
    }
}
