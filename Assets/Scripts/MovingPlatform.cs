using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private GameObject[] _waypoints;

    private int _index;

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector2 waypointPosition = _waypoints[_index].transform.position;
        float distance = Vector2.Distance(transform.position, waypointPosition);

        if (distance < 0.1f)
        {
            _index++;

            if (_index > _waypoints.Length - 1)
                _index = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypointPosition, Time.deltaTime * _speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = null;
    }

}
