using UnityEngine;

[CreateAssetMenu(fileName = "WaterLevels", menuName = "ScriptableObjects/WaterSystem")]
public class WaterLevels : ScriptableObject
{
    /// <summary>
    /// The size of the water meter, level by level
    /// </summary>
    public float[] WaterMeterAmounts;
}
