using UnityEngine;

public static class PlayerInput
{
    private static void Update()
    {
        ButtonDown();
        ButtonHeld();
    }

    public static bool ButtonDown()   
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ButtonHeld()   
    {
        if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
