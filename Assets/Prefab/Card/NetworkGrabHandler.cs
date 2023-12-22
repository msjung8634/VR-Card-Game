using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGrabHandler : NetworkBehaviour
{
    public Rigidbody rigidbody;

    private void Awake()
    {
        TryGetComponent(out rigidbody);
    }

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

    public void SyncRigidbody(bool isKinematic, Vector3 velocity, Vector3 angularVelocity, Vector3 inertiaTensor, Quaternion inertiaTensorRotation, Vector3 centorOfMass)
    {
        CmdSyncRigidbody(isKinematic, velocity, angularVelocity, inertiaTensor, inertiaTensorRotation, centorOfMass);
    }

    [Command(requiresAuthority = false)]
    private void CmdSyncRigidbody(bool isKinematic, Vector3 velocity, Vector3 angularVelocity, Vector3 inertiaTensor, Quaternion inertiaTensorRotation, Vector3 centorOfMass)
    {
        rigidbody.isKinematic = isKinematic;
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
        rigidbody.inertiaTensor = inertiaTensor;
        rigidbody.inertiaTensorRotation = inertiaTensorRotation;
        rigidbody.centerOfMass = centorOfMass;
        RPCSyncRigidbody(isKinematic, velocity, angularVelocity, inertiaTensor, inertiaTensorRotation, centorOfMass);
    }

    [ClientRpc]
    private void RPCSyncRigidbody(bool isKinematic, Vector3 velocity, Vector3 angularVelocity, Vector3 inertiaTensor, Quaternion inertiaTensorRotation, Vector3 centorOfMass)
    {
        rigidbody.isKinematic = isKinematic;
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
        rigidbody.inertiaTensor = inertiaTensor;
        rigidbody.inertiaTensorRotation = inertiaTensorRotation;
        rigidbody.centerOfMass = centorOfMass;
    }
}
