using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public GameObject toSpawn;
    public Vector3 direzione;
    public float tempoDiSpawn;
    private Camera mainCamera;
    protected Vector3 vicinanza;
    public Vector3 puntoPiuVicino;
    private MeshCollider pg;
    private bool puoiSpawnare;
    protected float min_x, min_z, max_x, max_z;
    private GameObject giocatore;

    private void Start()
    {
        puoiSpawnare = false;
        pg = GetComponent<MeshCollider>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        giocatore = GameObject.FindObjectOfType<Movimentonuovo>().gameObject;
        MineMaxXFromCollider();
        MineMaxYfromCollider();
        Debug.Log("Max x:" + max_x+" Max z: " +max_z+ " Min x " +min_x + " min z: " +min_z);
    }
    private void Update()
    {
        puntoPiuVicino = pg.ClosestPoint(giocatore.transform.position);
        vicinanza = mainCamera.WorldToViewportPoint(puntoPiuVicino);
       
        if (vicinanza.x > 0 && vicinanza.y > 0 && vicinanza.x < 1 && vicinanza.y < 1)
        {
            if (!puoiSpawnare)
            {
               
                puoiSpawnare = true;
                StartCoroutine(Spawna());
            }

        }
        else
        {
            puoiSpawnare = false;
        }

    }

    protected IEnumerator Spawna()
    {
        while (puoiSpawnare)
        {
            Instantiate(toSpawn, DoveSpawnare(), Quaternion.Euler(direzione));
            yield return new WaitForSeconds(tempoDiSpawn+ Random.Range(-0.5f,0.5f));
        }
    }

    protected Vector3 DoveSpawnare()
    {
        float x, z;
        Vector3 punto = new Vector3();
        Vector3 controllo = new Vector3();
        int n = 0; ;
        while (n<1000)
        {
            x = Random.Range( min_x, max_x);
            z = Random.Range( min_z, max_z);
            punto = new Vector3(x,0,z);
            

            if (pg.bounds.Contains(punto))
            {


                controllo = mainCamera.WorldToViewportPoint(punto);

                if (controllo.x > 0 && controllo.x < 1 && controllo.y > 0 && controllo.y < 1)
                {
                    Debug.Log(punto);
                    return punto;

                }
            }
            /*if (pg.OverlapPoint(punto))
            {
                
                controllo = mainCamera.WorldToViewportPoint(punto);
                
                if (controllo.x>0 && controllo.x<1 && controllo.y>0 && controllo.y<1)
                {
                
                    return punto;

                }
            }*/
            n++;
        }
        Debug.Log("Ho riportato  il più vicino");
        return puntoPiuVicino;

    }


    private void MineMaxXFromCollider()
    {
        max_x = pg.bounds.max.x;
        min_x = pg.bounds.min.x;
        
       /* float attuale;
        max_x = -1000000;
        min_x = 10000000;
        for (int i = 0; i < pg.points.Length; i++)
        {
            attuale = pg.points[i].x;
            if (attuale < min_x)
            {
                min_x = attuale;
            }
            else if (attuale > max_x)
            {
                max_x = attuale;
            }
        }

        min_x *= transform.localScale.x;
        max_x *= transform.localScale.x;*/

    }

    private void MineMaxYfromCollider()
    {
        max_z = pg.bounds.max.z;
        min_z = pg.bounds.min.z;
        /* float attuale;
         max_z = -1000000;
         min_z = 10000000;
         for (int i = 0; i < pg.points.Length; i++)
         {
             attuale = pg.points[i].y;
             if (attuale < min_z)
             {
                 min_z = attuale;
             }
             else if (attuale > max_z)
             {
                 max_z = attuale;
             }
         }

         min_z *= transform.localScale.y;
         max_z *= transform.localScale.y;

         */
    }
   
}
