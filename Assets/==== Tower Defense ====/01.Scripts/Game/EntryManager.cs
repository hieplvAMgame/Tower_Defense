using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntryManager : MonoBehaviour
{
    [SerializeField] List<WaveEntry> entries = new();
    public void Setup(BigWave bw)
    {
        for (int i = 0; i < entries.Count; i++)
        {
            var entry = entries[i];
            var wave = bw.waves.FirstOrDefault(x => x.idWaveEntry == entry.id);
            if (wave != null)
                entry.Setup(wave);
            else
                Debug.Log($"Data not have wave data with entry id {entry.id}!");
        }
    }
    public void SpawnWave()
    {
        entries.ForEach(x=>x.SpawnWave());
    }
}
