using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveBoot : MonoBehaviour {

    [SerializeField]
    GameObject _begin1;
    [SerializeField]
    GameObject _begin2;
    
    [SerializeField]
    GameObject _end1;
    [SerializeField]
    GameObject _end2;

    [SerializeField]
    GameObject _pass1;
    [SerializeField]
    GameObject _pass2;

    public Vector3 beginpoint;

    public Vector3 beginpoint1;
    public Vector3 beginpoint2;

    public Vector3 endpoint1;
    public Vector3 endpoint2;

    public Vector3 passpoint1;
    public Vector3 passpoint2;



    public Vector3 point;
    public Vector3 targetVector;
    public Vector3 BootVector;
    public Vector3 check;

    NavMeshAgent _navMeshAgent;
    

    public float collisionCheckDistance;
    public bool aboutToCollide;
    public float distanceToCollision;
    public Rigidbody rb;

    public bool nr1 = false;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        collisionCheckDistance = 40f;
        

        beginpoint1 = _begin1.transform.position;
        beginpoint2 = _begin2.transform.position;

        endpoint1 = _end1.transform.position;
        endpoint2 = _end2.transform.position;

        passpoint1 = _pass1.transform.position;
        passpoint2 = _pass2.transform.position;

        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        beginpoint = _navMeshAgent.transform.position;


        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (beginpoint == beginpoint1)
            {
                point = passpoint1;
                targetVector = endpoint1;
            }

            if (beginpoint == beginpoint2)
            {
                point = passpoint2;
                targetVector = endpoint2;
            }

            

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
            StopBike();
        }
        else
        {
            aboutToCollide = false;
            if (nr1 == true)
            {
                SetPoint();
            }
            else
            {
                SetDestination();
            }
        }

        if (aboutToCollide != true)
        {
            BootVector = _navMeshAgent.transform.position;
            float x = BootVector.x;
            float z = BootVector.z;

            if (point.x <= x + 5 && point.x >= x - 5 && point.z <= z + 5 && point.z >= z - 5)
            {
                if (targetVector != check)
                {
                    SetDestination();
                }
            }

            if (targetVector.x <= x + 20 && targetVector.x >= x - 20 && targetVector.z <= z + 20 && targetVector.z >= z - 20)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetPoint()
    {
        nr1 = true;
        if (point != null)
        {
            _navMeshAgent.SetDestination(point);
        }
    }

    
    public void SetDestination()
    {
        nr1 = false;
        _navMeshAgent.SetDestination(targetVector);
    }

    public void StopBike()
    {
        _navMeshAgent.SetDestination(gameObject.transform.position);
    }
}
