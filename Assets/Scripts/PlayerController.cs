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
    float sizeY = 0.05f;
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
        lasPos = playersBack.transform.localPosition;
        frontPos = playersFront.transform.localPosition;
        //sizeZ = brick.transform.localScale.z;
        //sizeY = brick.transform.localScale.y;
        //Run();
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
            other.transform.parent = playersBack.transform;
            other.transform.localPosition = positionVector + new Vector3(0, sizeY*brickSpace, 0);
            other.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            other.transform.localScale = Vector3.Scale(new Vector3(0.5f, 0.1f, 1f), new Vector3(1f, 0.5f, 0.75f));
 
            bricks[brickIndex]= other.gameObject;
            brickIndex++;
            brickSpace++;

            //ChildGet();
            Debug.Log(brickIndex);
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
            }      }
        //Debug.Log(Children.Count);
    }


    void BackToFront()
    {

        //brick = Instantiate(brickPrefab, frontPos + new Vector3(0,0.2f,0.2f),Quaternion.AngleAxis(90, Vector3.up));
        //brickIndex--;
        //Debug.Log(brickIndex);

        //if (bricks == null)

        //    bricks = GameObject.FindGameObjectsWithTag("Bricks");

        //Vector3 aheadPos = new Vector3();

        //aheadPos.x = lasPos.x;
        //aheadPos.y = lasPos.y + offsetY;
        //aheadPos.z = lasPos.z + offsetZ;

        //GameObject respawn = Instantiate(brickPrefab, aheadPos, Quaternion.AngleAxis(90, Vector3.up));
        //respawn.transform.parent = player.transform;

        foreach (GameObject item in bricks)
        {

            if (frontBrick == null)
            {

                if (brickIndex > 0)
                {
                    frontBrick = Instantiate(brickPrefab, transform.position + new Vector3(0, 0.2f, 0.2f), Quaternion.AngleAxis(90, Vector3.up));
                    //frontBrick.transform.parent = playersFront.transform;
                    brickIndex--;
                    Destroy(bricks[brickIndex]);
                }
            }
            else
            {
                if (brickIndex > 0)
                {
                    frontBrick = Instantiate(brickPrefab, frontBrick.transform.position + new Vector3(0, 0.2f, 0.2f), Quaternion.AngleAxis(90, Vector3.up));
                    //frontBrick.transform.parent = playersFront.transform;
                    brickIndex--;
                    Destroy(bricks[brickIndex]);
                }
            }
                 
                //Vector3 positionVector = new Vector3();
                //positionVector = frontPos;

                //item.transform.parent = playersFront.transform;
                //item.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y*sizeY, transform.localPosition.z*sizeZ);
                //item.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                //item.transform.localScale = Vector3.Scale(new Vector3(0.5f, 0.1f, 1f), new Vector3(2f, 2f, 2f));
                //brickIndex--;
                //Debug.Log(brickIndex);
                ////Destroy(bricks[brickIndex]);

            
        }
        
            

    }

    private void Run()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, 1.5f), Time.deltaTime * 2);
    }

    public List<GameObject> ReplaceBackBrick()
    {
        Vector3 positionVector = new Vector3();
        positionVector = lasPos;
        Temp[0].transform.position = positionVector;
        positionVector.z = lasPos.z + 1;
        Temp[1].transform.position = positionVector;
        positionVector.z = lasPos.z + 2;
        Temp[2].transform.position = positionVector;
        positionVector.y = lasPos.y + 1;
        Temp[3].transform.position = positionVector;
        positionVector.y = lasPos.y + 1;
        positionVector.z = lasPos.z + 1;
        Temp[4].transform.position = positionVector;
        positionVector.y = lasPos.y + 1;
        positionVector.z = lasPos.z + 2;
        Temp[5].transform.position = positionVector;
        positionVector.y = lasPos.y + 2; 
        Temp[6].transform.position = positionVector;
        positionVector.y = lasPos.y + 2;
        positionVector.z = lasPos.z + 1;
        Temp[7].transform.position = positionVector;
        positionVector.y = lasPos.y + 2;
        positionVector.z = lasPos.z + 2;
        Temp[8].transform.position = positionVector;
        return Temp;
    }

   
      
    
 
}
