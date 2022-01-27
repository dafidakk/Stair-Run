using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    bool started;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update

    void Start()
    {
        started = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            Movement();
        }
        
    }

    void Movement()
    {
        //Vector3 newVelocity = new Vector3();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            rb.velocity += new Vector3(0f, 0f, speed*Time.deltaTime);
            //started = true;
            //Game will start by Game manager here..
        }
        
    }
}
