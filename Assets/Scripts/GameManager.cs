using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Sliders")] 
    [SerializeField] private Slider Slider_playerInput;
    [SerializeField] private Slider Slider_fishReelStatus;
    [SerializeField] private Slider Slider_castLine;
    [SerializeField] private Slider Slider_fishCircle;

    [Header("GameObjects")] 
    [SerializeField] private GameObject BG_Yellow;
    [SerializeField] private GameObject BG_Green;
    [SerializeField] private GameObject SFX;
    [SerializeField] private GameObject Handle_CastLocation;
    [SerializeField] private GameObject LineCast;

    [Header("Display Text")]
    [SerializeField] private GameObject WinningUI;
    [SerializeField] private GameObject LoosingUI;
    
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

    private bool isCasting;
    private bool canRestart;      
    private bool define_NewCircleLocation;

    public enum GameState
    {
        Idle,
        CastingLine,
        BeforeFishing,
        Fishing,
        Game_Won,
        Game_Loss,
        Reward
    }
    [SerializeField] public GameState currentGameState;



    void Start()
    {
        currentGameState = GameState.Idle;
        sliderWidth = Slider_playerInput.GetComponent<RectTransform>().sizeDelta.x;
        current_yellowArea = default_yellowArea;
        current_greenArea = default_greenArea;
        define_NewCircleLocation = true;
    }

    void Update()
    {
        Update_Boundries();
        Update_GameState();
        Detect_StartFishing();
        Detect_EndFishing();
        Detect_RestartGame();
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
            ChooseFishCircleLocation();
            Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
            Slider_fishReelStatus.value = 0f;
            Slider_castLine.value = 0f;
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
            if(AlarmDelayCount.AlarmSetting(2f, true))
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
            define_NewCircleLocation = true;
            ShowFishingActionUI(false);
            WinningUI.SetActive(true);
            if(AlarmDelayCount.AlarmSetting(0.5f, false))
            {
                canRestart = true;
            }
        }
        
        if (currentGameState == GameState.Game_Loss)
        {
            define_NewCircleLocation = true;
            ShowFishingActionUI(false);
            LoosingUI.SetActive(true);
            if(AlarmDelayCount.AlarmSetting(0.5f, false))
            {
                canRestart = true;
            }
        }
    }
    
    void Detect_StartFishing()
    { 
        if ((PlayerInput.ButtonDown() || PlayerInput.ButtonHeld()) 
        && currentGameState == GameState.Idle && AlarmDelayCount.AlarmSetting(0.5f, false))
        {
            isCasting = false;
            currentGameState = GameState.CastingLine;
        }

        if(PlayerInput.ButtonHeld() && currentGameState == GameState.CastingLine)
        {
            Slider_castLine.value += (playerCastForce * Time.deltaTime);
            isCasting = true;
        } 
        
        if (isCasting && !PlayerInput.ButtonHeld())
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
            RandomItem_Generator.PickRandomItem();
            // Display Reward
        }
        else if (isLoss)
        {
            currentGameState = GameState.Game_Loss;
        }
    }

    void Detect_RestartGame()
    {
        if (canRestart && PlayerInput.ButtonDown())
        {  
            currentGameState = GameState.Idle; 
            canRestart = false;
        }
    }

    void ChooseFishCircleLocation()
    {
        if(define_NewCircleLocation)
        {
            fishCircleLocation = Random.Range(Slider_fishCircle.minValue * 0.1f, Slider_fishCircle.maxValue);
            Slider_fishCircle.value = fishCircleLocation;
            define_NewCircleLocation = false;
        }
    }

    void Define_SliderAreas()
    {
        float castScore = Mathf.Abs(Slider_castLine.value - fishCircleLocation);
        current_yellowArea = default_yellowArea * (100 - castScore) * 0.01f;
        current_greenArea = default_greenArea * (100 - castScore) * 0.01f;
    }

    void ShowFishingActionUI(bool choice)
    {
        Slider_playerInput.gameObject.SetActive(choice);
        Slider_fishReelStatus.gameObject.SetActive(choice);
        Handle_CastLocation.SetActive(choice);
        LineCast.SetActive(choice);
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

        Slider_playerInput.value += (gameMoveSpeed * Time.deltaTime);
        Slider_fishReelStatus.value += (currentSpeed * Time.deltaTime);
    }

    void PlayerFishingInput()
    {
        if(PlayerInput.ButtonHeld())
        {
            Slider_playerInput.value -= (playerMoveSpeed * Time.deltaTime);
        }
    }
}