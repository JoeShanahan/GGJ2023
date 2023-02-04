using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class WaterSystem : MonoBehaviour
{
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
    }

    void OnProgression()
    {
        if (ProgressionManager.HasDone(ProgressStep.UnlockedWater))
        {
            StartCoroutine(ShowWaterUI());
        }
    }

    IEnumerator ShowWaterUI()
    {
        yield return new WaitForSeconds(1);

        FindObjectOfType<TutorialManager>().EnableTutorial(TutorialManager.TutorialID.WaterMeter);
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressionManager.HasDone(ProgressStep.UnlockedWater) == false)
            return;

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
