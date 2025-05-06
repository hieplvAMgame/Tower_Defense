using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config",menuName = "SO/New Config")]
public class MapData : ScriptableObject
{
    [SerializeField] int ID;
    [SerializeField] Vector2 spacing;
    [ShowInInspector]
    public Dictionary<Vector2Int,bool> data = new Dictionary<Vector2Int,bool>();
    public void SaveData(Dictionary<Vector2Int,bool> _data, Vector2 _spacing)
    {
        data.Clear();
        data = _data;
        spacing = _spacing;
    }
    [Button]
    public void SaveToPlayPrefs()
    {
        Debug.Log(JsonUtility.ToJson(this));
    }
}
