using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AuthorityHandler : NetworkBehaviour
{
    public uint ownerNetID = uint.MaxValue;
    public bool owned = false;

    NetworkIdentity cardIdentity;
    public NetworkIdentity lastPlayerIdentity;

	public override void OnStartServer()
	{
		base.OnStartServer();
        cardIdentity = GetComponent<NetworkIdentity>();
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lastPlayerIdentity = other.gameObject.transform.root.GetComponent<NetworkIdentity>();

            if (owned)
            {
                cardIdentity.RemoveClientAuthority();
                owned = false;
            }

            if (cardIdentity.AssignClientAuthority(lastPlayerIdentity.connectionToClient))
			{
                owned = true;
                ownerNetID = lastPlayerIdentity.netId;
                Debug.Log($"[By Player] {gameObject.name} Authority -> NetID:{lastPlayerIdentity.netId:#00}");
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Card"))
		{
            var cardIdentity = other.gameObject.GetComponent<NetworkIdentity>();
            var authorityHandler = other.gameObject.GetComponent<AuthorityHandler>();
            cardIdentity.RemoveClientAuthority();
            if (cardIdentity.AssignClientAuthority(lastPlayerIdentity.connectionToClient))
			{
                authorityHandler.ownerNetID = ownerNetID;
                authorityHandler.lastPlayerIdentity = lastPlayerIdentity;
                Debug.Log($"[By Card] {other.gameObject.name} Authority -> NetID:{lastPlayerIdentity.netId:#00}");
            }
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
