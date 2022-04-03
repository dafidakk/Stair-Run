using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] public float speed = 1.5f;
    [SerializeField] public float speedY = 1.5f;
    [SerializeField] public float speedZ = 1.5f;
    [SerializeField] private GameObject playersBack;
    [SerializeField] private GameObject playersFront; 
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject stairPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private bool _isSpline = false;
    private GameObject brick;
    private GameObject frontBrick;
    private bool gameOver;
    public SplineFollower _splineFollower; 
    public SplineComputer _splineComputer;
    public SplineProjector splineProjector;
    private List<GameObject> bricks;
    private int _brickSpace = 0;
    //Vector3 newVelocity = new Vector3();
    private bool started = false;
    private bool isMouseDown = false;
    Vector3 lasPos;
    Vector3 frontPos;
    public float followSpeed = 250f;
    float sizeZ;
    float sizeY;
    Rigidbody rb;
    private float eulerAngY; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        bricks = new List<GameObject>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren <Animator>();
        StartCoroutine(BackToFront());
        _splineFollower = GetComponent<SplineFollower>();
        _splineComputer = GetComponent<SplineComputer>();
        splineProjector = GetComponent<SplineProjector>();
        gameOver = false; 
    }
    void Update()
    {

        TabToStart();
        rb.useGravity = true;

        lasPos = playersBack.transform.localPosition;
        frontPos = playersFront.transform.position;
        sizeZ = brickPrefab.transform.localScale.z;
        sizeY = brickPrefab.transform.localScale.y;

        bricks = bricks.Where(x => x.gameObject != null).ToList();

        for (int i = 0; i < bricks.Count; i++)
        {
          
          
            if (bricks[i] == null)
            {
                Debug.Log("");

                bricks.Remove(bricks[i]);
              

            }
            bricks[i].transform.position = playersBack.transform.position + new Vector3(0, sizeY * i, 0);

        }

        //foreach (var item in bricks)
        //{
        //    item.transform.position = playersBack.transform.position + new Vector3(0, sizeY , 0);
        //}

        eulerAngY = transform.localEulerAngles.y;
        if (gameOver)
        {
            GameManager.instance.GameOver();
        }
        //int index;
        //double lerp;
        //_playerController._splineComputer.GetSamplingValues(_playerController._splineFollower.result.percent, out index, out lerp);
        //_playerController._splineFollower.GetSample(index, _playerController.sample);
        //Debug.Log($"Forward: {_playerController.sample.forward}");
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
            GameManager.instance.StartGame();
            animator.SetBool("started", true);
            _splineFollower.follow = true;
            _splineFollower.followSpeed = followSpeed * Time.fixedDeltaTime;

            //Debug.Log(eulerAngY);
            if (Input.GetMouseButton(0))
            {
                isMouseDown = true;
                rb.useGravity = false;

                if (bricks.Count > 0)
                {
                    transform.position += new Vector3(0, speedY * Time.deltaTime , 0);
                }
                else
                {
                    transform.position += new Vector3(0, -speedY * Time.deltaTime, 0);
                }
            }
            else 
            {
                if (FinishTrigger.instance.isFinish == true)
                {
                    
                    if (bricks.Count > 0)
                    {
                        isMouseDown = true;
                        transform.position += new Vector3(0, speedY * Time.deltaTime, 0);
                        if (LevelEnd.instance.levelEnd)
                        {
                            isMouseDown = false;
                            frontBrick = null;
                            gameOver = true;
                            started = false;
                            animator.SetBool("started", false);
                            rb.useGravity = true;
                            _splineFollower.follow = false;

                        }
                    }
                    else
                    {
                        transform.position += new Vector3(0, -speedY * Time.deltaTime, 0);
                    }
                }
                else
                {
                    rb.useGravity = true;
                    isMouseDown = false;
                    frontBrick = null;
                    transform.position += new Vector3(0, -speed * Time.deltaTime, speed * Time.deltaTime);
                }
                              
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
        _splineFollower.follow = false;
        //started = true;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bricks")
        {
            other.transform.parent = playersBack.transform;
            ReLocate(other); 
            bricks.Add(other.gameObject);
            //Debug.Log(bricks.Count); 
        }
    }

    private void ReLocate(Collider other)
    {
        var positionVector = lasPos;
        sizeY = other.transform.localScale.y;
        other.transform.localPosition = positionVector + new Vector3(0, sizeY * _brickSpace, 0);
        other.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        other.transform.localScale = new Vector3(0.5f, 0.125f, 0.6f);
        _brickSpace++;
    }

    void turn15Degree()
    {
        transform.localRotation *= Quaternion.Euler(0, -15, 0);
    }
    IEnumerator BackToFront()
    {
        while (true)
        {
            if (isMouseDown)
            { 
                if (frontBrick == null)
                {
                    if (bricks.Count > 0)
                    {
                        if (eulerAngY > 260f)
                        { 
                            frontBrick = Instantiate(stairPrefab, frontPos + new Vector3(stairPrefab.transform.localScale.z, stairPrefab.transform.localScale.y, 0), Quaternion.Euler(0,eulerAngY,0));
                        } 
                        else
                        {
                            frontBrick = Instantiate(stairPrefab, frontPos + new Vector3(0, stairPrefab.transform.localScale.y, stairPrefab.transform.localScale.z), Quaternion.Euler(0, eulerAngY, 0)); 
                        }  
                        var gameObject = bricks[bricks.Count-1];
                        bricks.Remove(gameObject);
                        Destroy(gameObject);
                    }
                    else
                    {
                        rb.useGravity = true;
                    }
                }
                else
                {
                    if (bricks.Count > 0)
                    {
                        if (eulerAngY > 260f)
                        {
                            frontBrick = Instantiate(stairPrefab, frontPos + new Vector3(stairPrefab.transform.localScale.z, stairPrefab.transform.localScale.y, 0), Quaternion.Euler(0, eulerAngY, 0));
                        }
                        else
                        {
                            frontBrick = Instantiate(stairPrefab, frontPos + new Vector3(0, stairPrefab.transform.localScale.y, stairPrefab.transform.localScale.z), Quaternion.Euler(0, eulerAngY, 0));
                        }  
                        var gameObject = bricks[bricks.Count-1];
                        bricks.Remove(gameObject);
                        Destroy(gameObject);
                    }
                    else
                    { 
                        isMouseDown = false;
                        frontBrick = null;
                        gameOver = true;
                        started = false;
                        animator.SetBool("started", false);
                        rb.useGravity = true;
                        _splineFollower.follow = false;
                    }
                }
            }
            yield return new WaitForSeconds(0.06f);
        } 
        //} 
    }
 
}
