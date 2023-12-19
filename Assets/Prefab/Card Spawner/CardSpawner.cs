using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardSpawner : NetworkBehaviour
{
    public GameObject cardPrefab;

    public int spawnCount = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();

        StartCoroutine(GenerateCards());
    }

    private IEnumerator GenerateCards()
    {
        while (spawnCount < 5)
        {
            yield return new WaitForSeconds(3f);
            spawnCount++;

            GameObject card = Instantiate(cardPrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(card);
            
            yield return null;
        }
    }
}
