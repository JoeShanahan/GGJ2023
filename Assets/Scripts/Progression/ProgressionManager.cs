using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public enum ProgressStep { Unknown, WateredTree, CollectedTwigs, GrownTree, UnlockedRoots, UnlockedUpgrades, GrownTree2, GrownTree3, DismissedTitle }

[DefaultExecutionOrder(-1000)]
public class ProgressionManager : MonoBehaviour
{
    private static ProgressionManager _instance;
    private static System.Action _onStepComplete;

    [SerializeField] private bool _hasWateredTree;
    [SerializeField] private bool _hasCollectedTwigs;
    [SerializeField] private bool _hasGrownTree;
    [SerializeField] private bool _hasGrownTree2;
    [SerializeField] private bool _hasGrownTree3;
    [SerializeField] private bool _hasUnlockedRoots;
    [SerializeField] private bool _hasUnlockedUpgrades;
    [SerializeField] private bool _hasDismissedTitle;
    
    void Awake() => _instance = this;

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
            case (ProgressStep.GrownTree2):
                return _instance._hasGrownTree2;
            case (ProgressStep.GrownTree3):
                return _instance._hasGrownTree3;
            case (ProgressStep.UnlockedRoots):
                return _instance._hasUnlockedRoots;
            case (ProgressStep.UnlockedUpgrades):
                return _instance._hasUnlockedUpgrades;
            case (ProgressStep.DismissedTitle):
                return _instance._hasDismissedTitle;
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
            case (ProgressStep.GrownTree2):
                _instance._hasGrownTree2 = true;
                break;
            case (ProgressStep.GrownTree3):
                _instance._hasGrownTree3 = true;
                break;
            case (ProgressStep.UnlockedRoots):
                _instance._hasUnlockedRoots = true;
                break;
            case (ProgressStep.UnlockedUpgrades):
                _instance._hasUnlockedUpgrades = true;
                break;
            case (ProgressStep.DismissedTitle):
                _instance._hasDismissedTitle = true;
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
    
    public static void Unsubscribe(System.Action act)
    {
        _onStepComplete -= act;
    }

    [Button]
    private void NotifySubscribers()
    {
        _onStepComplete?.Invoke();
    }
}
