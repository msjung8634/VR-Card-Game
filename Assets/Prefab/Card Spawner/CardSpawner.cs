using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardSpawner : NetworkBehaviour
{
    public bool isStopped = false;

    public MeshRenderer spawnerMesh;

    [SyncVar(hook = nameof(ChangeMaterial))]
    public bool isRed = false;

    private void ChangeMaterial(bool _, bool newFlag)
	{
        spawnerMesh.material = isRed ? red : blue;
	}

    public GameObject redCardPrefab;
    public GameObject blueCardPrefab;
    public Material red;
    public Material blue;

    public Transform roundTable;
    float tableRadius;
    [SerializeField] Vector3 targetPos;
    public float distanceThreshold = 1f;
    public float moveSpeed = 5f;

    public float spawnInterval = 2f;
    public int maxSpawnCount = 30;
    public int currentSpawnCount = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();

        isRed = false;

        StartSpawn();
    }

	private void Start()
    {
        tableRadius = roundTable.localScale.x / 2;
        targetPos = transform.position;
    }

    [ServerCallback]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 1f);
    }

    public void StartSpawn()
    {
        StartCoroutine(RandomMove());
        StartCoroutine(GenerateCards());
    }

    private IEnumerator RandomMove()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            // µµÂø
            if (Vector3.Distance(transform.position, targetPos) < distanceThreshold)
            {
                var randomPos = Random.insideUnitCircle * tableRadius;
                targetPos = new Vector3(
                    randomPos.x,
                    transform.position.y,
                    randomPos.y
                );
            }
            // ÀÌµ¿
            else
            {
                var targetDirecation = Vector3.Normalize(targetPos - transform.position) * moveSpeed * Time.deltaTime;
                transform.Translate(targetDirecation, Space.World);
            }

            yield return null;
        }
    }

    private IEnumerator GenerateCards()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            yield return new WaitForSeconds(spawnInterval);
            currentSpawnCount++;

            isRed = currentSpawnCount % 2 == 0 ? true : false;

            var cardObj = isRed ? redCardPrefab : blueCardPrefab;
            GameObject card = Instantiate(cardObj, transform.position, cardObj.transform.rotation);
            NetworkServer.Spawn(card);

            yield return null;
        }

        isStopped = true;
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }
}
