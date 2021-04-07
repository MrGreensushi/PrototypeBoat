using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   // public GameObject perDebug;
    private NavMeshAgent controler;
    private GameObject giocatore;
    private bool insegui;
    public float iniziaAttaccare;
    private Vector3[] punti = new Vector3[100];
    private int indicepunto;
    private bool primoGiro;
    bool nonStoAttaccando;
    private float distanzamin =2;
    private Sparare spara;

    private void Start()
    {
        nonStoAttaccando = false;
        primoGiro = false;
        controler = GetComponent<NavMeshAgent>();
        giocatore = GameObject.FindObjectOfType<Movimentonuovo>().gameObject;
        insegui = false;
        TrovaPrimoPunto();
        spara = GetComponent<Sparare>();
      /* foreach(Vector3 p in punti)
        {
            Instantiate(perDebug, p,Quaternion.identity); 
        }*/
    }

    private void Update()
    {
        if (insegui && nonStoAttaccando)
        {
            primoGiro = false;
            VaiVersoGiocatore();

        }
        if (Mathf.Abs((transform.position - giocatore.transform.position).magnitude) < iniziaAttaccare)
        {
            
            nonStoAttaccando = false;
            if (!primoGiro)
            {
                indicepunto = TrovaPrimoPunto();
                
            }
                
            primoGiro=true;
            GiraIntornoAlGiocatore();
        }
        else
            nonStoAttaccando = true;
       
    }
    public void VaiVersoGiocatore()
    {
        controler.SetDestination(giocatore.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == giocatore)
        {
            //inizia  inseguimento
            insegui = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == giocatore)
        {
            //finisci  inseguimento
            insegui = false;
        }
    }

    private void GiraIntornoAlGiocatore()
    {
        AggiornaPunti();
        controler.SetDestination(punti[indicepunto]);
        SparaADestraOSinistra();
        //aggiorno il punto solo quando ci arrivo
        if (Mathf.Abs(Vector3.Distance(punti[indicepunto], transform.position))<distanzamin )
        {
           
            indicepunto++;
            if (indicepunto >= 100)
                indicepunto = 0;

        }
            
        
    }
    private void AggiornaPunti()
    {
        for (int i = 0; i < 100; i++)
        {
            punti[i] = new Vector3(Mathf.Cos(i*Mathf.PI/50) * 5,0, Mathf.Sin(i * Mathf.PI / 50) * 5) + giocatore.transform.position;
        }
    }
    private int TrovaPrimoPunto()
    {
        
        float[] dis = new float[100];
        float min = 0;
        int indice = -1;
        bool trovato = false;
        AggiornaPunti();
        for (int i = 0; i < 100; i++)
        {            
            dis[i] = Mathf.Abs(Vector3.Distance(punti[i], transform.position));
        }
        min = Mathf.Min(dis);
     
        while (!trovato)
        {
            indice++;
            if (min == dis[indice])
            {
                trovato = true;
            }
        }
        //prendi il punto più  vicino che disti almeno un minimo senno il tipo non si muove che sia in senso orario
        trovato = false;
        int numero = indice;
      
        while (!trovato)
        {
            if (dis[numero] < distanzamin)
            {
                numero++;
                if (numero >= 100)
                    numero = 0;
            }
            else
            {
                trovato = true;
            }
               
        }

        return numero;
    }

    private void SparaADestraOSinistra()
    {
        RaycastHit[] hit = Physics.RaycastAll(transform.position , - this.transform.right,  10);
       
        for(int i=0; i < hit.Length; i++)
        {
            
            if (hit[i].collider.GetComponent<Movimentonuovo>() != null)
                spara.Spara(-this.transform.right, transform.position - this.transform.right * 0.8f);

        }
    }
    
   
}
