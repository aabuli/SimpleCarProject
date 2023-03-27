using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;


public class CarParticles : MonoBehaviour
{
    public ParticleSystem debris;
    public ParticleSystem speedlines;
    public ParticleSystem speedlinesCam;
    public ParticleSystem speedImpact;

    private bool impactSpawned = false;

    private void OnEnable()
    {
        debris.Stop();
        speedlines.Stop();
        speedlinesCam.Stop();
        speedImpact.Stop();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            debris.Play();
        }
        else
        {
            debris.Stop();
        }

        if (GetComponent<CarController>().IsCarBoosting())
        {
            speedlines.Play();
            speedlinesCam.Play();
            if (!speedImpact.isPlaying && !impactSpawned)
            {
                speedImpact.Play();
                impactSpawned = true;
            }
        }
        else
        {
            if (speedlines.isPlaying)
            {
                speedlines.Stop();
                speedlinesCam.Stop();
                impactSpawned = false;
            }
        }
    }
}
