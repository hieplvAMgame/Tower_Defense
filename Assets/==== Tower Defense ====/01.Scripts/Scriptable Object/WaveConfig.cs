using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Wave Config")]
public class WaveConfig : ScriptableObject
{
    public List<Level> levelData = new List<Level>();
}
[System.Serializable]
public class Crowd
{
    public GameObject unitPrefabs;
    public int quantity;
}

[System.Serializable]
public class Wave
{
    public int idWaveEntry;
    public List<Crowd> crowds;
    public float intervalTime;
    public int TotalQuantity => crowds.Sum(x => x.quantity);
}
[System.Serializable]
public class BigWave
{
    public List<Wave> waves = new();
}
[System.Serializable]
public class Level
{
    public string levelName;
    public List<BigWave> bigWaves = new();
}
