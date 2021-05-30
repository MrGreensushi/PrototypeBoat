using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arpia : Corpo
{
    public GameObject bersaglio;
    public float velocita;
   
    public float tempoPrimaDiDistacco;
    public float tempoBloccoVele;
    private int temp;
    private bool sonoSullaNAve=false;
    private Vector3 punto;
    private SphereCollider c2d;
    private float puntoz, puntox;
    

    protected override void Start()
    {
        rb2d = GetComponent<Rigidbody>();
        c2d = GetComponent<SphereCollider>();
       
    }
    protected override void ComeSubiscoDanni(int danni)
    {
       vita-=danni;
    }

    private void FixedUpdate()
    {
        if (bersaglio != null)
        {
            GuardoBersaglio();
            if (!sonoSullaNAve)
                ComemiMuovo();
            else
            {

                transform.position = bersaglio.transform.position+ bersaglio.transform.forward*puntoz+bersaglio.transform.right*puntox;
            }
        }
    }
    protected void ComemiMuovo()
    {
        Vector3 nextStep = Vector3.MoveTowards(transform.position, bersaglio.transform.position, velocita * Time.deltaTime);
        // transform.position = new Vector3(nextStep.x, yposition(nextStep), nextStep.z);
        transform.position =nextStep;
    }

    protected void GuardoBersaglio()
    {

        // Determine which direction to rotate towards
        Vector3 targetDirection = bersaglio.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = velocita * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (bersaglio == null)
        {
            if (collision.GetComponent<Movimentonuovo>() != null)
                bersaglio = collision.gameObject;

        }
        /*else if(collision.gameObject==bersaglio && !sonoSullaNAve)
        {
           
            punto = (transform.position - bersaglio.transform.position);
            puntox = Vector3.Dot(punto, bersaglio.transform.right);
            puntoz = Vector3.Dot(punto, bersaglio.transform.forward);

            bersaglio.GetComponent<Corpo>().SubiscoDanni(dannicorpoacorpo);
            bersaglio.GetComponent<Movimentonuovo>().BloccoAlleVelePerXSecondi(tempoBloccoVele); 
            StartCoroutine(StosullaNabve());
        }*/
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

    override protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == bersaglio && !sonoSullaNAve)
        {

            punto = (transform.position - bersaglio.transform.position);
            puntox = Vector3.Dot(punto, bersaglio.transform.right);
            puntoz = Vector3.Dot(punto, bersaglio.transform.forward);

            bersaglio.GetComponent<Corpo>().SubiscoDanni(dannicorpoacorpo);
            bersaglio.GetComponent<Movimentonuovo>().BloccoAlleVelePerXSecondi(tempoBloccoVele);
            StartCoroutine(StosullaNabve());
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
        transform.position = transform.position - bersaglio.transform.forward * 5;
        possosubire_danni = true;
        dannicorpoacorpo = temp;
    }

    private Vector3 yposition( Vector3 nextstep)
    {
        /*float maxy = 0;
        Vector3 position = new Vector3(nextstep.x,40,nextstep.z);
        RaycastHit[] colpito;
        Debug.DrawRay(position, -Vector3.up*50, Color.red);
        colpito=Physics.RaycastAll(position, -Vector3.up*50);
        foreach(RaycastHit toCheck in colpito){
            if (toCheck.point.y > maxy && !toCheck.collider.isTrigger)
            {
                if (toCheck.collider.gameObject.name != "Arpia")
                {
                    maxy = toCheck.point.y;
                    Debug.Log(toCheck.collider.gameObject.name);
                }
                
            }
               
        }
        Debug.Log(maxy);*/
        RaycastHit[] colpito;
        Vector3 position =this.transform.position;
        Vector3 newNext = nextstep;
        //Debug.DrawRay(position, nextstep.normalized, Color.red);
        colpito = Physics.RaycastAll(position, nextstep);
         
        while (colpito.Length != 0)
        {
            newNext = newNext + Vector3.up * 0.1f;
            colpito = Physics.RaycastAll(position, nextstep);
        }

        return newNext;
    }
}
