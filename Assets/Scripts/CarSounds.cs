using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    private AudioSource engineSound;

    void Start()
    {
        engineSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && !Input.GetKey(KeyCode.Space) &&
            !GetComponent<CarController>().IsCarBoosting())
        {
            if (engineSound.pitch < 3) engineSound.pitch += .01f;
            else if (engineSound.pitch > 3) engineSound.pitch -= .01f;
        }
        else if (Input.GetAxis("Vertical") > 0 && !Input.GetKey(KeyCode.Space) &&
            GetComponent<CarController>().IsCarBoosting())
        {
            if (engineSound.pitch < 3.5) engineSound.pitch += .01f;
        }
        else if (Input.GetAxis("Vertical") < 0 || Input.GetKey(KeyCode.Space))
        {
            if (engineSound.pitch < 1.5) engineSound.pitch += .01f;
            else if (engineSound.pitch > 1.5) engineSound.pitch -= .01f;
        }
        else
        {
            if (engineSound.pitch < 1) engineSound.pitch += .1f;
            else if (engineSound.pitch > 1) engineSound.pitch -= .1f;
        }

        if(MenuController.gameIsPaused)
        {
            engineSound.volume= 0;
        }
        else
        {
            engineSound.volume= .5f;
        }
    }
}
