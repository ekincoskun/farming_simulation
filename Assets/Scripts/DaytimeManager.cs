using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaytimeManager : MonoBehaviour
{
    #region Enum

    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    #endregion

    #region Variables

    [SerializeField] public TimeComponent dayTimer = new TimeComponent();

    [SerializeField] public int _day;
    [SerializeField] private Season _season;

    [SerializeField] private int _hour;
    [SerializeField] private int _minute;

    public float secondsPerMinute;
    public float dayDuration;
    public float daysPerSeason;

    #endregion

    #region Fields

    public int day { get; private set; }
    public Season season { get; private set; }
    public int hour { get; private set; }
    public int minute { get; private set; }

    #endregion

    void Start()
    {
        dayTimer.timerDuration = secondsPerMinute;
        dayTimer.onTimerEnd.AddListener(OnMinuteEnd);
        dayTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        dayTimer.Update();
    }

    #region Event Binded Functions

    private void OnMinuteEnd()
    {
        minute++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
            if (hour >= dayDuration)
            {
                hour = 0;
                day++;
                if (day >= daysPerSeason)
                {
                    day = 0;
                    switch (season)
                    {
                        case Season.Spring:
                            season = Season.Summer;
                            break;
                        case Season.Summer:
                            season = Season.Autumn;
                            break;
                        case Season.Autumn:
                            season = Season.Winter;
                            break;
                        case Season.Winter:
                            season = Season.Spring;
                            break;
                    }
                }
            }
        }

        dayTimer.StartTimer();
    }

    #endregion

    #region Event Bindings

    public void BindOnDayEnd(UnityAction action)
    {
        dayTimer.onTimerEnd.AddListener(action);
    }

    public void BindOnHourEnd(UnityAction action)
    {
        dayTimer.onTimerEnd.AddListener(action);
    }

    public void BindOnMinuteEnd(UnityAction action)
    {
        dayTimer.onTimerEnd.AddListener(action);
    }

    #endregion
}