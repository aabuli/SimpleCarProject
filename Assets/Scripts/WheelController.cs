using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    //// Wheel Collider Method:
    //[SerializeField] WheelCollider frontRight;
    //[SerializeField] WheelCollider frontLeft;
    //[SerializeField] WheelCollider backRight;
    //[SerializeField] WheelCollider backLeft;

    //[SerializeField] Transform frontRightTransform;
    //[SerializeField] Transform frontLeftTransform;
    //[SerializeField] Transform backRightTransform;
    //[SerializeField] Transform backLeftTransform;

    //public float maxAcceleration = 800f;
    //public float maxBrakingForce = 400f;
    //public float maxTurnAngle = 30f;

    //private float currentAcceleration = 0f;
    //private float currentBrakeForce = 0f;
    //private float currentTurnAngle = 0f;

    //private void FixedUpdate()
    //{
    //    currentAcceleration = maxAcceleration * Input.GetAxis("Vertical");

    //    // Braking
    //    if (Input.GetKey(KeyCode.Space))
    //        currentBrakeForce = maxBrakingForce;
    //    else
    //        currentBrakeForce = 0f;

    //    // Accelerating
    //    backLeft.motorTorque = currentAcceleration;
    //    backRight.motorTorque = currentAcceleration;

    //    backRight.brakeTorque= currentBrakeForce;
    //    backLeft.brakeTorque= currentBrakeForce;
    //    frontRight.brakeTorque= currentBrakeForce;
    //    frontLeft.brakeTorque= currentBrakeForce;

    //    // Steering
    //    currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
    //    frontLeft.steerAngle= currentTurnAngle;
    //    frontRight.steerAngle= currentTurnAngle;

    //    UpdateWheel(frontLeft, frontLeftTransform);
    //    UpdateWheel(frontRight, frontRightTransform);
    //    UpdateWheel(backLeft, backLeftTransform);
    //    UpdateWheel(backRight, backRightTransform);
    //}

    //void UpdateWheel(WheelCollider col, Transform trans)
    //{
    //    Vector3 pos;
    //    Quaternion rot;

    //    col.GetWorldPose(out pos, out rot);

    //    trans.position = pos;
    //    trans.rotation = rot;
    //}

    public float rotationSpeed = 2000f;
    public Gradient trailColor;
    public Material trailMaterial;

    private Animator anim;
    private GameObject trail;


    private void Start()
    {
        anim = transform.GetComponentInParent<Animator>();
        GenerateTrail();
    }

    private void Update()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        transform.Rotate(verticalAxis * rotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);
    
        if(horizontalAxis>0)
        {
            anim.SetBool("goingLeft", false);
            anim.SetBool("goingRight", true);
        } else if(horizontalAxis<0)
        {
            anim.SetBool("goingRight", false);
            anim.SetBool("goingLeft", true);
        }
        else
        {
            anim.SetBool("goingLeft", false);
            anim.SetBool("goingRight", false);
        }

        if(horizontalAxis!=0 && GetComponentInParent<CarController>().IsCarGrounded())
            trail.GetComponent<TrailRenderer>().emitting = true;
        else
            trail.GetComponent<TrailRenderer>().emitting = false;
    }

    private void GenerateTrail()
    {
        trail = new GameObject();
        trail.gameObject.name = "wheelTrail";
        trail.transform.position= transform.position;
        trail.transform.parent= transform;
        trail.AddComponent<TrailRenderer>();
        trail.GetComponent<TrailRenderer>().time = 1f;
        trail.GetComponent<TrailRenderer>().material = trailMaterial;
        trail.GetComponent<TrailRenderer>().colorGradient = trailColor;
        trail.GetComponent<TrailRenderer>().shadowCastingMode = 0;
        trail.GetComponent<TrailRenderer>().receiveShadows = false;
        trail.GetComponent<TrailRenderer>().startWidth = 0.22f;
        trail.GetComponent<TrailRenderer>().endWidth = 0.22f;
        trail.GetComponent<TrailRenderer>().emitting = false;
    }
}
