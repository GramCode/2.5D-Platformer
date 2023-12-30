using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject[] _waypoints;

    private int _index = 1;
    private bool _move = false;
    private int _previousIndex;

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector2 waypointPosition = _waypoints[_index].transform.position;
        float distance = Vector2.Distance(transform.position, waypointPosition);

        _previousIndex = _index;

        if (distance < 0.1f)
        {
            _index++;

            if (_index > _waypoints.Length - 1)
                _index = 0;
        }

        if (_previousIndex != _index)
        {
            _move = false;
            ElevatorPanel panel = GameObject.Find("Elevator_Panel").GetComponent<ElevatorPanel>();
            panel.ElevatorNotMoving();
        }

        if (_move != false)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypointPosition, Time.deltaTime * _speed);
        }
   

    }

    public void CallElevator()
    {
        _move = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
