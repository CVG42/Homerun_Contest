using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAngle : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private bool isRotating = true;
    public GameObject magnusBar;
    public GameObject leftMagnusBar;

    void Start()
    {
  
    }

    void Update()
    {
        if (isRotating)
        {
            GetAngle();
        }

        if (Input.GetMouseButtonDown(0))
        {
          
            isRotating = false;
            magnusBar.SetActive(true);
            leftMagnusBar.SetActive(true);
        }
    }

    void GetAngle()
    {
        float angle = Mathf.Sin(Time.time) * 22.5f - 67.5f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
