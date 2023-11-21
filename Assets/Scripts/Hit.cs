using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private Rigidbody rb;
    //public Transform launchDirection;
    public GameObject arrow;
    [SerializeField] private int clicks = 0;
    public float force = 0f;
    private bool canClick = true;
    private bool didOnce = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!didOnce)
        {
            didOnce = true;
            Invoke("Launch", 10f);
        }

        if(canClick && Input.GetKeyDown(KeyCode.Space))
        {
            clicks++;
            force += 2;

            if (Mathf.Approximately(force % 20f, 0f)) //Incrementa cada 20
            {
                force += 10f; 
            }
        }
    }

    void Launch()
    {
        arrow.SetActive(true);
        //rb.AddForce(launchDirection.up * (force * clicks));
        canClick = false;
    }
}
