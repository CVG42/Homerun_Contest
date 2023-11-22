using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Rigidbody rb;
    public Transform launchDirection;
    public GameObject arrow;
    public MagnusController barController;

    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private Vector3 angularVelocity = Vector3.zero;

    public int clicks = 0;
    public float force = 0f;
    private bool canClick = true;
    private bool didOnce = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
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

    private void FixedUpdate()
    {
        if(!barController.magnusBarOn)
        {
            LaunchBall();
        }    
    }

    void Launch()
    {
        arrow.SetActive(true);
        //rb.AddForce(launchDirection.up * (force * clicks));
        canClick = false;
    }

    void LaunchBall()
    {
        rb.AddForce(CalculateMagnusForce() + launchDirection.up * (force * clicks));
    }

    public Vector3 CalculateMagnusForce()
    {
        return barController.currentCoefficient * Vector3.Cross(rb.angularVelocity, rb.velocity);
    }
}
