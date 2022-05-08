using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider Slider_playerInput;
    [SerializeField] private Slider Slider_fishReelStatus;
    
    [SerializeField] private float gameMoveSpeed;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float fishReelSpeed;
    
    [SerializeField] private int moveDirection = 1;

    void Start()
    {
        Slider_playerInput.value = Slider_playerInput.maxValue * 0.5f;
        ChooseMoveDirection();
    }

    void FixedUpdate()
    {
        MoveSliderAutomatically();
        UpdateFishReelStatus();
        PlayerInput();
    }

    void MoveSliderAutomatically()
    {
        Slider_playerInput.value += (gameMoveSpeed * moveDirection);
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

    void PlayerInput()
    {
        if(Input.GetMouseButton(0))
        {
            Slider_playerInput.value -= (playerMoveSpeed * moveDirection);
        }
    }
}
