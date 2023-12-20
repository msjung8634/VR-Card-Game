using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardHandler : NetworkBehaviour
{
    public CardSpawner cardSpawner;

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        // ī��� �浹�ϸ�
        if (other.gameObject.layer == LayerMask.NameToLayer("Card"))
        {
            Destroy(other.gameObject);
            cardSpawner.maxSpawnCount++;
            Debug.Log($"[Spawner] {cardSpawner.currentSpawnCount}/{cardSpawner.maxSpawnCount}");

            if (cardSpawner.isStopped)
            {
                cardSpawner.isStopped = false;
                cardSpawner.StartSpawn();
            }
        }
    }
}
