using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaterSystem : MonoBehaviour
{
    private const float SECOND = 1.0f;

    [SerializeField]
    private TreehouseManager _treehouseManager;

    // The rate of the water drips per second
    [SerializeField]
    private float _waterDripsPerSecond = 1f;

    [SerializeField]
    private WaterLevels _waterLevels;

    // The amount you need to fill the water meter
    [SerializeField]
    private float _waterMeterAmount;   

    // How much of the meter is filled
    [SerializeField]
    private float _waterMeterFill;

    public float Second = SECOND;

    public bool MeterIsFilled;

    [Header("Water UI")]
    [SerializeField]
    private TextMeshProUGUI _fillCounter;

    [SerializeField]
    private Image _fillCircle;

    //[SerializeField]
    //private Image _fillBar;

    void Start()
    {
        SetWaterMeterAmount();
    }

    // Update is called once per frame
    void Update()
    {
        if (MeterIsFilled)
        {
            _treehouseManager.IncreaseTreeHeight();
            SetWaterMeterAmount();
            ResetWaterFillAmount();
        }
        
        //Second tick for drip system
        Second -= Time.deltaTime;        

        if (Second <= 0)
        {
            TickComplete();
        }

        // UI updates
        _fillCircle.fillAmount = Second;
        _fillCounter.SetText(_waterDripsPerSecond.ToString());
        
    }

    void TickComplete()
    {
        FillMeter(_waterDripsPerSecond);        

        Second = SECOND;
    }

    void ResetWaterFillAmount()
    {
        _waterMeterFill = 0;
    }

    void SetWaterMeterAmount()
    {
        _waterMeterAmount = _waterLevels.WaterMeterAmounts[_treehouseManager.GetCurrentHeight];
    }

    void FillMeter(float fillAmount)
    {
        _waterMeterFill += fillAmount;

        //Fills the meter UI to match the fill amount
        //_fillBar.fillAmount = _waterMeterFill;
    }
}
