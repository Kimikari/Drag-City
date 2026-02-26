using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    [Header("Settings")]
    public float motorForce = 1500f;
    public float breakForce = 3000f;
    public float maxSteerAngle = 35f;

    [Header("Physics Setup")]
    public Transform centerOfMass; // We will assign this in Unity

    private float horizontalInput;
    private float verticalInput;
    private bool isBreaking;
    private Rigidbody carRb;

    void Start()
    {
        // This tells the Rigidbody to use our custom weight point
        carRb = GetComponent<Rigidbody>();
        if (centerOfMass != null)
        {
            carRb.centerOfMass = centerOfMass.localPosition;
        }
    }

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        rearLeft.motorTorque = verticalInput * motorForce;
        rearRight.motorTorque = verticalInput * motorForce;

        float currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBraking(currentBreakForce);
    }

    private void ApplyBraking(float force)
    {
        frontLeft.brakeTorque = force;
        frontRight.brakeTorque = force;
        rearLeft.brakeTorque = force;
        rearRight.brakeTorque = force;
    }

    private void HandleSteering()
    {
        float currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeft.steerAngle = currentSteerAngle;
        frontRight.steerAngle = currentSteerAngle;
    }
}
