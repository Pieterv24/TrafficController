using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour {

    public bool PT11;
    public bool PT12;
    public bool PT13;
    public bool PT14;
    public bool PT15;
    public bool PT16;
    public bool PT17;
    public bool PT18;
    public bool PT19;
    public bool PT110;
    public bool PT111;
    public bool PT112;

    public bool ST11;
    public bool ST12;
    public bool ST13;
    public bool ST14;
    public bool ST15;
    public bool ST16;
    public bool ST17;
    public bool ST18;
    public bool ST19;
    public bool ST110;
    public bool ST111;
    public bool ST112;

    void Start () {

        PT11 = false;
        PT12 = false;
        PT13 = false;
        PT14 = false;
        PT15 = false;
        PT16 = false;
        PT17 = false;
        PT18 = false;
        PT19 = false;
        PT110 = false;
        PT111 = false;
        PT112 = false;

        ST11 = false;
        ST12 = false;
        ST13 = false;
        ST14 = false;
        ST15 = false;
        ST16 = false;
        ST17 = false;
        ST18 = false;
        ST19 = false;
        ST110 = false;
        ST111 = false;
        ST112 = false;
    }
	
	void Update () {
        //Primary Triggers true
        if (GameObject.Find("PrimaryTrigger1").GetComponent<primaryTrigger>().occupied)
            PT11 = true;
        if (GameObject.Find("PrimaryTrigger2").GetComponent<primaryTrigger>().occupied)
            PT12 = true;
        if (GameObject.Find("PrimaryTrigger3").GetComponent<primaryTrigger>().occupied)
            PT13 = true;
        if (GameObject.Find("PrimaryTrigger4").GetComponent<primaryTrigger>().occupied)
            PT14 = true;
        if (GameObject.Find("PrimaryTrigger5").GetComponent<primaryTrigger>().occupied)
            PT15 = true;
        if (GameObject.Find("PrimaryTrigger6").GetComponent<primaryTrigger>().occupied) //bus
            PT16 = true;
        if (GameObject.Find("PrimaryTrigger7").GetComponent<primaryTrigger>().occupied)
            PT17 = true;
        if (GameObject.Find("PrimaryTrigger8").GetComponent<primaryTrigger>().occupied)
            PT18 = true;
        if (GameObject.Find("PrimaryTrigger9").GetComponent<primaryTrigger>().occupied)
            PT19 = true;
        if (GameObject.Find("PrimaryTrigger10").GetComponent<primaryTrigger>().occupied)
            PT110 = true;
        if (GameObject.Find("PrimaryTrigger11").GetComponent<primaryTrigger>().occupied)
            PT111 = true;
        if (GameObject.Find("PrimaryTrigger12").GetComponent<primaryTrigger>().occupied)
            PT112 = true;
        //false
        if (!GameObject.Find("PrimaryTrigger1").GetComponent<primaryTrigger>().occupied)
            PT11 = false;
        if (!GameObject.Find("PrimaryTrigger2").GetComponent<primaryTrigger>().occupied)
            PT12 = false;
        if (!GameObject.Find("PrimaryTrigger3").GetComponent<primaryTrigger>().occupied)
            PT13 = false;
        if (!GameObject.Find("PrimaryTrigger4").GetComponent<primaryTrigger>().occupied)
            PT14 = false;
        if (!GameObject.Find("PrimaryTrigger5").GetComponent<primaryTrigger>().occupied)
            PT15 = false;
        if (!GameObject.Find("PrimaryTrigger6").GetComponent<primaryTrigger>().occupied) //bus
            PT16 = false;
        if (!GameObject.Find("PrimaryTrigger7").GetComponent<primaryTrigger>().occupied)
            PT17 = false;
        if (!GameObject.Find("PrimaryTrigger8").GetComponent<primaryTrigger>().occupied)
            PT18 = false;
        if (!GameObject.Find("PrimaryTrigger9").GetComponent<primaryTrigger>().occupied)
            PT19 = false;
        if (!GameObject.Find("PrimaryTrigger10").GetComponent<primaryTrigger>().occupied)
            PT110 = false;
        if (!GameObject.Find("PrimaryTrigger11").GetComponent<primaryTrigger>().occupied)
            PT111 = false;
        if (!GameObject.Find("PrimaryTrigger12").GetComponent<primaryTrigger>().occupied)
            PT112 = false;

        //Secondary Triggers true
        if (GameObject.Find("SecondaryTrigger1").GetComponent<primaryTrigger>().occupied)
            ST11 = true;
        if (GameObject.Find("SecondaryTrigger2").GetComponent<primaryTrigger>().occupied)
            ST12 = true;
        if (GameObject.Find("SecondaryTrigger3").GetComponent<primaryTrigger>().occupied)
            ST13 = true;
        if (GameObject.Find("SecondaryTrigger4").GetComponent<primaryTrigger>().occupied)
            ST14 = true;
        if (GameObject.Find("SecondaryTrigger5").GetComponent<primaryTrigger>().occupied)
            ST15 = true;
        if (GameObject.Find("SecondaryTrigger6").GetComponent<primaryTrigger>().occupied) //bus
            ST16 = true;
        if (GameObject.Find("SecondaryTrigger7").GetComponent<primaryTrigger>().occupied)
            ST17 = true;
        if (GameObject.Find("SecondaryTrigger8").GetComponent<primaryTrigger>().occupied)
            ST18 = true;
        if (GameObject.Find("SecondaryTrigger9").GetComponent<primaryTrigger>().occupied)
            ST19 = true;
        if (GameObject.Find("SecondaryTrigger10").GetComponent<primaryTrigger>().occupied)
            ST110 = true;
        if (GameObject.Find("SecondaryTrigger11").GetComponent<primaryTrigger>().occupied)
            ST111 = true;
        if (GameObject.Find("SecondaryTrigger12").GetComponent<primaryTrigger>().occupied)
            ST112 = true;
        //false
        if (!GameObject.Find("SecondaryTrigger1").GetComponent<primaryTrigger>().occupied)
            ST11 = false;
        if (!GameObject.Find("SecondaryTrigger2").GetComponent<primaryTrigger>().occupied)
            ST12 = false;
        if (!GameObject.Find("SecondaryTrigger3").GetComponent<primaryTrigger>().occupied)
            ST13 = false;
        if (!GameObject.Find("SecondaryTrigger4").GetComponent<primaryTrigger>().occupied)
            ST14 = false;
        if (!GameObject.Find("SecondaryTrigger5").GetComponent<primaryTrigger>().occupied)
            ST15 = false;
        if (!GameObject.Find("SecondaryTrigger6").GetComponent<primaryTrigger>().occupied) //bus
            ST16 = false;
        if (!GameObject.Find("SecondaryTrigger7").GetComponent<primaryTrigger>().occupied)
            ST17 = false;
        if (!GameObject.Find("SecondaryTrigger8").GetComponent<primaryTrigger>().occupied)
            ST18 = false;
        if (!GameObject.Find("SecondaryTrigger9").GetComponent<primaryTrigger>().occupied)
            ST19 = false;
        if (!GameObject.Find("SecondaryTrigger10").GetComponent<primaryTrigger>().occupied)
            ST110 = false;
        if (!GameObject.Find("SecondaryTrigger11").GetComponent<primaryTrigger>().occupied)
            ST111 = false;
        if (!GameObject.Find("SecondaryTrigger12").GetComponent<primaryTrigger>().occupied)
            ST112 = false;


    }
}
