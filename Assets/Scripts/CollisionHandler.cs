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
    double eval;
    private bool gameOver;

    private void Start()
    {
        _playerController = PlayerController.instance;
        DOTween.Init();
    } 
    private void Update()
    {
        if (gameOver)
        {
            GameManager.instance.GameOver();
        }
    } 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var renderer = _playerController.GetComponent<Renderer>();
            var playerAngle = _playerController.transform.localEulerAngles.y;
            renderer.material.color = Color.red;

            _playerController.HitTheObstacle();
            _playerController._splineFollower.follow = false;
            if (playerAngle > 260f)
            {
                collision.transform.DOMove(transform.position + new Vector3(4f, 5f, 0), 0.6f);
            }
            else
            {
                collision.transform.DOMove(transform.position + new Vector3(0f, 5f, -4f), 0.6f).OnComplete(()=> {
                    percentage = _playerController.splineProjector.result.percent;
                    eval = _playerController.splineProjector.Evaluate(percentage).percent;
                    distance = _playerController._splineFollower.Travel(eval, _playerController.gameObject.transform.position.x, Spline.Direction.Forward);
                    _playerController._splineFollower.SetPercent(distance);
                });
            } 
            _playerController._splineFollower.follow = true;

            if (_playerController.bricks.Count > 0)
            {
               _playerController.started = true;
            }
            else
            {
                gameOver = true;
                _playerController.started = false;
            }
        }
    }

}
    
        

