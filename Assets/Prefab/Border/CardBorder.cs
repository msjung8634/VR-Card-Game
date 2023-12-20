using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardBorder : NetworkBehaviour
{
    public CardSpawner cardSpawner;

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        // 카드와 충돌하면
        if (other.gameObject.layer == LayerMask.NameToLayer("Card"))
        {
            Destroy(other.gameObject);
            cardSpawner.maxSpawnCount++;
            Debug.Log($"[Border] {cardSpawner.currentSpawnCount}/{cardSpawner.maxSpawnCount}");

            if (cardSpawner.isStopped)
            {
                cardSpawner.isStopped = false;
                cardSpawner.StartSpawn();
            }
        }
    }
}
