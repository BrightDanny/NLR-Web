using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void OnMouseDrag()
    {
        transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
        transform.RotateAround(transform.position, Vector3.right, Input.GetAxis("Mouse Y") * -rotationSpeed);
    }
}
