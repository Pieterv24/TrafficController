using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopregeling : MonoBehaviour {
    
    [SerializeField]
    GameObject _stoplicht1;
    [SerializeField]
    GameObject _stoplicht2;
    [SerializeField]
    GameObject _stoplicht3;
    [SerializeField]
    GameObject _stoplicht4;
    [SerializeField]
    GameObject _stoplicht5;
    [SerializeField]
    GameObject _stoplicht6;
    [SerializeField]
    GameObject _stoplicht7;
    [SerializeField]
    GameObject _stoplicht8;
    [SerializeField]
    GameObject _stoplicht9;
    [SerializeField]
    GameObject _stoplicht10;
    [SerializeField]
    GameObject _stoplicht11;
    [SerializeField]
    GameObject _stoplicht12;
    [SerializeField]
    GameObject _stoplicht13;
    [SerializeField]
    GameObject _stoplicht13B;

    [SerializeField]
    GameObject _B1;
    [SerializeField]
    GameObject _B2;
    [SerializeField]
    GameObject _B3;
    [SerializeField]
    GameObject _B4;

    [SerializeField]
    GameObject _Bo1;
    [SerializeField]
    GameObject _Bo2;


    [SerializeField]
    GameObject _brug;

    public bool stop1;
    public bool stop2;
    public bool stop3;
    public bool stop4;
    public bool stop5;
    public bool stop6;
    public bool stop7;
    public bool stop8;
    public bool stop9;
    public bool stop10;
    public bool stop11;
    public bool stop12;
    public bool stop13;
    public bool stopB1;
    public bool stopB2;
    public bool stopB3;
    public bool stopB4;
    public bool stopBo1;
    public bool stopBo2;
    public bool brugMoetOpenLocal;

    public float timer = 0f;

    void Start ()
    {
        stop1 = true;
        stop2 = true;
        stop3 = true;
        stop4 = true;
        stop5 = true;
        stop6 = true;
        stop7 = true;
        stop8 = true;
        stop9 = true;
        stop10 = true;
        stop11 = true;
        stop12 = true;
        stop13 = true;
        stopB1 = true;
        stopB2 = true;
        stopB3 = true;
        stopB4 = true;
        stopBo1 = true;
        stopBo2 = true;

    }
	

	void Update ()
    {
        // get values from cotroller
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop11 == "green")
            stop1 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop11 == "red")
            stop1 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop12 == "green")
            stop2 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop12 == "red")
            stop2 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop13 == "green")
            stop3 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop13 == "red")
            stop3 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop14 == "green")
            stop4 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop14 == "red")
            stop4 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop15 == "green")
            stop5 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop15 == "red")
            stop5 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop16 == "green")
            stop6 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop16 == "red")
            stop6 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop17 == "green")
            stop7 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop17 == "red")
            stop7 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop18 == "green")
            stop8 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop18 == "red")
            stop8 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop19 == "green")
            stop9 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop19 == "red")
            stop9 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop110 == "green")
            stop10 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop110 == "red")
            stop10 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop111 == "green")
            stop11 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop111 == "red")
            stop11 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop112 == "green")
            stop12 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop112 == "red")
            stop12 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop113 == "green")
            stop13 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stop113 == "red")
            stop13 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB1 == "green")
            stopB1 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB1 == "red")
            stopB1 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB2 == "green")
            stopB2 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB2 == "red")
            stopB2 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB3 == "green")
            stopB3 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB3 == "red")
            stopB3 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB4 == "green")
            stopB4 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopB4 == "red")
            stopB4 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopBo1 == "green")
            stopBo1 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopBo1 == "red")
            stopBo1 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopBo2 == "green")
            stopBo2 = false;
        if (GameObject.Find("stop_regeing").GetComponent<Networking>().stopBo2 == "red")
            stopBo2 = true;

        if (GameObject.Find("stop_regeing").GetComponent<Networking>().brugmoetopen != brugMoetOpenLocal)
        {
            brugMoetOpenLocal = GameObject.Find("stop_regeing").GetComponent<Networking>().brugmoetopen;

            if (brugMoetOpenLocal)
            {
                // brug open
                brugOpen();
            } else
            {
                // brug dicht
                brugDicht();
            }
        }

        // stop
        if (stop1 == false)
        {
            Vector3 temp = new Vector3(_stoplicht1.transform.position.x, -5.0f, _stoplicht1.transform.position.z);
            _stoplicht1.transform.position = temp;
        }

        if (stop1 == true)
        {
            Vector3 temp = new Vector3(_stoplicht1.transform.position.x, 5.0f, _stoplicht1.transform.position.z);
            _stoplicht1.transform.position = temp;
        }

        if (stop2 == false)
        {
            Vector3 temp = new Vector3(_stoplicht2.transform.position.x, -5.0f, _stoplicht2.transform.position.z);
            _stoplicht2.transform.position = temp;
        }

        if (stop2 == true)
        {
            Vector3 temp = new Vector3(_stoplicht2.transform.position.x, 5.0f, _stoplicht2.transform.position.z);
            _stoplicht2.transform.position = temp;
        }

        if (stop3 == false)
        {
            Vector3 temp = new Vector3(_stoplicht3.transform.position.x, -5.0f, _stoplicht3.transform.position.z);
            _stoplicht3.transform.position = temp;
        }

        if (stop3 == true)
        {
            Vector3 temp = new Vector3(_stoplicht3.transform.position.x, 5.0f, _stoplicht3.transform.position.z);
            _stoplicht3.transform.position = temp;
        }

        if (stop4 == false)
        {
            Vector3 temp = new Vector3(_stoplicht4.transform.position.x, -5.0f, _stoplicht4.transform.position.z);
            _stoplicht4.transform.position = temp;
        }

        if (stop4 == true)
        {
            Vector3 temp = new Vector3(_stoplicht4.transform.position.x, 5.0f, _stoplicht4.transform.position.z);
            _stoplicht4.transform.position = temp;
        }

        if (stop5 == false)
        {
            Vector3 temp = new Vector3(_stoplicht5.transform.position.x, -5.0f, _stoplicht5.transform.position.z);
            _stoplicht5.transform.position = temp;
        }

        if (stop5 == true)
        {
            Vector3 temp = new Vector3(_stoplicht5.transform.position.x, 5.0f, _stoplicht5.transform.position.z);
            _stoplicht5.transform.position = temp;
        }

        if (stop6 == false)
        {
            Vector3 temp = new Vector3(_stoplicht6.transform.position.x, -5.0f, _stoplicht6.transform.position.z);
            _stoplicht6.transform.position = temp;
        }

        if (stop6 == true)
        {
            Vector3 temp = new Vector3(_stoplicht6.transform.position.x, 5.0f, _stoplicht6.transform.position.z);
            _stoplicht6.transform.position = temp;
        }

        if (stop7 == false)
        {
            Vector3 temp = new Vector3(_stoplicht7.transform.position.x, -5.0f, _stoplicht7.transform.position.z);
            _stoplicht7.transform.position = temp;
        }

        if (stop7 == true)
        {
            Vector3 temp = new Vector3(_stoplicht7.transform.position.x, 5.0f, _stoplicht7.transform.position.z);
            _stoplicht7.transform.position = temp;
        }

        if (stop8 == false)
        {
            Vector3 temp = new Vector3(_stoplicht8.transform.position.x, -5.0f, _stoplicht8.transform.position.z);
            _stoplicht8.transform.position = temp;
        }

        if (stop8 == true)
        {
            Vector3 temp = new Vector3(_stoplicht8.transform.position.x, 5.0f, _stoplicht8.transform.position.z);
            _stoplicht8.transform.position = temp;
        }

        if (stop9 == false)
        {
            Vector3 temp = new Vector3(_stoplicht9.transform.position.x, -5.0f, _stoplicht9.transform.position.z);
            _stoplicht9.transform.position = temp;
        }

        if (stop9 == true)
        {
            Vector3 temp = new Vector3(_stoplicht9.transform.position.x, 5.0f, _stoplicht9.transform.position.z);
            _stoplicht9.transform.position = temp;
        }

        if (stop10 == false)
        {
            Vector3 temp = new Vector3(_stoplicht10.transform.position.x, -5.0f, _stoplicht10.transform.position.z);
            _stoplicht10.transform.position = temp;
        }

        if (stop10 == true)
        {
            Vector3 temp = new Vector3(_stoplicht10.transform.position.x, 5.0f, _stoplicht10.transform.position.z);
            _stoplicht10.transform.position = temp;
        }

        if (stop11 == false)
        {
            Vector3 temp = new Vector3(_stoplicht11.transform.position.x, -5.0f, _stoplicht11.transform.position.z);
            _stoplicht11.transform.position = temp;
        }

        if (stop11 == true)
        {
            Vector3 temp = new Vector3(_stoplicht11.transform.position.x, 5.0f, _stoplicht11.transform.position.z);
            _stoplicht11.transform.position = temp;
        }

        if (stop12 == false)
        {
            Vector3 temp = new Vector3(_stoplicht12.transform.position.x, -5.0f, _stoplicht12.transform.position.z);
            _stoplicht12.transform.position = temp;
        }

        if (stop12 == true)
        {
            Vector3 temp = new Vector3(_stoplicht12.transform.position.x, 5.0f, _stoplicht12.transform.position.z);
            _stoplicht12.transform.position = temp;
        }

        if (stop13 == false)
        {
            Vector3 temp = new Vector3(_stoplicht13.transform.position.x, -5.0f, _stoplicht13.transform.position.z);
            Vector3 temp2 = new Vector3(_stoplicht13B.transform.position.x, -5.0f, _stoplicht13B.transform.position.z);
            _stoplicht13.transform.position = temp;
            _stoplicht13B.transform.position = temp2;
        }

        if (stop13 == true)
        {
            Vector3 temp = new Vector3(_stoplicht13.transform.position.x, 5.0f, _stoplicht13.transform.position.z);
            Vector3 temp2 = new Vector3(_stoplicht13B.transform.position.x, 5.0f, _stoplicht13B.transform.position.z);
            _stoplicht13.transform.position = temp;
            _stoplicht13B.transform.position = temp2;
        }


        if (stopB1 == false)
        {
            Vector3 temp = new Vector3(_B1.transform.position.x, -5.0f, _B1.transform.position.z);
            _B1.transform.position = temp;
        }

        if (stopB1 == true)
        {
            Vector3 temp = new Vector3(_B1.transform.position.x, 5.0f, _B1.transform.position.z);
            _B1.transform.position = temp;
        }


        if (stopB2 == false)
        {
            Vector3 temp = new Vector3(_B2.transform.position.x, -5.0f, _B2.transform.position.z);
            _B2.transform.position = temp;
        }

        if (stopB2 == true)
        {
            Vector3 temp = new Vector3(_B2.transform.position.x, 5.0f, _B2.transform.position.z);
            _B2.transform.position = temp;
        }


        if (stopB3 == false)
        {
            Vector3 temp = new Vector3(_B3.transform.position.x, -5.0f, _B3.transform.position.z);
            _B3.transform.position = temp;
        }

        if (stopB3 == true)
        {
            Vector3 temp = new Vector3(_B3.transform.position.x, 5.0f, _B3.transform.position.z);
            _B3.transform.position = temp;
        }


        if (stopB4 == false)
        {
            Vector3 temp = new Vector3(_B4.transform.position.x, -5.0f, _B4.transform.position.z);
            _B4.transform.position = temp;
        }

        if (stopB4 == true)
        {
            Vector3 temp = new Vector3(_B4.transform.position.x, 5.0f, _B4.transform.position.z);
            _B4.transform.position = temp;
        }

        if (stopBo1 == false)
        {
            Vector3 temp = new Vector3(_Bo1.transform.position.x, -5.0f, _Bo1.transform.position.z);
            _Bo1.transform.position = temp;
        }

        if (stopBo1 == true)
        {
            Vector3 temp = new Vector3(_Bo1.transform.position.x, 5.0f, _Bo1.transform.position.z);
            _Bo1.transform.position = temp;
        }


        if (stopBo2 == false)
        {
            Vector3 temp = new Vector3(_Bo2.transform.position.x, -5.0f, _Bo2.transform.position.z);
            _Bo2.transform.position = temp;
        }

        if (stopBo2 == true)
        {
            Vector3 temp = new Vector3(_Bo2.transform.position.x, 5.0f, _Bo2.transform.position.z);
            _Bo2.transform.position = temp;
        }
    }

    public void brugOpen()
    {
        Vector3 brug = new Vector3(_brug.transform.position.x, 20.0f, _brug.transform.position.z);
        _brug.transform.position = brug;
        GameObject.Find("stop_regeing").GetComponent<Networking>().addBrigeResponse(new Assets.Models.BridgeResponse()
        {
            type = "BridgeStatusData",
            opened = true
        });
    }

    public void brugDicht()
    {
        Vector3 brug = new Vector3(_brug.transform.position.x, -2.23f, _brug.transform.position.z);
        _brug.transform.position = brug;
        GameObject.Find("stop_regeing").GetComponent<Networking>().addBrigeResponse(new Assets.Models.BridgeResponse()
        {
            type = "BridgeStatusData",
            opened = false
        });
    }
}
