﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{

    public enum Destination { SangSangLobby, Grazie, SangSangEntry, YeonGuEntry, NakSan, NakSanEntry, MiRae, MiRaeEntry, goto3, goto4, goto5, return2, return3, return4}
    public Destination destiantion;

    bool DoTeleport = false;

    public float start = 0.0f;
    float finish = 2.1f;
    Collider ob;
    public Image im;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoTeleport && ob.tag == "Player" && ob.name.Equals("Me"))
        {
            start += Time.deltaTime;
            if (start <= 1.0f)
            {
                im.color += new Color(0, 0, 0, Time.deltaTime);
                ob.GetComponent<TPSCharacterController>().moveSwitch = false;
            }
            else if (start > 1.0f && start < 1.1f)
            {
                switch (destiantion)
                {
                    case Destination.Grazie:
                        ob.GetComponent<Transform>().position = new Vector3(-352f, 1.75f, 72f);
                        break;
                    case Destination.YeonGuEntry:
                        ob.GetComponent<Transform>().position = new Vector3(-77.9f, 9.75f, 3.03f);
                        break;
                    case Destination.SangSangLobby:
                        ob.GetComponent<Transform>().position = new Vector3(-365.232f, 1.5f, 190f);
                        break;
                    case Destination.SangSangEntry:
                        ob.GetComponent<Transform>().position = new Vector3(-35.742f, 2.286f, 41.676f);
                        break;
                    case Destination.NakSan:
                        ob.GetComponent<Transform>().position = new Vector3(-444.86f, -6.24f, -150.08f);
                        GameObject.Find("EnvironmentManager").GetComponent<EventInstance>().attend = true;
                        break;
                    case Destination.NakSanEntry:
                        ob.GetComponent<Transform>().position = new Vector3(87.61545f, 12.72846f, 2f);
                        GameObject.Find("EnvironmentManager").GetComponent<EventInstance>().attend = false;
                        break;
                    case Destination.MiRae:
                        ob.GetComponent<Transform>().position = new Vector3(-410, 3.34f, 51.735f);
                        break;
                    case Destination.MiRaeEntry:
                        ob.GetComponent<Transform>().position = new Vector3(33f, 3.5f, 54.117f);
                        break;
                    case Destination.goto3:
                        ob.GetComponent<Transform>().position = new Vector3(-476f, 3.29f, 115.39f);
                        break;
                    case Destination.goto4:
                        ob.GetComponent<Transform>().position = new Vector3(-466.19f, 3.43f, 334.01f);
                        break;
                    case Destination.goto5:
                        ob.GetComponent<Transform>().position = new Vector3(-577.36f, 3.34f, 114.64f);
                        break;
                    case Destination.return2:
                        ob.GetComponent<Transform>().position = new Vector3(-423.86f, 3.39f, 55f);
                        break;
                    case Destination.return3:
                        ob.GetComponent<Transform>().position = new Vector3(-472f, 3.38f, 110.09f);
                        break;
                    case Destination.return4:
                        ob.GetComponent<Transform>().position = new Vector3(-467.54f, 3.43f, 332.41f);
                        break;
                }
            }
            else if (start >= 1.1f && start <= finish)
            {
                im.color -= new Color(0, 0, 0, Time.deltaTime);
            }
            else if (start > finish)
            {
                ob.GetComponent<TPSCharacterController>().moveSwitch = true;
                start = 0.0f;
                DoTeleport = false;
            }
           
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ob = other;
        if (ob.tag == "Player" && ob.name.Equals("Me")) {
            Debug.Log("ob on");
            DoTeleport = true;
        }

    }
}
