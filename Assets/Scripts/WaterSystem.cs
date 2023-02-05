using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class WaterSystem : MonoBehaviour
{
    private bool _shownWaterUITutorial;
    
    private const float SECOND = 1.0f;

    [SerializeField]
    private TreehouseManager _treehouseManager;

    // The rate of the water drips per second
    [SerializeField]
    private float _fillSpeedPerSecond = 1f;

    [SerializeField]
    private WaterLevels _waterLevels;

    // How much of the meter is filled
    [SerializeField]
    private float _currentFillAmount;

    [Header("Water UI")]
    [SerializeField]
    private Image _fillCircle;

    void Start()
    {
        ProgressionManager.Subscribe(OnProgression);
        _fillCircle.fillAmount = 0f;
    }

    void OnProgression()
    {
        if (ProgressionManager.HasDone(ProgressStep.CollectedTwigs))
        {
            if (_shownWaterUITutorial)
            {
                return;
            }

            _shownWaterUITutorial = true;
            StartCoroutine(ShowWaterUI());
        }
    }

    IEnumerator ShowWaterUI()
    {
        yield return new WaitForSeconds(1);

        var m = FindObjectOfType<TutorialManager>();
        m.EnableTutorial(TutorialManager.TutorialID.WaterMeter);
        
        yield return new WaitForSeconds(4);
        
        m.DismissTutorial();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressionManager.HasDone(ProgressStep.CollectedTwigs) == false)
            return;

        if (_treehouseManager.GetCurrentHeight >= _waterLevels.WaterMeterAmounts.Length )
        {
            return;
        }
        float currentMaximum = _waterLevels.WaterMeterAmounts[_treehouseManager.GetCurrentHeight];

        if (_currentFillAmount >= currentMaximum)
            return;
        
        _currentFillAmount += _fillSpeedPerSecond * Time.deltaTime;
        _fillCircle.fillAmount = _currentFillAmount / currentMaximum;
    }

    void ResetWaterFillAmount()
    {
        _currentFillAmount = 0;
    }
}
