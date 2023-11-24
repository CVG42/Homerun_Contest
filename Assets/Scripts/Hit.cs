using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Rigidbody rb;
    public Transform launchDirection;
    public GameObject arrow;
    public MagnusController barController;

    //[SerializeField] private Vector3 velocity = Vector3.zero;
    //[SerializeField] private Vector3 angularVelocity = Vector3.zero;

    public int clicks = 0;
    public float force = 0f;
    public bool isTouchingGround = false;
    private bool canClick = true;
    private bool didOnce = false;
    //Vector3 lateralDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = velocity;
        //rb.angularVelocity = angularVelocity;
    }

    void Update()
    {
        if(!didOnce)
        {
            didOnce = true;
            Invoke("Launch", 5f);
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

        if(!barController.magnusBarOn)
        {
            //LaunchBall();
            rb.AddForce(launchDirection.up * (force * clicks) * Time.deltaTime);

            
            if(isTouchingGround)
            {
                Invoke("StopOnGround", 5f);
            }

            if(transform.position.y >= 10f)
            {
                rb.AddForce(Vector3.down * Physics.gravity.magnitude, ForceMode.Acceleration);
            }
        }    
    }
    /*
    private void FixedUpdate()
    {
        Magnus();
    }*/

    void Launch()
    {
        arrow.SetActive(true);
        //rb.AddForce(launchDirection.up * (force * clicks));
        canClick = false;
    }

    void StopOnGround()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    /*

    void LaunchBall()
    {
        Vector3 shoot = ((launchDirection.position) - this.transform.position).normalized;
        rb.angularDrag = 1;
        rb.AddForce(shoot * ((force * clicks)/100) + new Vector3(lateralDirection.x, 3f, lateralDirection.z), ForceMode.Impulse);
    }*/

    /*
    public Vector3 CalculateMagnusForce()
    {
        return barController.currentCoefficient * Vector3.Cross(rb.angularVelocity, rb.velocity);
    }*/

    /*
    void Magnus()
    {
        Vector3 velocity = rb.velocity;
        Vector3 lateralVelocity = new Vector3(velocity.x, 0, velocity.z);
        float speed = lateralVelocity.magnitude;
        lateralDirection = lateralVelocity.normalized;
        Vector3 magnusEffect = Vector3.Cross(lateralDirection * .6f, Physics.gravity.normalized) * barController.currentCoefficient * speed;

        rb.AddForce(magnusEffect, ForceMode.Force);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        isTouchingGround = true;
    }
}
