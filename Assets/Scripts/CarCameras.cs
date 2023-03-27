using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarCameras : MonoBehaviour
{
    public CinemachineVirtualCamera rearCam;
    public CinemachineVirtualCamera frontCam;
    public CinemachineVirtualCamera speedCam;
    public CinemachineVirtualCamera MenuCam;
    private CarController car;


    private void Start()
    {
        car = FindObjectOfType<CarController>();
    }

    void Update()
    {
        if (!car.IsReadyToPlay())
        {
            rearCam.Priority = 0;
            speedCam.Priority = 0;
            frontCam.Priority = 0;
            MenuCam.Priority = 1;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Vector3.Dot(car.transform.forward.normalized,
            car.GetSphereRb().velocity.normalized) < 0)
        {
            MenuCam.Priority = 0;
            rearCam.Priority = 0;
            speedCam.Priority = 0;
            frontCam.Priority = 1;
        }
        else if (car.IsCarBoosting())
        {
            MenuCam.Priority = 0;
            rearCam.Priority = 0;
            frontCam.Priority = 0;
            speedCam.Priority = 1;
        }
        else
        {
            MenuCam.Priority = 0;
            speedCam.Priority = 0;
            frontCam.Priority = 0;
            rearCam.Priority = 1;
        }
    }
}
