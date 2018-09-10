using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour
{
    [SerializeField]
    GameObject _begin1;
    [SerializeField]
    GameObject _begin2;
    [SerializeField]
    GameObject _begin3;
    [SerializeField]
    GameObject _begin4;
    [SerializeField]
    GameObject _begin5;
    [SerializeField]
    GameObject _begin6;
    [SerializeField]
    GameObject _begin7;
    [SerializeField]
    GameObject _begin8;
    [SerializeField]
    GameObject _begin9;
    [SerializeField]
    GameObject _begin10;
    [SerializeField]
    GameObject _begin11;

    [SerializeField]
    GameObject _end1;
    [SerializeField]
    GameObject _end2;
    [SerializeField]
    GameObject _end3;
    [SerializeField]
    GameObject _end4;
    [SerializeField]
    GameObject _end5;
    [SerializeField]
    GameObject _pass2;
    [SerializeField]
    GameObject _pass3;
    [SerializeField]
    GameObject _pass4;
    [SerializeField]
    GameObject _pass5;
    [SerializeField]
    GameObject _pass7;
    [SerializeField]
    GameObject _pass8;
    [SerializeField]
    GameObject _pass9;
    [SerializeField]
    GameObject _pass10;
    [SerializeField]
    GameObject _pass11;
    [SerializeField]
    GameObject _pass12;
    [SerializeField]
    GameObject _pass13;
    [SerializeField]
    GameObject _pass14;
    [SerializeField]
    GameObject _pass15;
    [SerializeField]
    GameObject _pass16;
    [SerializeField]
    GameObject _pass17;
    [SerializeField]
    GameObject _pass18;
    [SerializeField]
    GameObject _pass19;
    [SerializeField]
    GameObject _pass20;

    public Vector3 beginpoint;

    public Vector3 beginpoint1;
    public Vector3 beginpoint2;
    public Vector3 beginpoint3;
    public Vector3 beginpoint4;
    public Vector3 beginpoint5;
    public Vector3 beginpoint6;
    public Vector3 beginpoint7;
    public Vector3 beginpoint8;
    public Vector3 beginpoint9;
    public Vector3 beginpoint10;
    public Vector3 beginpoint11;

    public Vector3 endpoint1;
    public Vector3 endpoint2;
    public Vector3 endpoint3;
    public Vector3 endpoint4;
    public Vector3 endpoint5;

    
    public Vector3 passpoint2;
    public Vector3 passpoint3;
    public Vector3 passpoint4;
    public Vector3 passpoint5;
    public Vector3 passpoint7;
    public Vector3 passpoint8;
    public Vector3 passpoint9;
    public Vector3 passpoint10;
    public Vector3 passpoint11;
    public Vector3 passpoint12;
    public Vector3 passpoint13;
    public Vector3 passpoint14;
    public Vector3 passpoint15;
    public Vector3 passpoint16;
    public Vector3 passpoint17;
    public Vector3 passpoint18;
    public Vector3 passpoint19;
    public Vector3 passpoint20;



    public Vector3 point;
    public Vector3 point2;
    public Vector3 point3;
    public Vector3 targetVector;
    public Vector3 CarVector;
    public Vector3 check;

    NavMeshAgent _navMeshAgent;

    public List<Vector3> end4 = new List<Vector3>();
    public List<Vector3> end7 = new List<Vector3>();
    System.Random r = new System.Random();

    public float collisionCheckDistance;
    public bool aboutToCollide;
    public float distanceToCollision;
    public Rigidbody rb;

    public bool nr1;
    public bool nr2;
    public bool nr3;
    public bool nr4;

    public bool trigger;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        collisionCheckDistance = 7f;

        nr1 = false;
        nr2 = false;
        nr3 = false;
        nr4 = false;

        beginpoint1 = _begin1.transform.position;
        beginpoint2 = _begin2.transform.position;
        beginpoint3 = _begin3.transform.position;
        beginpoint4 = _begin4.transform.position;
        beginpoint5 = _begin5.transform.position;
        beginpoint6 = _begin6.transform.position;
        beginpoint7 = _begin7.transform.position;
        beginpoint8 = _begin8.transform.position;
        beginpoint9 = _begin9.transform.position;
        beginpoint10 = _begin10.transform.position;
        beginpoint11 = _begin11.transform.position;

        endpoint1 = _end1.transform.position;
        endpoint2 = _end2.transform.position;
        endpoint3 = _end3.transform.position;
        endpoint4 = _end4.transform.position;
        endpoint5 = _end5.transform.position;
        
        passpoint2 = _pass2.transform.position;
        passpoint3 = _pass3.transform.position;
        passpoint4 = _pass4.transform.position;
        passpoint5 = _pass5.transform.position;
        passpoint7 = _pass7.transform.position;
        passpoint8 = _pass8.transform.position;
        passpoint9 = _pass9.transform.position;
        passpoint10 = _pass10.transform.position;
        passpoint11 = _pass11.transform.position;
        passpoint12 = _pass12.transform.position;
        passpoint13 = _pass13.transform.position;
        passpoint14 = _pass14.transform.position;
        passpoint15 = _pass15.transform.position;
        passpoint16 = _pass16.transform.position;
        passpoint17 = _pass17.transform.position;
        passpoint18 = _pass18.transform.position;
        passpoint19 = _pass19.transform.position;
        passpoint20 = _pass20.transform.position;

        end4.Add(endpoint1);
        end4.Add(endpoint4);
        end4.Add(endpoint5);
        end4.Add(endpoint3);

        end7.Add(endpoint1);
        end7.Add(endpoint5);

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
                point = passpoint5;
                point2 = endpoint5;
                targetVector = endpoint5;
            }

            if (beginpoint == beginpoint2)
            {
                point = endpoint3;
                point2 = endpoint3;
                targetVector = endpoint3;
            }

            if (beginpoint == beginpoint3)
            {
                point = passpoint7;
                point2 = passpoint8;
                targetVector = endpoint2;
            }

            if (beginpoint == beginpoint4)
            {
                int num = r.Next(4);
                if (end4[num] == endpoint1)
                {
                    point = passpoint19;
                    point2 = passpoint9;
                    point3 = passpoint10;
                }
                if (end4[num] == endpoint5)
                {
                    point = passpoint11;
                    point2 = passpoint12;
                }
                if (end4[num] == endpoint4)
                {
                    point = passpoint13;
                    point2 = passpoint14;
                }
                if (end4[num] == endpoint3)
                {
                    point = passpoint20;
                    point2 = passpoint15;
                    point3 = passpoint16;
                }
                targetVector = end4[num];
            }

            if (beginpoint == beginpoint5)
            {
                point = passpoint17;
                point2 = passpoint8;
                targetVector = endpoint2;
            }

            if (beginpoint == beginpoint6)
            {
                point = passpoint2;
                point2 = passpoint3;
                targetVector = endpoint2;
            }

            if (beginpoint == beginpoint7) 
            {
                int num = r.Next(2);
                if (end7[num] == endpoint5)
                {
                    point = passpoint18;
                    point2 = passpoint5;
                }
                else
                {
                    point = passpoint10;
                    point2 = end7[num];
                }
                targetVector = end7[num];
            }

            if (beginpoint == beginpoint8)
            {
                point = passpoint16;
                point2 = endpoint3;
                targetVector = endpoint3;
            }

            if (beginpoint == beginpoint9)
            {
                point = passpoint8;
                point2 = endpoint2;
                targetVector = endpoint2;
            }

            if (beginpoint == beginpoint10)
            {
                point = passpoint3;
                point2 = passpoint4;
                targetVector = endpoint2;
            }

            if (beginpoint == beginpoint11)
            {
                point = passpoint7;
                point2 = passpoint10;
                targetVector = endpoint1;
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
            StopCar(); 
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
                SetDestination();
            }
        }

        if (aboutToCollide != true)
        {
            CarVector = _navMeshAgent.transform.position;
            float x = CarVector.x;
            float z = CarVector.z;

            if (point.x <= x + 10 && point.x >= x - 10 && point.z <= z + 10 && point.z >= z - 10)
            {
                if (point2 != check)
                {
                    SetPoint2();
                }
            }

            if (point2.x <= x + 10 && point2.x >= x - 10 && point2.z <= z + 10 && point2.z >= z - 10)
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

            if (point3.x <= x + 10 && point3.x >= x - 10 && point3.z <= z + 10 && point3.z >= z - 10)
            {
                if (targetVector != check)
                {
                    SetDestination();
                }
            }

            if (targetVector.x <= x + 10 && targetVector.x >= x - 10 && targetVector.z <= z + 10 && targetVector.z >= z - 10)
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

    public void SetDestination()
    {
        nr4 = true;
        nr2 = false;
        nr3 = false;
        _navMeshAgent.SetDestination(targetVector);
    }

    public void StopCar()
    {
        _navMeshAgent.SetDestination(gameObject.transform.position);
    }

}
