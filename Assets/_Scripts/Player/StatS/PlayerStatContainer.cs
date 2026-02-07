using System.Collections.Generic;
using UnityEngine;

public class PlayerStatContainer : MonoBehaviour
{
    public List<Stat> statValue;

    void Awake()
    {
        // load data
        // initialize stats

        foreach (var stat in statValue)
        {
            stat.calculateValue();
        }
    }

    [ContextMenu("TestStat")]
    public void TestAddPoint()
    {
        statValue[(int)StatType.strength].AddPointValue(50);
    }
}
