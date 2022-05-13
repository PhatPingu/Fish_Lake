using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Sliders")] 
    [SerializeField] private Slider Slider_playerInput;
    [SerializeField] private Slider Slider_fishReelStatus;

    [Header("Display Text")]
    [SerializeField] private GameObject WinningUI;
    [SerializeField] private GameObject LoosingUI;
    [SerializeField] private GameObject DelayCount_02;
    [SerializeField] private GameObject DelayCount_01;
    
    [Header("Game Attributes")]
    [SerializeField] private float gameMoveSpeed;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float fishReelSpeed;
    [SerializeField] private float fishReelSlowSpeed;
    [SerializeField] private float slowReelBoundry;
    [SerializeField] private float looseBoundry;
    
    [Header("Info for Debug")]
    [SerializeField] private float moveDirection = 1f;
    [SerializeField] private float alarmStart_time = 2f;

    private bool input_MouseButtonDown_0;
    private bool input_MouseButtonHeld_0;
    
    private enum GameState
    {
        Idle,
        BeforeFishing,
        Fishing,
        Game_Won,
        Game_Loss
    }
    [SerializeField] private GameState currentGameState;



    void Start()
    {
        currentGameState = GameState.Idle;
    }

    void Update()
    {
        DetectInput();
        Update_GameState();
        Detect_StartFishing();
        Detect_EndFishing();
    }

    void DetectInput()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            input_MouseButtonDown_0 = true;
        }
        else
        {
            input_MouseButtonDown_0 = false;
        }
        
        if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            input_MouseButtonHeld_0 = true;
        }
        else
        {
            input_MouseButtonHeld_0 = false;
        }
    }

    void Update_GameState()
    {
        if (currentGameState == GameState.Idle)
        {
            ChooseMoveDirection();
            Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
            Slider_fishReelStatus.value = 0f;
            ShowFishingActionUI(false);
            WinningUI.SetActive(false);
            LoosingUI.SetActive(false);
        }

        if (currentGameState == GameState.BeforeFishing)
        {
            ShowFishingActionUI(true);
            if(DelayCount_Alarm(alarmStart_time))
            {
                currentGameState = GameState.Fishing;
                alarmStart_time = 2f;
            }
        }

        if (currentGameState == GameState.Fishing)
        {
            Game_MoveSliders();
            PlayerFishingInput();
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

        bool DelayCount_Alarm(float resetAlarm_time)  // this uses a placeholder DelayCount_Animation
        {
            alarmStart_time -= Time.deltaTime;

            if(alarmStart_time < 0)
            {
                alarmStart_time = resetAlarm_time;
                DelayCount_02.SetActive(false);
                DelayCount_01.SetActive(false);
                return true;
            }
            else if(alarmStart_time < 1)
            {
                DelayCount_02.SetActive(false);
                DelayCount_01.SetActive(true);
                return false;
            }
            else
            {
                DelayCount_02.SetActive(true);
                DelayCount_01.SetActive(false);
                return false;
            }
        }        
    }

    void Detect_StartFishing()
    {
        if(input_MouseButtonDown_0 && currentGameState == GameState.Idle)
        {
            currentGameState = GameState.BeforeFishing;
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
            if(input_MouseButtonDown_0)
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
        float currentSpeed;
        if (    Slider_playerInput.value <= slowReelBoundry 
        ||      Slider_playerInput.value >= Mathf.Abs(100-slowReelBoundry))
        {
            currentSpeed = fishReelSlowSpeed;
        }
        else
        {
            currentSpeed = fishReelSpeed;
        }

        Slider_playerInput.value += (gameMoveSpeed * moveDirection * Time.deltaTime);
        Slider_fishReelStatus.value += (currentSpeed * Time.deltaTime);
    }

    void ChooseMoveDirection()
    {
        float direction = Random.Range(0f,1f);
        {
            if (direction < 0.5f)
            {
                moveDirection = 1f;
            }
            else{
                moveDirection = -1f;
            }
        }
    }

    void PlayerFishingInput()
    {
        if(input_MouseButtonHeld_0)
        {
            Slider_playerInput.value -= (playerMoveSpeed * moveDirection * Time.deltaTime);
        }
    }
}
