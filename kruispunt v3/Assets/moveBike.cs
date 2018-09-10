using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveBike : MonoBehaviour {

    [SerializeField]
    GameObject _begin1;
    [SerializeField]
    GameObject _begin2;
    [SerializeField]
    GameObject _begin3;
    [SerializeField]
    GameObject _begin4;
    
    [SerializeField]
    GameObject _end1;
    [SerializeField]
    GameObject _end2;
    [SerializeField]
    GameObject _end3;
    [SerializeField]
    GameObject _end4;

    [SerializeField]
    GameObject _pass1;
    [SerializeField]
    GameObject _pass2;
    [SerializeField]
    GameObject _pass3;
    [SerializeField]
    GameObject _pass4;
    [SerializeField]
    GameObject _pass5;
    [SerializeField]
    GameObject _pass6;
    [SerializeField]
    GameObject _pass7;
    [SerializeField]
    GameObject _pass8;
    [SerializeField]
    GameObject _pass9;

    public Vector3 beginpoint;

    public Vector3 beginpoint1;
    public Vector3 beginpoint2;
    public Vector3 beginpoint3;
    public Vector3 beginpoint4;

    public Vector3 endpoint1;
    public Vector3 endpoint2;
    public Vector3 endpoint3;
    public Vector3 endpoint4;

    public Vector3 passpoint1;
    public Vector3 passpoint2;
    public Vector3 passpoint3;
    public Vector3 passpoint4;
    public Vector3 passpoint5;
    public Vector3 passpoint6;
    public Vector3 passpoint7;
    public Vector3 passpoint8;
    public Vector3 passpoint9;



    public Vector3 point;
    public Vector3 point2;
    public Vector3 point3;
    public Vector3 point4;
    public Vector3 point5;
    public Vector3 point6;
    public Vector3 targetVector;
    public Vector3 BikeVector;
    public Vector3 check;

    NavMeshAgent _navMeshAgent;

    public List<Vector3> end1 = new List<Vector3>();
    public List<Vector3> end2 = new List<Vector3>();
    public List<Vector3> end3 = new List<Vector3>();
    public List<Vector3> end4 = new List<Vector3>();
    System.Random r = new System.Random();

    public float collisionCheckDistance;
    public bool aboutToCollide;
    public float distanceToCollision;
    public Rigidbody rb;

    public bool nr1;
    public bool nr2;
    public bool nr3;
    public bool nr4;
    public bool nr5;
    public bool nr6;
    public bool nr7;

    public bool trigger;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        collisionCheckDistance = 7f;

        nr1 = false;
        nr2 = false;
        nr3 = false;
        nr4 = false;
        nr5 = false;
        nr6 = false;
        nr7 = false;

        beginpoint1 = _begin1.transform.position;
        beginpoint2 = _begin2.transform.position;
        beginpoint3 = _begin3.transform.position;
        beginpoint4 = _begin4.transform.position;

        endpoint1 = _end1.transform.position;
        endpoint2 = _end2.transform.position;
        endpoint3 = _end3.transform.position;
        endpoint4 = _end4.transform.position;

        passpoint1 = _pass1.transform.position;
        passpoint2 = _pass2.transform.position;
        passpoint3 = _pass3.transform.position;
        passpoint4 = _pass4.transform.position;
        passpoint5 = _pass5.transform.position;
        passpoint6 = _pass6.transform.position;
        passpoint7 = _pass7.transform.position;
        passpoint8 = _pass8.transform.position;
        passpoint9 = _pass9.transform.position;

        end1.Add(endpoint2);
        end1.Add(endpoint3);
        end1.Add(endpoint4);

        end2.Add(endpoint1);
        end2.Add(endpoint3);
        end2.Add(endpoint4);

        end3.Add(endpoint1);
        end3.Add(endpoint2);
        end3.Add(endpoint4);

        end4.Add(endpoint1);
        end4.Add(endpoint2);
        end4.Add(endpoint3);

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
                int num = r.Next(3);
                if (end1[num] == endpoint2)
                {
                    point = passpoint1;
                    point2 = passpoint2;
                    point3 = passpoint3;
                    point4 = passpoint5;
                    point5 = passpoint6;
                    point6 = passpoint9;
                }
                if (end1[num] == endpoint3)
                {
                    point = passpoint1;
                    point2 = passpoint2;
                    point3 = passpoint3;
                    point4 = passpoint4;
                }
                if (end1[num] == endpoint4)
                {
                    point = passpoint1;
                    point2 = passpoint2;
                }
                targetVector = end1[num];
            }

            if (beginpoint == beginpoint2)
            {
                int num = r.Next(3);
                if (end2[num] == endpoint1)
                {
                    point = passpoint7;
                    point2 = passpoint8;
                }
                if (end2[num] == endpoint3)
                {
                    point = passpoint7;
                    point2 = passpoint8;
                    point3 = passpoint1;
                    point4 = passpoint2;
                    point5 = passpoint3;
                    point6 = passpoint4;
                }
                if (end2[num] == endpoint4)
                {
                    point = passpoint7;
                    point2 = passpoint8;
                    point3 = passpoint1;
                    point4 = passpoint2;
                }
                targetVector = end2[num];
            }

            if (beginpoint == beginpoint3)
            {
                int num = r.Next(3);
                if (end3[num] == endpoint1)
                {
                    point = passpoint5;
                    point2 = passpoint6;
                    point3 = passpoint7;
                    point4 = passpoint8;
                }
                if (end3[num] == endpoint2)
                {
                    point = passpoint5;
                    point2 = passpoint6;
                    point3 = passpoint9;
                }
                if (end3[num] == endpoint4)
                {
                    point = passpoint5;
                    point2 = passpoint6;
                    point3 = passpoint7;
                    point4 = passpoint8;
                    point5 = passpoint1;
                    point6 = passpoint2;
                }
                targetVector = end3[num];
            }

            if (beginpoint == beginpoint4)
            {
                int num = r.Next(3);
                if (end4[num] == endpoint1)
                {
                    point = passpoint3;
                    point2 = passpoint4;
                    point3 = passpoint5;
                    point4 = passpoint6;
                    point5 = passpoint7;
                    point6 = passpoint8;
                }
                if (end4[num] == endpoint2)
                {
                    point = passpoint3;
                    point2 = passpoint4;
                    point3 = passpoint5;
                    point4 = passpoint6;
                    point5 = passpoint9;
                }
                if (end4[num] == endpoint3)
                {
                    point = passpoint3;
                    point2 = passpoint4;
                }
                targetVector = end4[num];
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
            if (nr2 == true)
            {
                SetPoint2();
            }
            if (nr3 == true)
            {
                SetPoint3();
            }
            if (nr4 == true)
            {
                SetPoint4();
            }
            if (nr5 == true)
            {
                SetPoint5();
            }
            if (nr6 == true)
            {
                SetPoint6();
            }
            if (nr7 == true)
            {
                SetDestination();
            }
        }

        if (aboutToCollide != true)
        {
            BikeVector = _navMeshAgent.transform.position;
            float x = BikeVector.x;
            float z = BikeVector.z;

            if (point.x <= x + 5 && point.x >= x - 5 && point.z <= z + 5 && point.z >= z - 5)
            {
                if (point2 != check)
                {
                    SetPoint2();
                }
            }

            if (point2.x <= x + 5 && point2.x >= x - 5 && point2.z <= z + 5 && point2.z >= z - 5)
            {
                if (point3 != check)
                {
                    SetPoint3();
                }
                else
                {
                    SetDestination();
                }
            }

            if (point3.x <= x + 5 && point3.x >= x - 5 && point3.z <= z + 5 && point3.z >= z - 5)
            {
                if (point4 != check)
                {
                    SetPoint4();
                }
                else
                {
                    SetDestination();
                }
            }

            if (point4.x <= x + 5 && point4.x >= x - 5 && point4.z <= z + 5 && point4.z >= z - 5)
            {
                if (point5 != check)
                {
                    SetPoint5();
                }
                else
                {
                    SetDestination();
                }
            }

            if (point5.x <= x + 5 && point5.x >= x - 5 && point5.z <= z + 5 && point5.z >= z - 5)
            {
                if (point6 != check)
                {
                    SetPoint6();
                }
                else
                {
                    SetDestination();
                }
            }

            if (point6.x <= x + 5 && point6.x >= x - 5 && point6.z <= z + 5 && point6.z >= z - 5)
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

    public void SetPoint2()
    {
        nr2 = true;
        nr1 = false;
        _navMeshAgent.SetDestination(point2);
    }

    public void SetPoint3()
    {
        nr3 = true;
        nr2 = false;
        _navMeshAgent.SetDestination(point3);
    }

    public void SetPoint4()
    {
        nr4 = true;
        nr3 = false;
        _navMeshAgent.SetDestination(point4);
    }

    public void SetPoint5()
    {
        nr5 = true;
        nr4 = false;
        _navMeshAgent.SetDestination(point5);
    }

    public void SetPoint6()
    {
        nr6 = true;
        nr5 = false;
        _navMeshAgent.SetDestination(point6);
    }

    public void SetDestination()
    {
        nr7 = true;
        nr2 = false;
        nr3 = false;
        nr4 = false;
        nr5 = false;
        nr6 = false;
        _navMeshAgent.SetDestination(targetVector);
    }

    public void StopBike()
    {
        _navMeshAgent.SetDestination(gameObject.transform.position);
    }
}
