using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip deathAudioClip;
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    BoxCollider boxCollider;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        DebugKeys();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(isTransitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Launch Pad":
                Debug.Log("Collided with Launch Pad!");
                break;
            case "Landing Pad":
                StartSuccessSequence();
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                StartCrashSequence();
                Invoke("RestartLevel", delay);
                break;
           }
    }

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudioClip);
        deathParticles.Play();
        GetComponent<Movement>().enabled = false;
        
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudioClip);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (boxCollider.isTrigger)
            {
                boxCollider.isTrigger = false;
            }
            else
            {
                boxCollider.isTrigger = true;
            }
        }
    }

}
