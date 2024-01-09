
using UnityEngine;

public class FarmLand : MonoBehaviour
{
    #region VARIABLES
    private PickUpItem _cropPickUpItem;
    
    [SerializeField] private GameObject cropPrefab;
    private GameObject _crop;
    private GameObject _seed;
    private GameObject _sprout;
    private GameObject _mature;
    
    #endregion
    
    #region UNITY FUNCTIONS
    private void Start()
    {
        PlantCrop();
    }
    #endregion
    
    #region

    public void PlantCrop()
    {
        _crop=Instantiate(cropPrefab, transform);
        _crop.GetComponent<CropTimer>().SetFarmLand(this);
        
        _seed= _crop.transform.GetChild(0).gameObject;
        _sprout= _crop.transform.GetChild(1).gameObject;
        _mature= _crop.transform.GetChild(2).gameObject;
        
        _cropPickUpItem = _crop.GetComponent<PickUpItem>();
        _cropPickUpItem.item.isDefaultItem = true;
    }
    public void ChangeGameObject(CropTimer.CropState cropState)
    {
        switch (cropState)
        {
            case CropTimer.CropState.Seed:
                _seed.SetActive(true);
                _sprout.SetActive(false);
                _mature.SetActive(false);
                break;
            case CropTimer.CropState.Sprout:
                _seed.SetActive(false);
                _sprout.SetActive(true);
                _mature.SetActive(false);
                break;
            case CropTimer.CropState.Mature:
                _seed.SetActive(false);
                _sprout.SetActive(false);
                _mature.SetActive(true);
                _cropPickUpItem.item.isDefaultItem = false;
                break;
            default:
               Debug.Log("Invalid Crop State");
                break;
        }
    }
    
    #endregion

   
}
