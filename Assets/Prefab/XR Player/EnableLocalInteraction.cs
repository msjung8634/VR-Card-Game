using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class EnableLocalInteraction : NetworkBehaviour
{
    public GameObject XRInputActionManager;
    public GameObject XROrigin;
    public GameObject XRLeftHandController;
    public GameObject XRRightHandController;

    private void Start()
    {
        //if (!isServer && !isLocalPlayer) return;

        XRInputActionManager.SetActive(true);

        XROrigin.GetComponent<XROrigin>().enabled = true;
        XROrigin.GetComponent<LocomotionSystem>().enabled = true;
        XROrigin.GetComponent<TwoHandedGrabMoveProvider>().enabled = true;
        XROrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = true;
        XROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;

        //XRLeftHandController.GetComponent<ActionBasedControllerManager>().enabled = true;
        XRLeftHandController.GetComponent<ActionBasedController>().enabled = true;
        XRLeftHandController.GetComponent<GrabMoveProvider>().enabled = true;
        XRLeftHandController.GetComponent<XRInteractionGroup>().enabled = true;

        //XRRightHandController.GetComponent<ActionBasedControllerManager>().enabled = true;
        XRRightHandController.GetComponent<ActionBasedController>().enabled = true;
        XRRightHandController.GetComponent<GrabMoveProvider>().enabled = true;
        XRRightHandController.GetComponent<XRInteractionGroup>().enabled = true;
    }
}
