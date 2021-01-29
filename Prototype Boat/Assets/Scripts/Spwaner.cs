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
    private Vector3 puntoPiuVicino;
    private PolygonCollider2D pg;
    private bool puoiSpawnare;
    protected float min_x, min_y, max_x, max_y;

    private void Start()
    {
        puoiSpawnare = false;
        pg = GetComponent<PolygonCollider2D>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        MineMaxXFromCollider();
        MineMaxYfromCollider();
    }
    private void Update()
    {
        puntoPiuVicino = pg.ClosestPoint(mainCamera.transform.position);
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
            yield return new WaitForSeconds(tempoDiSpawn);
        }
    }

    protected Vector3 DoveSpawnare()
    {
        float x, y;
        Vector3 punto = new Vector3();
        Vector3 controllo = new Vector3();
        int n = 0; ;
        while (n<1000)
        {
            x = Random.Range(transform.position.x+ min_x, transform.position.x + max_x);
            y = Random.Range(transform.position.y + min_y, transform.position.y + max_y);
            punto = new Vector2(x, y);
            
            if (pg.OverlapPoint(punto))
            {
                
                controllo = mainCamera.WorldToViewportPoint(punto);
                
                if (controllo.x>0 && controllo.x<1 && controllo.y>0 && controllo.y<1)
                {
                
                    return punto;

                }
            }
            n++;
        }
        Debug.Log("Ho riportato  il più vicino");
        return puntoPiuVicino;

    }


    private void MineMaxXFromCollider()
    {
        float attuale;
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
        max_x *= transform.localScale.x;

    }

    private void MineMaxYfromCollider()
    {
        float attuale;
        max_y = -1000000;
        min_y = 10000000;
        for (int i = 0; i < pg.points.Length; i++)
        {
            attuale = pg.points[i].y;
            if (attuale < min_y)
            {
                min_y = attuale;
            }
            else if (attuale > max_y)
            {
                max_y = attuale;
            }
        }

        min_y *= transform.localScale.y;
        max_y *= transform.localScale.y;


    }

}
