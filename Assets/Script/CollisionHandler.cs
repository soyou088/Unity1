using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 0;

    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    ParticleSystem particleSystem;
    CollisionHandler collisionHandler;

    bool isTransitioning = false;
    bool collisionDisable = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Finish":
                Debug.Log("Congrats, yo, you finished!");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Sorry, you blew up!");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        Invoke("LoadNextLevel", LevelLoadDelay);
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        successParticles.Play();
           
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        Invoke("ReloadLevel", LevelLoadDelay);
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
}
