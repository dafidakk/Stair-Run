using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().HitTheObstacle();
            collision.gameObject.GetComponent<PlayerController>().transform.position += new Vector3(0f, 2f, -3f);
            // bu k�s�m y,-z do�rultusunda geri sektiriyor. tu�las� var ise devam ko�ulu koyulacak.
        }
    }
}
    
        

