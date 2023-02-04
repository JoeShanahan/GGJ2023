using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public enum ProgressStep { Unknown, WateredTree, CollectedTwigs, GrownTree, UnlockedWater, UnlockedRoots, UnlockedUpgrades }

public class ProgressionManager : MonoBehaviour
{
    private static ProgressionManager _instance;
    private static System.Action _onStepComplete;

    [SerializeField] private bool _hasWateredTree;
    [SerializeField] private bool _hasCollectedTwigs;
    [SerializeField] private bool _hasGrownTree;
    [SerializeField] private bool _hasUnlockedWater;
    [SerializeField] private bool _hasUnlockedRoots;
    [SerializeField] private bool _hasUnlockedUpgrades;
    
    void Start() => _instance = this;

    public static bool HasDone(ProgressStep step)
    {
        if (_instance == null)
            return false;

        switch (step)
        {
            case (ProgressStep.WateredTree):
                return _instance._hasWateredTree;
            case (ProgressStep.CollectedTwigs):
                return _instance._hasCollectedTwigs;
            case (ProgressStep.GrownTree):
                return _instance._hasGrownTree;
            case (ProgressStep.UnlockedWater):
                return _instance._hasUnlockedWater;
            case (ProgressStep.UnlockedRoots):
                return _instance._hasUnlockedRoots;
            case (ProgressStep.UnlockedUpgrades):
                return _instance._hasUnlockedUpgrades;
            default:
                return false;
        }
    }

    public static void CompleteStep(ProgressStep step)
    {
        if (_instance == null)
            return;

        switch (step)
        {
            case (ProgressStep.WateredTree):
                _instance._hasWateredTree = true;
                break;
            case (ProgressStep.CollectedTwigs):
                _instance._hasCollectedTwigs = true;
                break;
            case (ProgressStep.GrownTree):
                _instance._hasGrownTree = true;
                break;
            case (ProgressStep.UnlockedWater):
                _instance._hasUnlockedWater = true;
                break;
            case (ProgressStep.UnlockedRoots):
                _instance._hasUnlockedRoots = true;
                break;
            case (ProgressStep.UnlockedUpgrades):
                _instance._hasUnlockedUpgrades = true;
                break;
            default:
                break;
        }

        _instance.NotifySubscribers();
    }

    public static void Subscribe(System.Action act)
    {
        _onStepComplete += act;
    }

    [Button]
    private void NotifySubscribers()
    {
        _onStepComplete?.Invoke();
    }
}
