using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target To Follow")]
    public Transform target; // Drag your car into this slot

    [Header("Camera Positioning")]
    public Vector3 offset = new Vector3(0, 3, -7); // 3 units up, 7 units behind
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate where the camera should be based on the car's position and rotation
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        
        // Smoothly move the camera to that position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        // Always look at the car
        transform.LookAt(target);
    }
}
