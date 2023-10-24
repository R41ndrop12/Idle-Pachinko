using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuManager : MonoBehaviour
{
    public GameObject[] gm;
    public GameObject[] gms;

    private void Start()
    {
        
    }

    public void Switch()
    {
        for (int i = 0; i < gms.Length; i++)
        {
            gms[i].SetActive(true);
        }
        for (int i = 0; i < gm.Length; i++)
        {
            if (gm != null)
                gm[i].SetActive(false);
        }
    }


}
