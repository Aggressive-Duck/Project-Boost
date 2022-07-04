using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{


    [SerializeField] float levelLoadDelay = 1.5f;
    [SerializeField] AudioClip FinishSfx;
    [SerializeField] AudioClip CrashSfx;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionKillSwitch = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatKeys();
    }

    void CheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
            Debug.Log("Cheat code L activated, next level loaded.");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionKillSwitch = !collisionKillSwitch; //toggle collision
            if (collisionKillSwitch == true)
            {
                Debug.Log("Cheat code C activated, collision disabled.");
            }
            if (collisionKillSwitch == false)
            {
                Debug.Log("Cheat code C activated, collision enabled.");
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionKillSwitch) { return; } //or just if (isTransitioning == false), the urrent one means if bool == false, return nothing, or else
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("friendly");
                    break;
                case "Finish":
                    StartFinishSequence();
                    break;
                default:
                    StartCrashSequence();
                    //Invoke("StartCrashSequence", 1f);//invoke uses string referance, delay one s when loading the method
                    break;
            }
        }
    }

    void StartFinishSequence()
    {
        isTransitioning = true;//the bool will reset to default type when the level reset
        // TODO add particle effect upon finish
        audioSource.Stop();
        audioSource.PlayOneShot(FinishSfx);
        successParticles.Play();
        //we'll need to call it (particals) indiependently
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        // TODO add particle effect upon crash
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSfx);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);// or use the scene index of sandbox, which is zero
        //this is just longer and clearer version of SceneManager.LoadScene(0);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)//example, load scene 3 (doesnt exist), and it also equals the total scene count, 0 1 2, total 3 levels
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}