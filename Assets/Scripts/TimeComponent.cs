using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimeComponent
{
    #region Variables

    [SerializeField] public UnityEvent onTimerStart;
    [SerializeField] public UnityEvent onTimerEnd;
    [SerializeField] public UnityEvent onTimerPause;
    [SerializeField] public float timerDuration;
    private float timerStartTime;
    [SerializeField] private bool isTimerRunning;
    [SerializeField] public float timePassed => Time.time - timerStartTime;

    #endregion

    #region Timer Functions

    public void StartTimer()
    {
        if (isTimerRunning)
        {
            return;
        }
        isTimerRunning = true;
        onTimerStart.Invoke();
        timerStartTime = Time.time;
    }
    
    public void StopTimer()
    {
        if (!isTimerRunning)
        {
            return;
        }
        isTimerRunning = false;
        onTimerEnd.Invoke();
    }
    
    public void PauseTimer()
    {
        if (!isTimerRunning)
        {
            return;
        }
        isTimerRunning = false;
        onTimerPause.Invoke();
    }
    
    public void ResumeTimer()
    {
        if (isTimerRunning)
        {
            return;
        }
        isTimerRunning = true;
    }
    

    #endregion

    #region Event Bindings
    
    public void BindOnTimerStart(UnityAction action)
    {
        onTimerStart.AddListener(action);
    }
    
    public void BindOnTimerEnd(UnityAction action)
    {
        onTimerEnd.AddListener(action);
    }
    
    public void BindOnTimerPause(UnityAction action)
    {
        onTimerPause.AddListener(action);
    }
    
    public void UnbindOnTimerStart(UnityAction action)
    {
        onTimerStart.RemoveListener(action);
    }
    
    public void UnbindOnTimerEnd(UnityAction action)
    {
        onTimerEnd.RemoveListener(action);
    }
    
    public void UnbindOnTimerPause(UnityAction action)
    {
        onTimerPause.RemoveListener(action);
    }

    #endregion

    #region Unity Functions

    public void Update()
    {
        if (!isTimerRunning)
        {
            return;
        }
        if (Time.time - timerStartTime >= timerDuration)
        {
            StopTimer();
        }
    }

    #endregion
    
}
