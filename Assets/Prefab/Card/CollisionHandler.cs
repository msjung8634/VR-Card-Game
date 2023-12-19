using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollisionHandler : NetworkBehaviour
{
    NetworkIdentity cardIdentity;
    NetworkIdentity lastPlayerIdentity;

    private void Awake()
    {
        cardIdentity = GetComponent<NetworkIdentity>();    
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lastPlayerIdentity = other.gameObject.transform.root.GetComponent<NetworkIdentity>();

            if (cardIdentity.isOwned) return;

            cardIdentity.AssignClientAuthority(lastPlayerIdentity.connectionToClient);
            Debug.Log($"권한부여 : {lastPlayerIdentity.netId}");
        }
    }

    //[Server]
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        cardIdentity.RemoveClientAuthority();
    //        Debug.Log($"권한해제 : {lastPlayerIdentity.netId}");
    //    }
    //}
}
