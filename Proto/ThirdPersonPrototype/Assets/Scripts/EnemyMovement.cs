using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent _enemy;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> _points;
    private int index;

    private bool playerDetected = false;

    void Start()
    {
        _enemy = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //patroling();
        if (!playerDetected)
        {

            _enemy.speed = 2f;
            _enemy.SetDestination(_points[index].transform.position);
            if (_enemy.transform.position.x == _points[index].transform.position.x && _enemy.transform.position.z == _points[index].transform.position.z)
            {
                if (index < _points.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
        }

        // var x = gameObject.GetComponent<FieldOfView>().VisibleTargets;

        // foreach (var detected in x)
        // {
        //     if (detected.transform.gameObject.tag == "Player")
        //     {
        //         Debug.Log("Player detected by " + gameObject.name);
        //         _enemy.SetDestination(detected.transform.position);
        //         playerDetected = true;
        //     }
        // }

    }

    // private void patroling()
    // {
    //     _enemy.speed = 2f;
    //     _enemy.SetDestination(_points[index].transform.position);
    //     if (_enemy.transform.position.x == _points[index].transform.position.x && _enemy.transform.position.z == _points[index].transform.position.z)
    //     {
    //         if (index < _points.Count - 1)
    //         {
    //             index++;
    //         }
    //         else
    //         {
    //             index = 0;
    //         }
    //     }

    // }
}
