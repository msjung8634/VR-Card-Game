using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardSpawner : NetworkBehaviour
{
    public GameObject cardPrefab;

    public int maxSpawnCount = 50;
    public int currentSpawnCount = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();

        StartCoroutine(GenerateCards());
    }

    private IEnumerator GenerateCards()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            yield return new WaitForSeconds(3f);
            currentSpawnCount++;

            GameObject card = Instantiate(cardPrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(card);
            
            yield return null;
        }
    }
}
