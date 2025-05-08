using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config",menuName = "SO/New Config")]
public class MapData : ScriptableObject
{
    public int ID;
    public float spacingX, spacingY;
    [ShowInInspector]
    public Dictionary<Vector2Int,int> data = new Dictionary<Vector2Int,int>();

    public MapData(int iD, float x,float y, Dictionary<Vector2Int, int> data)
    {
        ID = iD;
        this.data = data;
        spacingX = x;
        spacingY = y;
    }

    [Button]
    public void SaveToPlayPrefs()
    {
        Debug.Log(JsonConvert.SerializeObject(this));
    }
}
