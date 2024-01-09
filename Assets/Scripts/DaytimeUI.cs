using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeUI : MonoBehaviour
{
    [SerializeField] private DaytimeManager daytimeManager;
    [SerializeField] private TMPro.TextMeshProUGUI seasonText;
    [SerializeField] private TMPro.TextMeshProUGUI dayText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;

    private void Start()
    {
        daytimeManager.BindOnDayEnd(OnDayEnd);
        daytimeManager.BindOnMinuteEnd(OnMinuteEnd);
    }

    private void OnDayEnd()
    {
        dayText.text = $"{daytimeManager.day:00}";
        seasonText.text = daytimeManager.season.ToString();
    }



    private void OnMinuteEnd()
    {
        timeText.text = $"{daytimeManager.hour:00}:{daytimeManager.minute:00}";
    }
    
}
