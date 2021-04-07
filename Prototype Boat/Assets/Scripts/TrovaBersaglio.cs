using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrovaBersaglio : MonoBehaviour
{
    public float raggioesplosione;
    //public SphereCollider c2d;
    public Vector3 pos_bersaglio;
    public float tempo_prima_di_sparare;
    public GameObject pro;
    private bool sparo;
    private GameObject bersaglio;
    private GameObject esplosione;
    public GameObject reticolo;
    public float ricarica;
    private GameObject ret;

    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Movimentonuovo>() != null)
        {
            bersaglio = collision.gameObject;
            sparo = true;
            StartCoroutine(SparoRitardato());
        }
    }

    protected void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Movimentonuovo>() != null)
            sparo = false;
    }

    protected IEnumerator SparoRitardato()
    {
        while (sparo)
        {
            pos_bersaglio = bersaglio.transform.position;
            ret=Instantiate(reticolo, pos_bersaglio, Quaternion.identity);
            yield return new WaitForSeconds(tempo_prima_di_sparare);
            esplosione= Instantiate(pro, pos_bersaglio+ new Vector3(0,1,0), Quaternion.identity);
            esplosione.GetComponent<Onda>().distanzaprimadimorire = raggioesplosione;
            yield return new WaitForSeconds(ricarica);
            Destroy(ret);
        }
        if (ret != null)
            Destroy(ret);
    }
}
