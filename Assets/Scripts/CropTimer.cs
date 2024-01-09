using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTimer : MonoBehaviour
{
    #region Enum
    
    public enum CropState
    {
        Seed,
        Sprout,
        Mature
    } 

    #endregion
    
    #region Variables
    
    [SerializeField] private TimeComponent cropTimer = new TimeComponent();
    public CropState cropState;
    private FarmLand _farmLand;

    #endregion
    
    #region Fields
    
    public float seedDuration;
    public float sproutDuration;
    public float randomOffsetThreshold;
    
    #endregion
    
    #region Unity Functions
    
    private void Start()
    {
        cropTimer.timerDuration = seedDuration;
        cropTimer.onTimerEnd.AddListener(OnMinuteEnd);
        cropTimer.StartTimer();
        cropState = CropState.Seed;
    }
    public void SetFarmLand(FarmLand farmLand)
    {
        _farmLand = farmLand;
    }
    
    private void Update()
    {
        cropTimer.Update();
    }
    
    #endregion
    
    #region Event Binded Functions
    
    private void OnMinuteEnd()
    {
        switch (cropState)
        {
            case CropState.Seed:
                cropState = CropState.Sprout;
                cropTimer.timerDuration = seedDuration;
                cropTimer.StartTimer();
                _farmLand.ChangeGameObject(cropState);
                
                break;
            case CropState.Sprout:
                cropState = CropState.Mature;
                cropTimer.timerDuration = sproutDuration;
                cropTimer.StartTimer();
                _farmLand.ChangeGameObject(cropState);
                Debug.Log("Crop is sprout!");
                break;
            case CropState.Mature:
                _farmLand.ChangeGameObject(cropState);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    #endregion
}
