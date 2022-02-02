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

    private GameObject frontBrick;

    public List<GameObject> Children;
    public List<GameObject> Temp;

    private GameObject[] bricks = new GameObject[9999];
    private int brickIndex = 0;
    private int brickSpace = 0;
    //Vector3 newVelocity = new Vector3();
    private bool started = false;
    Vector3 lasPos;
    Vector3 frontPos;
    float sizeZ = 0.05f;
    float sizeY;
    //private GameObject playerPrefab;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        TabToStart();
        lasPos = playersBack.transform.localPosition;
        frontPos = playersFront.transform.localPosition;
        //sizeZ = brick.transform.localScale.z;
        //sizeY = brick.transform.localScale.y;
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
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, speed), Time.deltaTime * 2);
            //rb.velocity += new Vector3(0, 0, speed);
            
            if (Input.GetMouseButton(0))
            {
                rb.useGravity = false;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, speed*2, 0), Time.deltaTime * 2);
                BackToFront();

            }
            else
            {
                frontBrick = null;
                rb.useGravity = true;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, -speed*2, speed), Time.deltaTime * 2);
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
            other.transform.localPosition = positionVector + new Vector3(0, sizeY*brickSpace, 0);
            other.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            other.transform.localScale = Vector3.Scale(new Vector3(0.5f, 0.1f, 1f), new Vector3(1f, 0.5f, 0.75f));
 
            bricks[brickIndex]= other.gameObject;
            brickIndex++;
            brickSpace++;
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
                    frontBrick = Instantiate(brickPrefab, transform.position + new Vector3(0, brickPrefab.transform.localScale.y, brickPrefab.transform.localScale.z), Quaternion.AngleAxis(90, Vector3.up));
                    //frontBrick.transform.parent = playersFront.transform;
                    brickIndex--;
                    Destroy(bricks[brickIndex]);
                }
            }
            else
            {
                if (brickIndex > 0)
                {
                    frontBrick = Instantiate(brickPrefab, frontBrick.transform.position + new Vector3(0, brickPrefab.transform.localScale.y, brickPrefab.transform.localScale.z), Quaternion.AngleAxis(90, Vector3.up));
                    //frontBrick.transform.parent = playersFront.transform;
                    brickIndex--;
                    Destroy(bricks[brickIndex]);
                }
            }
            
        }
        
    }
 
}
