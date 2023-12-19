using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareRig : MonoBehaviour
{
    public HardwareHead head;
    public HardwareHand leftHand;
    public HardwareHand rightHand;

    public Vector3 rigPosition;
    public Quaternion rigRotation;

    public Vector3 headPosition;
    public Quaternion headRotation;    

    public Vector3 leftHandPosition;
    public Quaternion leftHandRotation;

    public Vector3 rightHandPosition;
    public Quaternion rightHandRotation;

    void LateUpdate()
    {
        rigPosition = transform.position;
        rigRotation = transform.rotation;

        headPosition = head.transform.position;
        headRotation = head.transform.rotation;

        leftHandPosition = leftHand.transform.position;
        leftHandRotation = leftHand.transform.rotation;

        rightHandPosition = rightHand.transform.position;
        rightHandRotation = rightHand.transform.rotation;
    }
}
