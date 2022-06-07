using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AlarmDelayCount
{
    static public GameObject DelayCount_02;
    static public GameObject DelayCount_01;
    
    static private float alarmStart_time;
    static private bool restartAlarm;
    
    
    public static bool AlarmSetting(float alarm_time, bool displayTimer)
    // This uses a placeholder DelayCount_Animation
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
}
