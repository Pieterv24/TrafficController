using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoot : MonoBehaviour {

    [SerializeField]
    GameObject _boot;
    [SerializeField]
    GameObject _spawnPoint1;
    [SerializeField]
    GameObject _spawnPoint2;
    [SerializeField]
    GameObject _begin;

    public Vector3 beginpoint;

    public Vector3 beginpoint1;
    public Vector3 beginpoint2;
    public float timer = 0f;
    public GameObject Boot;
    public List<Vector3> beginings = new List<Vector3>();
    System.Random r = new System.Random();

    void Start()
    {
        Boot = _boot;

        beginpoint1 = _spawnPoint1.transform.position;
        beginpoint2 = _spawnPoint2.transform.position;

        beginings.Add(beginpoint1);
        beginings.Add(beginpoint2);
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 50)
        {
            int num = r.Next(2);
            beginpoint = beginings[num];
            if (!GameObject.Find("Trigger4" + (num + 1)).GetComponent<primaryTrigger>().occupied) //will check if true
            {
                Instantiate(Boot, beginpoint, Quaternion.identity);
                timer = 0;
            }
        }


    }
}
