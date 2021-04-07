using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMarina : MonoBehaviour
{
    public float tempoSpawnaBarili;
    private NavMeshAgent agent;
    public Vector3[] doveMiMuovo;
    public int indicePunto;
    private float distanzaMinima;
    public float tempoSpawnOdne;
    public GameObject onda;
    public Vector3[] cannoni;
    public int[] dirCannoni;
    private bool spawnaonde;
    private Vector3[] pos ;
    private Quaternion[] finalDir;
    private bool spawnaBarili;
    public GameObject barile;
    void Start()
    {
        distanzaMinima = 20;
        indicePunto = 0;
        agent = GetComponent<NavMeshAgent>();
        spawnaonde = true;
        pos = new Vector3[cannoni.Length];
        finalDir = new Quaternion[dirCannoni.Length];
        StartCoroutine(SpwanaOnde());
        spawnaBarili = true;
        //StartCoroutine(SpawnaBarili());
        agent.SetDestination(doveMiMuovo[indicePunto]);
    }

    private void Update()
    {
        VadoVersoPuntoSuccessivo();
    }

    void VadoVersoPuntoSuccessivo()
    {
       
        
        if (Mathf.Abs(Vector3.Distance(transform.position, doveMiMuovo[indicePunto])) < distanzaMinima)
        {
            Debug.Log("sto per cambiare");
            indicePunto++;
            if (indicePunto >= doveMiMuovo.Length)
            {
                indicePunto = 0;
            }
            agent.SetDestination(doveMiMuovo[indicePunto]);
            Debug.Log("Destinazione cambiata");
        }
    }

    IEnumerator SpwanaOnde()
    {
    
        while (spawnaonde) 
        {
            yield return new WaitForSeconds(tempoSpawnOdne);
            Debug.Log("Aggiorno posizione");
            AggiornaPosizione();
            for (int i = 0; i < cannoni.Length; i++)
            {
                Debug.Log("Sto per spawnare onda");
             
                if (onda != null)
                {
                    Debug.Log("Onda");

                    Instantiate(onda, pos[i], finalDir[i]);

                }
                else
                {
                    Debug.Log("Nessuna onda");
                }
                //spwanata.SetDirezione(dirCannoni[i]);
            }
        }
    }

    void AggiornaPosizione()
    {
        for (int i = 0; i < cannoni.Length; i++)
        {
            pos[i] = transform.position + transform.right * cannoni[i].x + transform.forward * cannoni[i].z;
            /*finalDir[i] = Quaternion.Euler(0,0, transform.rotation.eulerAngles.x + dirCannoni[i]);
            if (dirCannoni[i] == 0)
            {
              
            }*/
            finalDir[i] = Quaternion.LookRotation(transform.right*dirCannoni[i], transform.up);
            if (dirCannoni[i] == 0)
            {
                finalDir[i] = Quaternion.LookRotation(-transform.forward, transform.up);
            }
        }
    }

    IEnumerator SpawnaBarili()
    {
        while (spawnaBarili)
        {
            yield return new WaitForSeconds(tempoSpawnaBarili);
            Debug.Log("BARILEE");
            Instantiate(barile, transform.position - transform.forward * transform.localScale.z / 1.8f, Quaternion.identity);
        }
    }
}
