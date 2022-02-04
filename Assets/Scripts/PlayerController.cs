using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject playersBack;
    [SerializeField] private GameObject playersFront;
    private GameObject brick;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Animator animator;

    private GameObject frontBrick;

    private GameObject[] bricks = new GameObject[9999];
    private int brickIndex = 0;
    private int brickSpace = 0;
    //Vector3 newVelocity = new Vector3();
    private bool started = false;
    Vector3 lasPos;
    Vector3 frontPos;
    float sizeZ;
    float sizeY;
    //private GameObject playerPrefab;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren <Animator>();
    }
    void Update()
    {
        TabToStart();
        
        lasPos = playersBack.transform.localPosition;
        frontPos = playersFront.transform.localPosition;
        sizeZ = brickPrefab.transform.localScale.z;
        sizeY = brickPrefab.transform.localScale.y;
        for (int i = 0; i < brickIndex; i++)
        {
            bricks[i].transform.position = playersBack.transform.position + new Vector3(0, sizeY * i, 0);
        }
        

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
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, 2f), Time.deltaTime);
            //rb.velocity += new Vector3(0, 0, speed);
            animator.SetBool("started", true);




            if (Input.GetMouseButton(0))
            {
                rb.useGravity = false;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 2.5f, 3f), Time.deltaTime);
                BackToFront();
                animator.SetBool("jump", true);
                animator.SetBool("started", true);

            }
            else
            {
                animator.SetBool("jump", true);
                frontBrick = null;
                rb.useGravity = true;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, -speed, speed), Time.deltaTime );
                animator.SetBool("started", false);
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
            
            Vector3 positionVector = new Vector3();
            positionVector = lasPos;
            sizeY = other.transform.localScale.y;
            other.transform.parent = playersBack.transform;
            other.transform.localPosition = positionVector + new Vector3(0, sizeY, 0);
            other.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            other.transform.localScale = brickPrefab.transform.localScale;
 
            bricks[brickIndex]= other.gameObject;
            brickIndex++;
            //brickSpace++;
            Debug.Log(brickIndex);
     
        }
    }

    void BackToFront()
    {

        foreach (GameObject item in bricks)
        {

            if (frontBrick == null)
            {

                if (brickIndex > 0)
                {
                    frontBrick = Instantiate(brickPrefab, transform.position + new Vector3(0, brickPrefab.transform.localScale.y, brickPrefab.transform.localScale.z/2), Quaternion.AngleAxis(90, Vector3.up));
                    //frontBrick.transform.parent = playersFront.transform;
                    
                    brickIndex--;
                    Destroy(bricks[brickIndex]);
                }
            }
            else
            {
                if (brickIndex > 0)
                {
                    frontBrick = Instantiate(brickPrefab, frontBrick.transform.position + new Vector3(0, brickPrefab.transform.localScale.y, brickPrefab.transform.localScale.z/2), Quaternion.AngleAxis(90, Vector3.up));
                    //frontBrick.transform.parent = playersFront.transform;
                    brickIndex--;
                    Destroy(bricks[brickIndex]);
                }
            }
            
        }
        
    }
 
}
