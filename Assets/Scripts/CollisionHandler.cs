using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionHandler : MonoBehaviour
{  
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = PlayerController.instance;
        DOTween.Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var renderer = _playerController.GetComponent<Renderer>();
            renderer.material.color = Color.red;
            _playerController.HitTheObstacle(); 
            collision.transform.DOMove(transform.position + new Vector3(0f, 5f, -4f), 0.6f);
            _playerController._splineFollower.Move(0.1f);
            //_playerController.transform.position += new Vector3(0f, 3f, -3f);
            // bu k�s�m y,-z do�rultusunda geri sektiriyor. tu�las� var ise devam ko�ulu koyulacak.
            //
            //Invoke("CallingFunction", 0.3f);
            _playerController.AfterHitTheObstacle();
        }
    }

    void CallingFunction()
    {
        _playerController.AfterHitTheObstacle();
        
    }
}
    
        

