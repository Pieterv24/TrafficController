using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveBus : MonoBehaviour
{

    [SerializeField]
    Transform _point;

    [SerializeField]
    Transform _point2;

    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;

    public Vector3 point;
    public Vector3 point2;
    public Vector3 targetVector;
    public Vector3 CarVector;

    public float collisionCheckDistance;
    public bool aboutToCollide;
    public float distanceToCollision;
    public Rigidbody rb;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        collisionCheckDistance = 30f;

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            SetPoint();
        }

    }

    void Update()
    {
        RaycastHit hit;
        if (rb.SweepTest(transform.forward, out hit, collisionCheckDistance))
        {
            aboutToCollide = true;
            distanceToCollision = hit.distance;
            StopBus();
        }
        else
        {
            aboutToCollide = false;
            SetPoint();
        }


        if (aboutToCollide != true)
        {
            CarVector = _navMeshAgent.transform.position;
            float x = CarVector.x;
            float z = CarVector.z;

            if (point.x <= x + 10 && point.x >= x - 10 && point.z <= z + 10 && point.z >= z - 10)
            {
                if (_point2 != null)
                {
                    SetPoint2();
                }
                else
                    Destroy(gameObject);
            }

            if (point2.x <= x + 10 && point2.x >= x - 10 && point2.z <= z + 10 && point2.z >= z - 10)
            {
                if (_destination != null)
                {
                    SetDestination();
                }
                else
                    Destroy(gameObject);
            }

            if (targetVector.x <= x + 10 && targetVector.x >= x - 10 && targetVector.z <= z + 10 && targetVector.z >= z - 10)
            {
                Destroy(gameObject);
            }
        }

    }

    public void SetPoint()
    {
        if (_point != null)
        {
            point = _point.transform.position;
            _navMeshAgent.SetDestination(point);
        }
    }

    public void SetPoint2()
    {
        _point = null;
        point2 = _point2.transform.position;
        _navMeshAgent.SetDestination(point2);
    }

    public void SetDestination()
    {
        _point2 = null;
        targetVector = _destination.transform.position;
        _navMeshAgent.SetDestination(targetVector);
    }

    public void StopBus()
    {
        _navMeshAgent.SetDestination(gameObject.transform.position);
    }


}
