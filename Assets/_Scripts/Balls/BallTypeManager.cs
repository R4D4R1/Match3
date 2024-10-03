using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BallTypeConfig
{
    public BallType Type;
    public Color Color;
}

[CreateAssetMenu(fileName = "BallTypeManager", menuName = "Match3/BallTypeManager")]
public class BallTypeManager : ScriptableObject
{
    public List<BallTypeConfig> BallConfigs;

    public BallTypeConfig GetRandomBallConfig()
    {
        if (BallConfigs == null || BallConfigs.Count == 0) return null;
        return BallConfigs[Random.Range(0, BallConfigs.Count)];
    }
}
