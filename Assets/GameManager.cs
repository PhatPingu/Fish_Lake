using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider Slider_playerInput;
    [SerializeField] private Slider Slider_fishReelStatus;

    [SerializeField] private GameObject WinningUI;
    [SerializeField] private GameObject LoosingUI;
    
    [SerializeField] private float gameMoveSpeed;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float fishReelSpeed;
    [SerializeField] private float looseBoundry;
    
    [SerializeField] private int moveDirection = 1;

    [SerializeField] private bool fishingInProgress;
    [SerializeField] private bool canRestartGame;

    void Start()
    {
        Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
        ChooseMoveDirection();
    }

    void FixedUpdate()
    {
        Detect_StartFishing();
        MoveSliderAutomatically();
        UpdateFishReelStatus();
        PlayerFishingInput();
        Detect_EndFishing();
    }

    void Detect_StartFishing()
    {
        if(Input.GetMouseButtonDown(0) && !fishingInProgress)
        {
            fishingInProgress = true;
            Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
            ShowFishingActionUI(true);
        }
    }

    void Detect_EndFishing()
    {
      
        bool isWinner = Slider_fishReelStatus.value == Slider_fishReelStatus.maxValue;
        bool isFail = Slider_playerInput.value == Mathf.Abs(looseBoundry);
        if(isWinner)
        {
            // Choose Random Reward from rewardPool
            // Display Reward
            ShowFishingActionUI(false);
            WinningUI.SetActive(true);
            Detect_RestartGame();
        }
        else if (isFail)
        {
            ShowFishingActionUI(false);
            LoosingUI.SetActive(true);
            Detect_RestartGame();
        }

        void Detect_RestartGame()
        {
            canRestartGame = true;
            if(Input.GetMouseButtonDown(0))
            {
                fishingInProgress = false;
                isWinner = false;
                isFail = false;
                canRestartGame = false;
                WinningUI.SetActive(false);
                LoosingUI.SetActive(false);
            }
        }
    }

    void ShowFishingActionUI(bool choice)
    {
        Slider_playerInput.gameObject.SetActive(choice);
        Slider_fishReelStatus.gameObject.SetActive(choice);
    }

    void MoveSliderAutomatically()
    {
        if(!canRestartGame)
        {
            Slider_playerInput.value += (gameMoveSpeed * moveDirection);
        }
    }

    void UpdateFishReelStatus()
    {
        Slider_fishReelStatus.value += fishReelSpeed;
    }

    void ChooseMoveDirection()
    {
        float direction = Random.Range(0,1);
        {
            if (direction < 0.5f)
            {
                moveDirection = 1;
            }
            else{
                moveDirection = -1;
            }
        }
    }

    void PlayerFishingInput()
    {
        if(Input.GetMouseButton(0) && fishingInProgress && !canRestartGame)
        {
            Slider_playerInput.value -= (playerMoveSpeed * moveDirection);
        }
    }
}
