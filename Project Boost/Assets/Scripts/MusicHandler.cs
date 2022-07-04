using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Music();
    }
    void Update()
    {
    }

    // Update is called once per frame
    void Music()
    {
        if (audioSource.isPlaying == true)
        {
            audioSource.Stop();
        }
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
        
        
    }


}
