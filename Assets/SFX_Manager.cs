using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{ 
    [SerializeField] private AudioClip[] sfx_List;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameManager GameManager;

    [SerializeField] private GameManager.GameState lastGameState;

    void Update()
    {
        Play_SFX();
    }

    void Play_SFX()
    {
        if (GameManager.currentGameState == GameManager.GameState.CastingLine
        &&                 lastGameState != GameManager.GameState.CastingLine)
        {
            audioSource.PlayOneShot(sfx_List[0]);
            lastGameState = GameManager.GameState.CastingLine;
        }

        if (GameManager.currentGameState == GameManager.GameState.Game_Won
        &&                 lastGameState != GameManager.GameState.Game_Won)
        {
            audioSource.PlayOneShot(sfx_List[1]);
            lastGameState = GameManager.GameState.Game_Won;
        }

        if (GameManager.currentGameState == GameManager.GameState.Game_Loss
        &&                 lastGameState != GameManager.GameState.Game_Loss)
        {
            audioSource.PlayOneShot(sfx_List[2]);
            lastGameState = GameManager.GameState.Game_Loss;
        }

        if (GameManager.currentGameState == GameManager.GameState.Reward
        &&                 lastGameState != GameManager.GameState.Reward)
        {
            audioSource.PlayOneShot(sfx_List[3]);
            lastGameState = GameManager.GameState.Reward;
        }
    }
}
    