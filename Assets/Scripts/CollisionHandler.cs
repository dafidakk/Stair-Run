using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class CollisionHandler : MonoBehaviour
{  
    private PlayerController _playerController;
    public double percentage;
    private float length;
    double distance;

    private void Start()
    {
        _playerController = PlayerController.instance;
        DOTween.Init();
    } 
    private void Update()
    { 
        Debug.Log(_playerController.splineProjector.result.percent);
        percentage = _playerController.splineProjector.result.percent;
        distance = _playerController._splineFollower.Travel(1-percentage,transform.position.x,Spline.Direction.Forward);
        length = _playerController._splineFollower.CalculateLength(distance);
       // Debug.Log(length); 
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
                _playerController._splineFollower.follow = false;
            }
            else
            {
                collision.transform.DOMove(transform.position + new Vector3(0f, 5f, -4f), 0.6f);
                _playerController._splineFollower.follow = false;
            }
            //_playerController.AfterHitTheObstacle();  
            _playerController._splineFollower.SetDistance(length);
            _playerController._splineFollower.follow = true;
            _playerController.followSpeed = 250f;


        }
    }

}
    
        

