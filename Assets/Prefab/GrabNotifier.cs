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
    public void OnSelectEntered()
    {
        xrDirectInteractor.GetValidTargets(validTargets);

        if (validTargets.Count > 0)
        {
            var grabInteractable = validTargets[0] as XRGrabInteractable;
            var grabbedCard = grabInteractable.gameObject;
            var netID = grabbedCard.GetComponent<NetworkIdentity>().netId;

            Debug.Log($"{grabInteractable.gameObject}<color=cyan>[ID:{netID}] on {gameObject.name}</color>");

            networkGrabHandler = grabbedCard.GetComponent<NetworkGrabHandler>();
        }

        isGrabbing = true;
    }

    public void OnSelectExited()
    {
        isGrabbing = false;
        
        networkGrabHandler = null;
    }

    public void OnHoverExited()
    {
        //Debug.Log("OnHoverExited");
    }

    private void Update()
    {
        if (!isGrabbing) return;

        //networkGrabHandler.SyncTransform(transform.position, transform.rotation);
        networkGrabHandler.SyncTransform(networkGrabHandler.gameObject.transform.position, networkGrabHandler.gameObject.transform.rotation);
    }
}
