using System.Collections;
using DG.Tweening;
using EasyButtons;
using UnityEngine;

public class TreehouseRootsSystem : MonoBehaviour
{
    private int _rootStage;
    
    [SerializeField]
    private float _rootsGrowthSpeed = 2f;

    [SerializeField]
    private int[] rootUpgradeCosts;
    
    [SerializeField]
    private GameObject[] rootGameObjects;

    [SerializeField]
    private GameObject _spreadRootsButton;

    [SerializeField]
    private float[] rootStageWaterFillPerSeconds;

    [SerializeField]
    private WaterSystem waterSystem;
    
    public void Awake()
    {
        for (int i = 0; i < rootGameObjects.Length; i++)
        {
            if (i == 0)
            {
                rootGameObjects[i].SetActive(true);
            }
            else
            {
                rootGameObjects[i].SetActive(false);
            }
        }

        waterSystem.FillSpeedPerSecond = rootStageWaterFillPerSeconds[0];
    }

    public void TryGrowRoots()
    {
        if (ResourceSystem.Instance.TryDecreaseResource(Resource.Hearts, rootUpgradeCosts[_rootStage]))
        {
            StartCoroutine(GrowRoots());
        }
    }

    [Button]
    public void DEBUG_GrowRoots()
    {
        StartCoroutine(GrowRoots());
    }
    
    public IEnumerator GrowRoots()
    {
        if (_rootStage >= 2)
        {
            yield break;
        }
        
        waterSystem.FillSpeedPerSecond = rootStageWaterFillPerSeconds[_rootStage];

        var treehouseManager = FindObjectOfType<TreehouseManager>();
  
        _rootStage++;

        DOTween.To(
            ()=> treehouseManager.TreeMaterial.GetFloat("_ReplaceSlider"), 
            x=> treehouseManager.TreeMaterial.SetFloat("_ReplaceSlider", x), 
            1, 0.5f);

        DOTween.To(
            () => treehouseManager.ResourceUIAlpha, 
            x => treehouseManager.ResourceUIAlpha = x, 
            0, 0.5f);

        yield return new WaitForSeconds(0.5f);
        
        rootGameObjects[_rootStage].SetActive(true);
        rootGameObjects[_rootStage].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        rootGameObjects[_rootStage].transform.DOScale(new Vector3(1, 1, 1), _rootsGrowthSpeed);
        
        yield return new WaitForSeconds(_rootsGrowthSpeed + 0.2f);

        DOTween.To(
            ()=> treehouseManager.TreeMaterial.GetFloat("_ReplaceSlider"), 
            x=> treehouseManager.TreeMaterial.SetFloat("_ReplaceSlider", x), 
            0, 0.5f);

        DOTween.To(
            () => treehouseManager.ResourceUIAlpha, 
            x => treehouseManager.ResourceUIAlpha = x, 
            1, 0.5f);

        if (_rootStage == 2)
        {
            _spreadRootsButton.SetActive(false);
        }
    }
}