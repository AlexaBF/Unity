﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100);

        }   
    }
}
