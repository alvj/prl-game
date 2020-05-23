using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 10f;
    [SerializeField] Vector3 offset;

    void LateUpdate() {
        Vector3 finalPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
