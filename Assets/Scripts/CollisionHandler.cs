using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    PlayerController player;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            collision.gameObject.GetComponent<PlayerController>().HitTheObstacle();
            collision.gameObject.GetComponent<PlayerController>().transform.position += new Vector3(0f, 3f, -3f);
            // bu k�s�m y,-z do�rultusunda geri sektiriyor. tu�las� var ise devam ko�ulu koyulacak.
            //
            Invoke("CallingFunction", 0.3f);

        }
    }

    void CallingFunction()
    {
        FindObjectOfType<PlayerController>().AfterHitTheObstacle();
        
    }
}
    
        

