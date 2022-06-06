using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource CurrentMusic;

    [SerializeField] private AudioClip[] musicList;
    
    [SerializeField] private float currentClipLength;

    void Start()
    {
        CurrentMusic.clip = musicList[Random.Range(0, musicList.Length)];
        CurrentMusic.Play();
        currentClipLength = CurrentMusic.clip.length;
    }

    void Update()
    {
        currentClipLength -= Time.deltaTime;
        if(currentClipLength < 0)
        {
            CurrentMusic.clip = musicList[Random.Range(0, musicList.Length)];
            currentClipLength = CurrentMusic.clip.length;
            CurrentMusic.Play();
        }
    }
}
