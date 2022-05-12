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

    private enum GameState
    {
        Idle,
        Fishing,
        Game_Won,
        Game_Loss
    }
    [SerializeField] private GameState currentGameState;



    void Start()
    {
        currentGameState = GameState.Idle;
        ChooseMoveDirection();
    }

    void Update()
    {
        PlayerFishingInput();
        Update_GameState();
        Detect_StartFishing();
        Detect_EndFishing();
    }

    void Update_GameState()
    {
        if (currentGameState == GameState.Idle)
        {
            Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
            Slider_fishReelStatus.value = 0f;
            WinningUI.SetActive(false);
            LoosingUI.SetActive(false);

        }
        if (currentGameState == GameState.Fishing)
        {
            ShowFishingActionUI(true);
            Game_MoveSliders();

        }
        if (currentGameState == GameState.Game_Won)
        {
            ShowFishingActionUI(false);
            WinningUI.SetActive(true);

        }
        if (currentGameState == GameState.Game_Loss)
        {
            ShowFishingActionUI(false);
            LoosingUI.SetActive(true);
        }
    }

    void Detect_StartFishing()
    {
        if(Input.GetMouseButtonDown(0) && currentGameState == GameState.Idle)
        {
            currentGameState = GameState.Fishing;
        }
    }

    void Detect_EndFishing()
    {
        bool isWin = Slider_fishReelStatus.value == Slider_fishReelStatus.maxValue;
        bool isLoss =   Slider_playerInput.value <= looseBoundry
        ||              Slider_playerInput.value >= Mathf.Abs(100-looseBoundry);

        if(isWin)
        {
            currentGameState = GameState.Game_Won;
            // Choose Random Reward from rewardPool
            // Display Reward
        }
        else if (isLoss)
        {
            currentGameState = GameState.Game_Loss;
        }
        Detect_RestartGame();

        void Detect_RestartGame()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(currentGameState == GameState.Game_Won 
                || currentGameState == GameState.Game_Loss)
                {
                    currentGameState = GameState.Idle;
                }
            }
        }
    }

    void ShowFishingActionUI(bool choice)
    {
        Slider_playerInput.gameObject.SetActive(choice);
        Slider_fishReelStatus.gameObject.SetActive(choice);
    }

    void Game_MoveSliders()
    {
        Slider_playerInput.value += (gameMoveSpeed * moveDirection * Time.deltaTime);
        Slider_fishReelStatus.value += (fishReelSpeed * Time.deltaTime);
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
        if(Input.GetMouseButton(0) && currentGameState == GameState.Fishing)
        {
            Slider_playerInput.value -= (playerMoveSpeed * moveDirection * Time.deltaTime);
        }
    }
}
