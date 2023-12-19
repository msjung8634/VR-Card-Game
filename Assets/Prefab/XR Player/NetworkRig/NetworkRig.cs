using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkRig : NetworkBehaviour
{
    public NetworkHead networkHead;
    public NetworkHand networkLeftHand;
    public NetworkHand networkRightHand;

    HardwareRig hardwareRig;

    private void Start()
    {
        if (!isLocalPlayer) return;

        hardwareRig = GameObject.FindWithTag("Player").GetComponent<HardwareRig>();
    }

    private void LateUpdate()
    {
        if (hardwareRig == null) return;

        transform.SetPositionAndRotation(
            hardwareRig.rigPosition,
            hardwareRig.rigRotation);

        networkHead.transform.SetPositionAndRotation(
            hardwareRig.headPosition,
            hardwareRig.headRotation
            );

        networkLeftHand.transform.SetPositionAndRotation(
            hardwareRig.leftHandPosition,
            hardwareRig.leftHandRotation
            );

        networkRightHand.transform.SetPositionAndRotation(
            hardwareRig.rightHandPosition,
            hardwareRig.rightHandRotation
            );
    }
}
