using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float verticalThrust = 100f;
    [SerializeField] float horizontalThrust = 1f;
    [SerializeField] AudioClip thrustAudioClip;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster1;
    [SerializeField] ParticleSystem leftBooster2;
    [SerializeField] ParticleSystem rightBooster1;
    [SerializeField] ParticleSystem rightBooster2;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
    }

    void StartThrusting()
    {
        Vector3 force = Vector3.up * verticalThrust * Time.deltaTime;
        rb.AddRelativeForce(force);

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioClip);
        }
    }

    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopAllThrusters();
        }
    }
    void RotateLeft()
    {
        ApplyRotation(horizontalThrust);

        if (!rightBooster1.isPlaying)
        {
            leftBooster1.Stop();
            leftBooster2.Stop();
            rightBooster1.Play();
            rightBooster2.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-horizontalThrust);

        if (!leftBooster1.isPlaying)
        {
            leftBooster1.Play();
            leftBooster2.Play();
            rightBooster1.Stop();
            rightBooster2.Stop();
        }
    }

    void StopAllThrusters()
    {
        leftBooster1.Stop();
        leftBooster2.Stop();
        rightBooster1.Stop();
        rightBooster2.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreezing rotation so physics system can take over
    }

}
