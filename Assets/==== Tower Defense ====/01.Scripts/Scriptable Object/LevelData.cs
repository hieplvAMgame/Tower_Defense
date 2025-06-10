using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Level Data")]
public class LevelData : ScriptableObject
{
    public List<List<BigWave>> levelData = new List<List<BigWave>>();
}
