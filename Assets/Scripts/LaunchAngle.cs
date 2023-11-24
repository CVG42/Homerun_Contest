using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAngle : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private bool isRotating = true;
    public GameObject magnusBar;

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
        }
    }

    void GetAngle()
    {
        float angle = Mathf.Sin(Time.time) * 52.5f - 47.5f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
