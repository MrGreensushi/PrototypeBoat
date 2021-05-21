using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AggiornatoCooldownAttacco : MonoBehaviour
{

    public Image[] immaginesingola;
    private  Image cooldownDestro,cooldownSinistro;
    private Image prontoDestro,prontoSinistro;
    private Canvas canv;
    private Sparare cannoneDestro,cannoneSinistro;

    void Start()
    {

        canv = FindObjectOfType<Canvas>();
       
        //instantiate right images
        cooldownDestro = Instantiate(immaginesingola[0], canv.transform);
        prontoDestro= Instantiate(immaginesingola[1], canv.transform);
        cooldownDestro.rectTransform.localPosition = new Vector3(100 , -175);
        prontoDestro.rectTransform.localPosition = new Vector3(100, -175);
        
        //instantiate left images
        cooldownSinistro = Instantiate(immaginesingola[0], canv.transform);
        prontoSinistro = Instantiate(immaginesingola[1], canv.transform);
        cooldownSinistro.rectTransform.localPosition = new Vector3(-100, -175);
        prontoSinistro.rectTransform.localPosition = new Vector3(-100, -175);

        //flip images
        cooldownSinistro.rectTransform.localScale = new Vector3(-1, 1, 1);
        prontoSinistro.rectTransform.localScale = new Vector3(-1, 1, 1);


        //set cooldown to false
        cooldownDestro.enabled = false;
        cooldownSinistro.enabled = false;
    }


    public void iniziaContare(bool destra)
    {
        if(destra)
            StartCoroutine(ConteggioDestra());
        else
            StartCoroutine(ConteggioSinistra());

    }

    IEnumerator ConteggioDestra()
    {

        cooldownDestro.enabled = true;
        prontoDestro.enabled = false;
        yield return new WaitForSeconds(cannoneDestro.tempoRicarica);
        prontoDestro.enabled = true;
        cooldownDestro.enabled = false;

    }
    IEnumerator ConteggioSinistra()
    {

        cooldownSinistro.enabled = true;
        prontoSinistro.enabled = false;
        yield return new WaitForSeconds(cannoneSinistro.tempoRicarica);
        prontoSinistro.enabled = true;
        cooldownSinistro.enabled = false;

    }

    public void setCannoni(Sparare destro, Sparare sinistro)
    {
        cannoneDestro = destro;
        cannoneSinistro = sinistro;
    }
}
