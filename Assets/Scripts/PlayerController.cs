using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    //Vector3 newVelocity = new Vector3();
    private bool started = false;
    //private GameObject playerPrefab;

    Rigidbody rb;

    void Awake()
    {
        
    }
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TabToStart();
    }
    private void FixedUpdate()
    {
        if (started)
        {
            Movement();
        }
    }

    void Movement()
    { 
        if (started)
        {
            rb.velocity += new Vector3(0, 0, speed);
            
            if (Input.GetKey(KeyCode.Space))
            {
                FindObjectOfType<BrickSpawner>().BrickAheadPlayer();
                rb.velocity = new Vector3(0, speed, speed);
            }
            else
            {
                rb.velocity = new Vector3(0, -speed, speed);
            }
        }
    }

    void TabToStart()
    {
        if (Input.GetMouseButton(0))
        {
            started = true;
        }
    }

    public void HitTheObstacle()
    {
        started = false;   
    }

    public void AfterHitTheObstacle()
    {
        started = true;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bricks")
        {
            Destroy(other.gameObject);
            FindObjectOfType<BrickSpawner>().BrickOnPlayer();
        }
    }
 
}
