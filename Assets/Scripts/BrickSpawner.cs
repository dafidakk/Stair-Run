using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject[] bricks;
    [SerializeField] float offsetZ = 1f;
    [SerializeField] float offsetY = 1f;

    Vector3 lasPos;
    float size;
    // Start is called before the first frame update
    void Start()
    {  
        //size = bricks.transform.localScale.x;
    }
    // Update is called once per frame
    void Update()
    {
        lasPos = player.transform.position;
    }
    //public void BrickOnPlayer()
    //{
    //    if (bricks == null)

    //        bricks = GameObject.FindGameObjectsWithTag("BricksOnPlayer");
    //        GameObject respawn  = Instantiate(brickPrefab, lasPos, brickPrefab.transform.rotation) as GameObject;
    //        respawn.transform.parent = player.transform;   
    //    //GameObject bricksOnPlayer = Instantiate(bricks, lasPos, Quaternion.identity);
    //    //Debug.Log(lasPos);
    //}
    //public void BrickAheadPlayer()
    //{
    //    if (bricks == null)

    //        bricks = GameObject.FindGameObjectsWithTag("Bricks");

    //    Vector3 aheadPos = new Vector3();

    //    aheadPos.x = lasPos.x;
    //    aheadPos.y = lasPos.y + offsetY;
    //    aheadPos.z = lasPos.z + offsetZ;

    //    GameObject respawn = Instantiate(brickPrefab, aheadPos, Quaternion.AngleAxis(90,Vector3.up));
    //    respawn.transform.parent = player.transform;

    //    //GameObject bricksOnPlayer = Instantiate(bricks, lasPos, Quaternion.identity);
    //    //Debug.Log(lasPos);
    //}
    
}