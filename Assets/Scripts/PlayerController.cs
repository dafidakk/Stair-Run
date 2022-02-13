using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 1.5f;
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
    private bool isMouseDown = false;
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
        StartCoroutine(BackToFront());
    }
    void Update()
    {
        TabToStart();
        
        lasPos = playersBack.transform.localPosition;
        frontPos = playersFront.transform.position;
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
            //transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, speed), Time.deltaTime*2);
            transform.position = transform.forward * speed * Time.deltaTime;
            //rb.velocity += new Vector3(0, 0, speed);
            animator.SetBool("started", true);
             
            if (Input.GetMouseButton(0))
            {

                isMouseDown = true;
                rb.useGravity = false;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, speed, speed), Time.deltaTime*2);
                BackToFront();
                
            }

            else 
            {
                
                rb.useGravity = true;
                isMouseDown = false;
                frontBrick = null;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, -speed*2, speed), Time.deltaTime * 2);

            } 
            
        }
    }

    void TabToStart()
    {
        if (Input.GetMouseButtonDown(0))
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
            //other.transform.localScale = brickPrefab.transform.localScale;
 
            bricks[brickIndex]= other.gameObject;
            brickIndex++;
            //brickSpace++;
            Debug.Log(brickIndex);
     
        }
    }

    void turn15Degree()
    {

        transform.localRotation *= Quaternion.Euler(0, -15, 0);

    }

    IEnumerator BackToFront()
    {


        while (true)
        {
            //foreach (GameObject item in bricks)
            //{
            if (isMouseDown)
            { 
                if (frontBrick == null)
                {

                    if (brickIndex > 0)
                    {
                        frontBrick = Instantiate(brickPrefab, frontPos + new Vector3(0, brickPrefab.transform.localScale.y*1.2f , brickPrefab.transform.localScale.z/1.5f), Quaternion.AngleAxis(90, Vector3.up));
                        //frontBrick.transform.parent = playersFront.transform;
                    
                        brickIndex--;
                        Destroy(bricks[brickIndex]);
                    }
                    else
                    {
                        rb.useGravity = true;
                    }
                }
                else
                {
                    if (brickIndex > 0)
                    {
                        frontBrick = Instantiate(brickPrefab, frontBrick.transform.position + new Vector3(0, brickPrefab.transform.localScale.y*1.2f, brickPrefab.transform.localScale.z/1.5f), Quaternion.AngleAxis(90, Vector3.up));
                        //frontBrick.transform.parent = playersFront.transform;
                        brickIndex--;
                        Destroy(bricks[brickIndex]);
                    }
                }

            }

            yield return new WaitForSeconds(0.07f);

        } 
        //} 
    }
 
}
