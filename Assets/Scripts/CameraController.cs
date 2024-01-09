using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 

    public float distance = 5.0f;
    public float height = 3.0f; 
    public float rotationSpeed = 5.0f; 

    private Vector3 offset;
    private float currentRotationAngle; 

    void Start()
    {
        offset = new Vector3(0, height, -distance);
        currentRotationAngle = transform.eulerAngles.y; 
    }

    void LateUpdate()
    {
        
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, currentRotationAngle, 0) * offset;
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);
        
        transform.LookAt(target);
    }

    void Update()
    {
        Vector3 movementDirection = target.forward;
        if (movementDirection != Vector3.zero)
        {
            currentRotationAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
        }
    }
}
