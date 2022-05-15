using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Sliders")] 
    [SerializeField] private Slider Slider_playerInput;
    [SerializeField] private Slider Slider_fishReelStatus;
    [SerializeField] private Slider Slider_fishCircle;

    [Header("GameObjects")] 
    [SerializeField] private GameObject BG_Yellow;
    [SerializeField] private GameObject BG_Green;

    [Header("Display Text")]
    [SerializeField] private GameObject WinningUI;
    [SerializeField] private GameObject LoosingUI;
    [SerializeField] private GameObject DelayCount_02;
    [SerializeField] private GameObject DelayCount_01;
    
    [Header("Game Attributes")]
    [SerializeField] private float playerCastForce;
    [SerializeField] private float gameMoveSpeed;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float fishReelSpeed;
    [SerializeField] private float fishReelSlowSpeed;
    
    [Header("Slider Area in Percents")]
    [SerializeField] private float default_yellowArea;
    [SerializeField] private float default_greenArea;
    [SerializeField] private float current_yellowArea;
    [SerializeField] private float current_greenArea;

    private float sliderWidth;
    
    [Header("Info for Debug")]
    [SerializeField] private float fishCircleLocation;
    [SerializeField] private float yellow_BoundryPercent;
    [SerializeField] private float green_BoundryPercent;
    [SerializeField] private float yellow_BoundryValue;
    [SerializeField] private float green_BoundryValue;
    [SerializeField] private float moveDirection = 1f;
    [SerializeField] private float alarmStart_time;

    private bool input_MouseButtonDown_0;
    private bool input_MouseButtonHeld_0;
    private bool isCasting;
    private bool canRestart;      
    private bool restartAlarm;

    private enum GameState
    {
        Idle,
        CastingLine,
        BeforeFishing,
        Fishing,
        Game_Won,
        Game_Loss
    }
    [SerializeField] private GameState currentGameState;



    void Start()
    {
        currentGameState = GameState.Idle;
        sliderWidth = Slider_playerInput.GetComponent<RectTransform>().sizeDelta.x;
        alarmStart_time = 0f;
        current_yellowArea = default_yellowArea;
        current_greenArea = default_greenArea;
    }

    void Update()
    {
        DetectInput();
        Update_Boundries();
        Update_GameState();
        Detect_StartFishing();
        Detect_EndFishing();
        Detect_RestartGame();
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

    void Update_Boundries()
    {
        yellow_BoundryPercent = Mathf.Abs(100 - current_yellowArea) * 0.5f;
        green_BoundryPercent = Mathf.Abs(100 - current_greenArea) * 0.5f;

        yellow_BoundryValue = yellow_BoundryPercent * sliderWidth * 0.01f;
        green_BoundryValue = green_BoundryPercent * sliderWidth * 0.01f;

        RectTransform yellowTransform = BG_Yellow.GetComponent<RectTransform>();
        yellowTransform.offsetMin = new Vector2 (yellow_BoundryValue, yellowTransform.offsetMin.y);
        yellowTransform.offsetMax = new Vector2 (-yellow_BoundryValue, yellowTransform.offsetMax.y);

        RectTransform greenTransform = BG_Green.GetComponent<RectTransform>();
        greenTransform.offsetMin = new Vector2 (green_BoundryValue, greenTransform.offsetMin.y);
        greenTransform.offsetMax = new Vector2 (-green_BoundryValue, greenTransform.offsetMax.y);

        if (yellow_BoundryValue > green_BoundryValue)
        {
            yellow_BoundryValue = green_BoundryValue;
        }
    }

    void Update_GameState()
    {
        if (currentGameState == GameState.Idle)
        {
            ChooseMoveDirection();
            ChooseFishCircleLocation();
            Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
            Slider_fishReelStatus.value = 0f;
            Slider_fishCircle.value = 0f;
            current_yellowArea = default_yellowArea;
            current_greenArea = default_greenArea;
            ShowFishingActionUI(false);
            WinningUI.SetActive(false);
            LoosingUI.SetActive(false);
        }
        
        if (currentGameState == GameState.CastingLine)
        {

        }

        if (currentGameState == GameState.BeforeFishing)
        {
            Define_SliderAreas();
            ShowFishingActionUI(true);
            if(Alarm_DelayCount(2f, true))
            {
                currentGameState = GameState.Fishing;
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
            if(Alarm_DelayCount(0.5f, false))
            {
                canRestart = true;
            }
        }
        
        if (currentGameState == GameState.Game_Loss)
        {
            ShowFishingActionUI(false);
            LoosingUI.SetActive(true);
            if(Alarm_DelayCount(0.5f, false))
            {
                canRestart = true;
            }
        }
    }
    
    bool Alarm_DelayCount(float alarm_time, bool displayTimer)  // this uses a placeholder DelayCount_Animation
    {
        if(restartAlarm)
        {
            alarmStart_time = alarm_time;
            restartAlarm = false;
        }

        alarmStart_time -= Time.deltaTime;

        if(alarmStart_time < 0)
        {
            restartAlarm = true;
            DelayCount_02.SetActive(false);
            DelayCount_01.SetActive(false);     
            return true;
        }
        else if(alarmStart_time < 1 && displayTimer)
        {
            DelayCount_02.SetActive(false);
            DelayCount_01.SetActive(true);
            return false;
        }
        else if (displayTimer)
        {
            DelayCount_02.SetActive(true);
            DelayCount_01.SetActive(false);
            return false;
        }
        else
        {
            return false;
        }
    }        

    void Detect_StartFishing()
    { 
        if ((input_MouseButtonDown_0 || input_MouseButtonHeld_0) 
        && currentGameState == GameState.Idle && Alarm_DelayCount(0.5f, false))
        {
            isCasting = false;
            currentGameState = GameState.CastingLine;
        }

        if(input_MouseButtonHeld_0 && currentGameState == GameState.CastingLine)
        {
            Slider_fishCircle.value += (playerCastForce * Time.deltaTime);
            isCasting = true;
        } 
        
        if (isCasting && !input_MouseButtonHeld_0)
        {
            isCasting = false;
            currentGameState = GameState.BeforeFishing;
        } 
    }

    void Detect_EndFishing()
    {
        bool isWin = Slider_fishReelStatus.value == Slider_fishReelStatus.maxValue;
        bool isLoss =   Slider_playerInput.value <= yellow_BoundryPercent
        ||              Slider_playerInput.value >= Mathf.Abs(100-yellow_BoundryPercent);

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
    }

    void Detect_RestartGame()
    {
        if (canRestart && input_MouseButtonDown_0)
        {  
            currentGameState = GameState.Idle; 
            canRestart = false;
        }
    }

    void ChooseFishCircleLocation()
    {
        fishCircleLocation = Random.Range(Slider_fishCircle.minValue * 0.1f, Slider_fishCircle.maxValue);
    }

    void Define_SliderAreas()
    {
        float castScore = Mathf.Abs(Slider_fishCircle.value - fishCircleLocation);
        current_yellowArea = default_yellowArea - (castScore * 0.01f);
        current_greenArea = default_greenArea - (castScore * 0.01f);
    }

    void ShowFishingActionUI(bool choice)
    {
        Slider_playerInput.gameObject.SetActive(choice);
        Slider_fishReelStatus.gameObject.SetActive(choice);
    }

    void Game_MoveSliders()
    {
        float currentSpeed;
        if (    Slider_playerInput.value <= green_BoundryPercent
        ||      Slider_playerInput.value >= Mathf.Abs(100-green_BoundryPercent))
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