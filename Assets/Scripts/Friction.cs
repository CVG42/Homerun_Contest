using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    public PhysicMaterial physicMaterial;

    void Start()
    {
        physicMaterial = GetComponent<Collider>().material;
    }

    void Update()
    {
        
    }
}
