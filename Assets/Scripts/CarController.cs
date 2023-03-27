using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.GlobalIllumination;

public class CarController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;

    private bool carGrounded;
    private bool carBoosting;
    private bool boostInCooldown;
    private bool readyToPlay;

    [SerializeField] private Rigidbody sphereRb;
    [SerializeField] private Rigidbody carRb;
    [SerializeField] private LayerMask ground;

    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float gravity = -40f;
    [SerializeField] private float groundDrag = 4f;
    [SerializeField] private float brakeDrag = 6f;
    [SerializeField] private float airDrag = 0.98f;
    [SerializeField] private float alignToGroundTime = 5f;
    [SerializeField] private float maxSpeed = 400f;
    [SerializeField] private float defaultSpeed = 300f;
    [SerializeField] private float boostTimer = 5f;
    [SerializeField] private float boostCooldown = 5f;

    public GameObject blueCar;
    public GameObject blackCar;

    void Start()
    {
        sphereRb.transform.parent = null;
        carRb.transform.parent = null;
        groundDrag = sphereRb.drag;
    }


    void Update()
    {
        if (readyToPlay)
        {
            // Player Input
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            // Car rotation      
            float carRotation = horizontalInput * turnSpeed * Time.deltaTime * verticalInput;

            if (carGrounded)
                transform.Rotate(Vector3.up * carRotation, Space.World);

            // Set car position to the sphere
            transform.position = sphereRb.transform.position;

            // Ground Check
            RaycastHit hit;
            carGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, ground);

            // Align car to the ground
            Quaternion rotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, alignToGroundTime * Time.deltaTime);

            // Car movement
            verticalInput *= verticalInput > 0 ? moveSpeed : moveSpeed * 0.5f;

            // Apply Drag
            sphereRb.drag = Input.GetKey(KeyCode.Space) ? brakeDrag : carGrounded ? groundDrag : airDrag; ;

            // Boost
            if (Input.GetKeyDown(KeyCode.LeftShift) && !carBoosting && !boostInCooldown && Input.GetAxisRaw("Vertical") > 0)
            {
                StartCoroutine(CarBoost());
            }
        }
    }
    private void FixedUpdate()
    {
        if (carGrounded)
            sphereRb.AddForce(transform.forward * verticalInput, ForceMode.Acceleration);
        else
            sphereRb.AddForce(transform.up * gravity);

        carRb.MoveRotation(transform.rotation);
    }

    private IEnumerator BoostCooldown()
    {
        boostInCooldown = true;
        yield return new WaitForSeconds(boostCooldown);
        boostInCooldown = false;
    }

    private IEnumerator CarBoost()
    {
        carBoosting = true;
        moveSpeed = maxSpeed;
        yield return new WaitForSeconds(boostTimer);
        carBoosting = false;
        moveSpeed = defaultSpeed;
        StartCoroutine(BoostCooldown());
    }


    public bool IsCarGrounded()
    {
        return carGrounded;
    }

    public bool IsCarBoosting()
    {
        return carBoosting;
    }

    public bool IsBoostInCooldown()
    {
        return boostInCooldown;
    }

    public bool IsReadyToPlay()
    {
        return readyToPlay;
    }

    public void SetReadyToPlay(bool startplaying)
    {
        readyToPlay = startplaying;
    }

    public Rigidbody GetSphereRb()
    {
        return sphereRb;
    }

    public void SelectBlueCar()
    {
        blackCar.SetActive(false);
        blueCar.SetActive(true);

    }

    public void SelectBlackCar()
    {
        blueCar.SetActive(false);
        blackCar.SetActive(true);
    }
}
