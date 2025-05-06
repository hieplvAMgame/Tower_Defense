using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector2 spacing;
    [Space, SerializeField] MapData mapData;
    List<GameObject> map = new List<GameObject>();
    GameObject go;
    [Button]
    public void GenMap(int width, int height)
    {
        if (map.Count != 0)
            map.ForEach(x => DestroyImmediate(x));
        map.Clear();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                go = Instantiate(tilePrefab, new Vector3(j, i, 0) + new Vector3(spacing.x * j, spacing.y * i), Quaternion.identity);
                go.name = $"Tile {j} - {i}";
                go.GetComponent<Tile>().SetPos(j, i);
                go.transform.SetParent(transform, false);
                map.Add(go);
            }
        }
    }
    Tile _tile;
    Dictionary<Vector2Int, bool> dict = new Dictionary<Vector2Int, bool>();
    [Button("Export Data")]
    public void ExportData()
    {
        dict.Clear();
        foreach (var tile in map)
        {
            _tile = tile.GetComponent<Tile>();
            if (!dict.ContainsKey(new Vector2Int(_tile.X, _tile.Y)))
                dict.Add(new Vector2Int(_tile.X, _tile.Y), _tile.IsBuilable);
        }
        mapData.SaveData(dict, spacing);
    }
}
