using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _playerRb;
    private bool _left;
    private bool _right;
    private float _jumpForce = 5f;

    

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            Movement();
        }

        if (GameManager.instance.freezeTiles)
        {
            //_playerRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.IncrementScore(1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TopTile" || other.tag == "LeftTile" || other.tag == "StartTile")
        {
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, -Vector3.up);
            if (!Physics.Raycast(downRay, out hit))
            {
                _playerRb.velocity = new Vector3(0, _playerRb.velocity.y, 0);
                GameManager.instance.EndGame();
            }
        }
    }
}


