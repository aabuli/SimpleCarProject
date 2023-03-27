using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{

    public GameObject brakeLeftLight;
    public GameObject brakeRightLight;
    public GameObject rearLeftLight;
    public GameObject rearRightLight;

    void Start()
    {
        brakeLeftLight.SetActive(false);
        brakeRightLight.SetActive(false);
        rearLeftLight.SetActive(false);
        rearRightLight.SetActive(false);
    }

    void Update()
    {
        #region Car Lights
        // Brake Lights
        if (Input.GetAxisRaw("Vertical") < 0 && Vector3.Dot(transform.forward.normalized,
            GetComponentInParent<CarController>().GetSphereRb().velocity.normalized) > 0 ||
            Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Vertical") == 0)
        {
            rearLeftLight.SetActive(false);
            rearRightLight.SetActive(false);
            brakeLeftLight.SetActive(true);
            brakeRightLight.SetActive(true);
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Vector3.Dot(transform.forward.normalized,
            GetComponentInParent<CarController>().GetSphereRb().velocity.normalized) < 0)
        {
            brakeLeftLight.SetActive(false);
            brakeRightLight.SetActive(false);
            rearLeftLight.SetActive(true);
            rearRightLight.SetActive(true);
        }
        else
        {
            brakeLeftLight.SetActive(false);
            brakeRightLight.SetActive(false);
            rearLeftLight.SetActive(false);
            rearRightLight.SetActive(false);
        }
        #endregion 
    }
}
