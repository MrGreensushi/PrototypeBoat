using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCasuale : MonoBehaviour
{
    public List<GameObject> daSpawnare;
  
    void Start()
    {
        int casuale = Random.Range(0, daSpawnare.Count);
        GameObject spawnato=Instantiate(daSpawnare[casuale],this.transform);
        this.transform.DetachChildren();
    }
}
