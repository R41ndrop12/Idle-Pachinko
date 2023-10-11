using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkManager : MonoBehaviour
{

    private GameObject child;
    public Animator anim;
    private bool triggerAnim = true;
    // Start is called before the first frame update
    void Start()
    {
        child = this.gameObject.transform.GetChild(0).gameObject;
        anim = child.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggerAnim)
        {
            anim.SetTrigger("PlinkHit");
            StartCoroutine("animTimer");
        }
    }

    IEnumerator animTimer()
    {
        triggerAnim = false;
        yield return new WaitForSeconds(1f);
        triggerAnim = true;
    }
}
