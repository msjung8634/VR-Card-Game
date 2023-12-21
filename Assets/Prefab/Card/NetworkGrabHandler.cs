using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGrabHandler : NetworkBehaviour
{
    public void SyncTransform(Vector3 handPosition, Quaternion handRotation)
    {
        CmdSyncTransform(handPosition, handRotation);
    }

    [Command(requiresAuthority = false)]
    private void CmdSyncTransform(Vector3 handPosition, Quaternion handRotation)
    {
        transform.position = handPosition;
        transform.rotation = handRotation;
        RPCSyncTransform(handPosition, handRotation);
    }

    [ClientRpc]
    private void RPCSyncTransform(Vector3 handPosition, Quaternion handRotation)
    {
        transform.position = handPosition;
        transform.rotation = handRotation;
    }
}
