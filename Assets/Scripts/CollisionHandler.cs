using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    PlayerController player;

    private Renderer _renderer;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var renderer = _playerController.GetComponent<Renderer>();
            renderer.material.color = Color.red;

            _playerController.HitTheObstacle();
            _playerController.transform.position += new Vector3(0f, 3f, -3f);
            // bu k�s�m y,-z do�rultusunda geri sektiriyor. tu�las� var ise devam ko�ulu koyulacak.
            //
            Invoke("CallingFunction", 0.3f);
        }
    }

    void CallingFunction()
    {
        _playerController.AfterHitTheObstacle();
        
    }
}
    
        

