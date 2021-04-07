using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arpia : Corpo
{
    public GameObject bersaglio;
    public float velocita;
    private MeshCollider bx;
    public float tempoPrimaDiDistacco;
    public float tempoBloccoVele;
    private int temp;
    private bool sonoSullaNAve=false;
    private Vector3 punto;
    private SphereCollider c2d;

    protected override void Start()
    {
        c2d = GetComponent<SphereCollider>();
        bx = GetComponent<MeshCollider>();
    }
    protected override void ComeSubiscoDanni(int danni)
    {
       vita-=danni;
    }

    private void FixedUpdate()
    {
        if (bersaglio != null)
        {
            if (!sonoSullaNAve)
                ComemiMuovo();
            else
            {
                transform.position = bersaglio.transform.position + punto;
            }
        }
    }
    protected void ComemiMuovo()
    {
       // .MovePosition(Vector2.MoveTowards(transform.position,bersaglio.transform.position, velocita*Time.deltaTime));
        transform.position = Vector3.MoveTowards(transform.position, bersaglio.transform.position, velocita * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (bersaglio == null)
        {
            if (collision.GetComponent<Movimentonuovo>() != null)
                bersaglio = collision.gameObject;

        }
        else if(collision.gameObject==bersaglio && !sonoSullaNAve)
        {
            punto = (transform.position - bersaglio.transform.position);
            bersaglio.GetComponent<Corpo>().SubiscoDanni(dannicorpoacorpo);
            bersaglio.GetComponent<Movimentonuovo>().BloccoAlleVelePerXSecondi(tempoBloccoVele); 
            StartCoroutine(StosullaNabve());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject== bersaglio)
        {
            if (!sonoSullaNAve)
            {
                if((bersaglio.transform.position-this.transform.position).magnitude>c2d.radius)
                    bersaglio = null;

            }
                
        }
    }
   

    IEnumerator StosullaNabve()
    {
        sonoSullaNAve = true;
        temp = dannicorpoacorpo;
        dannicorpoacorpo = 0;
        possosubire_danni = false;
        yield return new WaitForSeconds(tempoPrimaDiDistacco);
        sonoSullaNAve = false;
        transform.position = transform.position + new Vector3(Random.value, Random.value,0).normalized * 5;
        possosubire_danni = true;
        dannicorpoacorpo = temp;
    }
}
