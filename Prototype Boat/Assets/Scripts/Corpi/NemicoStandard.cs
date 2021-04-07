using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemicoStandard : Corpo
{
    private ParticleSystem ps;

    protected override void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    protected override void ComeSubiscoDanni(int danni)
    {
        vita -= danni;
        ps.Play();
    }

}
