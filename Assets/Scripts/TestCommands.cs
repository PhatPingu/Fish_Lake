using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommands : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Reward_Generator.PickRandomItem(1);
        }        
    }
}
