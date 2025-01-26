using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hide;
    [SerializeField]
    private GameObject[] show;

    public void Switch()
    {
        for (int i = 0; i < show.Length; i++)
        {
            show[i].SetActive(true);
        }
        for (int i = 0; i < hide.Length; i++)
        {
            if (hide != null)
                hide[i].SetActive(false);
        }
    }


}
