using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionHandler : MonoBehaviour
{  
    private PlayerController _playerController;
    private float eulerAngY;

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
            var playerAngle = _playerController.transform.localEulerAngles.y;
            renderer.material.color = Color.red;
            _playerController.HitTheObstacle();
            if (playerAngle > 260f)
            {
                collision.transform.DOMove(transform.position + new Vector3(4f, 5f, 0), 0.6f);
            }
            else
            {
                collision.transform.DOMove(transform.position + new Vector3(0f, 5f, -4f), 0.6f);
            }
            
            _playerController._splineFollower.Move(0.1f);
            //_playerController.transform.position += new Vector3(0f, 3f, -3f);
            // bu kýsým y,-z doðrultusunda geri sektiriyor. tuðlasý var ise devam koþulu koyulacak.
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
    
        

