﻿using Photon.Pun;
using System.IO;//path사용위해
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPunObservable
{
    static PhotonView PV;//포톤뷰 선언
    
    Scene_Character_Setting scs;

    void Start() 
    {
        PV = GetComponent<PhotonView>();
        scs = GameObject.Find("ItemManager").GetComponent<Scene_Character_Setting>();

        string model = scs.getModel();

        switch (model) {
            case "M1":
                CreateControllerMale1();
                break;
            case "M2":
                CreateControllerMale2();
                break;
            case "M3":
                CreateControllerMale3();
                break;
            case "M4":
                CreateControllerMale4();
                break;
            case "F1":
                CreateControllerFemale1();
                break;
            case "F3":
                CreateControllerFemale3();
                break;
            case "F4":
                CreateControllerFemale4();
                break;
            default:
                CreateControllerMale1();
                break;


        }

        switch (scs.pet) {
            case "Dog":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Dog"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Cat":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Cat"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Alpaca":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Alpaca"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Duck":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Duck"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Duck2":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Duck2"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Cow":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Cow"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Goat":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Goat"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Horse":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Horse"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
            case "Rabbit":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Rabbit"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;
             case "Sheep":
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Sheep"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
                break;


        }




    }
    static void CreateControllerMale1()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        GameObject me = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Male1"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);

    }

    static void CreateControllerMale2()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Male2"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
    }

    static void CreateControllerMale3()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Male3"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
    }

    static void CreateControllerMale4()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Male4"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
    }


    static void CreateControllerFemale1()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Female1"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
    }
    
    static void CreateControllerFemale3()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Female3"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
    }
    
    static void CreateControllerFemale4()//플레이어 컨트롤러 만들기
    {

        Debug.Log("Instantiated Controller");
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character"), Vector3.zero, Quaternion.identity,0, new object[] { PV.ViewID });
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Female4"), new Vector3(83f, 3f, 18f), Quaternion.identity, 0);
    }
    
    public void ReturnSceneCharacter() 
    {
        PhotonNetwork.Disconnect();
        LoadingSceneController.Instance.LoadScene("Scene_Character");
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {


        throw new System.NotImplementedException();
    }




}
