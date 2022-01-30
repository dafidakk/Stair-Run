using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject playersBack;
    [SerializeField] private GameObject playersFront;
    [SerializeField] private GameObject brick;

    public List<GameObject> Children;

    private GameObject[] bricks;
    //Vector3 newVelocity = new Vector3();
    private bool started = false;
    Vector3 lasPos;
    Vector3 frontPos;
    float sizeZ;
    float sizeY;
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
        lasPos = playersBack.transform.position;
        frontPos = playersFront.transform.position;
        sizeZ = brick.transform.localScale.z;
        sizeY = brick.transform.localScale.y;
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
                BackToFront();
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
            other.transform.position = lasPos;
            other.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            other.transform.localScale = Vector3.Scale(new Vector3(0.5f, 0.1f, 1f), new Vector3(1f, 0.5f, 0.75f));
            other.transform.parent = playersBack.transform;
            ChildGet();

            //Destroy(other.gameObject);
            //FindObjectOfType<BrickSpawner>().BrickOnPlayer();
        }
    }

    void ChildGet()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform item in child)
            {
                if (item.tag == "Bricks")
                {
                    Children.Add(item.gameObject);
                }
            }
            
        }
        //Debug.Log(Children.Count);

    }

    void BackToFront()
    {
        foreach (GameObject item in Children)
        {
            if (Children.Count != 0)
            {
                
              item.transform.parent = playersFront.transform;
              item.transform.position = frontPos;
                           
            }
            
        }
    }
 
}
