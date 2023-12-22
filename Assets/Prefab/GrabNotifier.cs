using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;

public class GrabNotifier : MonoBehaviour
{
    bool isGrabbing = false;

    public XRDirectInteractor xrDirectInteractor;
    List<IXRInteractable> validTargets = new List<IXRInteractable>();

    public void OnHoverEntered()
    {
        //Debug.Log("OnHoverEntered");
    }

    public NetworkGrabHandler networkGrabHandler;
    public Rigidbody networkRigidbody;
    public void OnSelectEntered()
    {
        xrDirectInteractor.GetValidTargets(validTargets);

        if (validTargets.Count > 0)
        {
            var grabInteractable = validTargets[0] as XRGrabInteractable;
            var grabbedCard = grabInteractable.gameObject;
            var netID = grabbedCard.GetComponent<NetworkIdentity>().netId;

            Debug.Log($"<color=cyan>Card[netID:{netID}] on {gameObject.name}</color>");

            networkGrabHandler = grabbedCard.GetComponent<NetworkGrabHandler>();
            networkRigidbody = grabbedCard.GetComponent<Rigidbody>();
        }

        isGrabbing = true;
    }

    public void OnSelectExited()
    {
        isGrabbing = false;

        var velocity = networkRigidbody.velocity;
        var angularVelocity = networkRigidbody.angularVelocity;
        var inertiaTensor = networkRigidbody.inertiaTensor;
        var inertiaTensorRotation = networkRigidbody.inertiaTensorRotation;
        var centerOfMass = networkRigidbody.centerOfMass;

        networkGrabHandler.SyncRigidbody(false, velocity, angularVelocity, inertiaTensor, inertiaTensorRotation, centerOfMass);
        networkGrabHandler = null;
    }

    public void OnHoverExited()
    {
        //Debug.Log("OnHoverExited");
    }

    private void Update()
    {
        if (!isGrabbing) return;

        Vector3 grabbedCardPosition = networkGrabHandler.gameObject.transform.position;
        Quaternion grabbedCardRotation = networkGrabHandler.gameObject.transform.rotation;
        networkGrabHandler.SyncTransform(grabbedCardPosition, grabbedCardRotation);

        var velocity = networkRigidbody.velocity;
        var angularVelocity = networkRigidbody.angularVelocity;
        var inertiaTensor = networkRigidbody.inertiaTensor;
        var inertiaTensorRotation = networkRigidbody.inertiaTensorRotation;
        var centerOfMass = networkRigidbody.centerOfMass;
        networkGrabHandler.SyncRigidbody(true, velocity, angularVelocity, inertiaTensor, inertiaTensorRotation, centerOfMass);
    }
}
