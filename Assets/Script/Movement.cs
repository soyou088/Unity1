using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainTrust = 1000.0f;
    [SerializeField] float rotationTrust = 100.0f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem LeftEngineParticle;
    [SerializeField] ParticleSystem RightEngineParticle;


    Rigidbody rb;
    AudioSource audioSource;
    ParticleSystem particleSystem;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
       
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RightRotationStart();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            LeftRotationStart();
        }
        else
        {
            RotationStop();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }
    void LeftRotationStart()
    {
        ApplyRotation(-rotationTrust);
        if (!LeftEngineParticle.isPlaying)
        {
            LeftEngineParticle.Play();
        }
    }
    void RightRotationStart()
    {
        ApplyRotation(rotationTrust);
        if (!RightEngineParticle.isPlaying)
        {
            RightEngineParticle.Play();
        }
    }
    void RotationStop()
    {
        RightEngineParticle.Stop();
        LeftEngineParticle.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
  