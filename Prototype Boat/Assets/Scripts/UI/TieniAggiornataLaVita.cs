using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TieniAggiornataLaVita : MonoBehaviour
{
    public Image immaginesingola;
    private Movimentonuovo giocatore;
    public Image[] vita;
    private Canvas canv;

    private void Start()
    {
        giocatore = FindObjectOfType<Movimentonuovo>();
        vita =new Image[giocatore.vita];
        canv = FindObjectOfType<Canvas>();
        for (int i = 0;i< giocatore.vita; i++)
        {
            vita[i] = Instantiate(immaginesingola, canv.transform);
            vita[i].rectTransform.localPosition = new Vector3(-380+i*30,200);
        }
    }
    
    public void AggiornaVita(int vitaTot)
    {
        for(int i = 0; i < vita.Length; i++)
        {
            if (vitaTot >= i+1)
            {
                vita[i].enabled = true ;
            }
            else
            {
                vita[i].enabled = false;
            }
        }
    }
}
