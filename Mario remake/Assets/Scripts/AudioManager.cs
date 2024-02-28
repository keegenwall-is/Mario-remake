using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    void Awake(){
        if(Instance ==null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else{
            Destroy(gameObject);
        }
    }
    public void CheckBG(){
        // if(audioSource.isPlaying = false){
        //     audioSource.Play();
        // }
    }
    public void Play(int index,bool bgOver = false){
        if(bgOver){
            audioSource.Stop();
        }
        // audioSource.PlayOneShot(audioSource.clip[index];
    }

    
    
}
