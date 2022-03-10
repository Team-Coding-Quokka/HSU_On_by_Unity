﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.IO; //path사용을위해

public class TPSCharacterController : MonoBehaviour
{
    private Join join;

    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    [SerializeField]
    public float movingSpeed = 3.0f;

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private CapsuleCollider hammer_head;

    [SerializeField]
    private GameObject hammer;

    Rigidbody rb;
    PhotonView PV;

    public Vector3 movingDirection;

    public Animator animator;
    public Button escButton;
    public Button attackButton;

    public SkinnedMeshRenderer characterModel;

    float time = 0.0f;

    [SerializeField]
    AudioSource audioSourceWalk;

    [SerializeField]
    AudioSource audioSourceRun;
    bool audioFlagWalk = false;
    bool audioFlagRun = false;
    public bool moveSwitch = true;


    public Scene_Character_Setting scs;



    public Texture HC01, HC11, HC21, HC31, HC41, HC51, HC61;
    public Texture HC02, HC12, HC22, HC32, HC42, HC52, HC62;
    public Texture HC03, HC13, HC23, HC33, HC43, HC53, HC63;
    public Texture HC04, HC14, HC24, HC34, HC44, HC54, HC64;
    public Texture HC05, HC15, HC25, HC35, HC45, HC55, HC65;
    public Texture HC06, HC16, HC26, HC36, HC46, HC56, HC66;
    public Texture HC07, HC17, HC27, HC37, HC47, HC57, HC67;
    public Texture HC08, HC18, HC28, HC38, HC48, HC58, HC68;
    public Texture HC09, HC19, HC29, HC39, HC49, HC59, HC69;
    public Texture HC10, HC20, HC30, HC40, HC50, HC60, HC70;
    // 머리색 끝

    // 상의 
    public Texture C01, C02, C03, C04, C05, C06, C07, C08, C09, C10;

    // 상의 끝

    // 하의
    public Texture P01, P02, P03, P04, P05, P06, P07, P08, P09, P10;


    // Start is called before the first frame update
    void Awake()
    {
        rb = characterBody.GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            this.name = "Me";
        }
        else if (!PV.IsMine)
        {
            this.name = "OtherPlayer";
            
        }
    }

    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
        escButton = GameObject.Find("UI").transform.Find("Menu_Set").gameObject.transform.Find("Image").gameObject.transform.Find("Reset_Button").gameObject.GetComponent<Button>();
        escButton.onClick.AddListener(escAction);
        attackButton = GameObject.Find("Canvas").transform.Find("AttackButton").GetComponent<Button>();
        attackButton.onClick.AddListener(AttackAction);

        join = GameObject.Find("Join").GetComponent<Join>();
        join.PVID = PV.ViewID;
        audioSourceWalk.mute = false;
        audioSourceWalk.loop = true;
        audioSourceRun.mute = false;
        audioSourceRun.loop = true;

        scs = GameObject.Find("ItemManager").GetComponent<Scene_Character_Setting>();

        if (PV.IsMine)
        {
            CallChangeMyAvatar(scs.hairColor, scs.top, scs.bottom);
        }//ChangeMyAvatar(scs.hairColor, scs.top, scs.bottom);
    }

    private void escAction()
    {
        transform.position = new Vector3(83f, 3f, 21f);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("attack1"))
        {
            moveSwitch = false;
            time += Time.deltaTime;
            if (time >= 0.3f && time <= 0.4f)
            {
                hammer_head.enabled = true;
            }
            else if (time >= 0.8f && time <= 0.9f)
            {
                hammer_head.enabled = false;
            }
            else if (time >= 1.0f)
            {

                animator.SetBool("attack1", false);
                moveSwitch = true;
                time = 0.0f;
            }
        }
    }

    void PlayAudioWalk() 
    {
        audioFlagRun = false;
        if (!audioFlagWalk && !animator.GetBool("isRun") && !animator.GetBool("isJump"))
        {
            audioSourceRun.Stop();
            audioSourceWalk.Play();
        }
        
        audioFlagWalk = true;
    }
    void PlayAudioRun()
    {
        audioFlagWalk = false;
        if (!audioFlagRun && animator.GetBool("isRun") && !animator.GetBool("isJump"))
        {
            audioSourceWalk.Stop();
            audioSourceRun.Play();
        }
        audioFlagRun = true;
    }
    public void Move(Vector2 inputDirection)
    {
        if (!PV.IsMine)
            return;
        // 이동 방향 구하기 1
        //Debug.DrawRay(cameraArm.position, cameraArm.forward, Color.red);

        // 이동 방향 구하기 2
        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

        // 이동 방향키 입력 값 가져오기
        Vector2 moveInput = inputDirection;
        // 이동 방향키 입력 판정 : 이동 방향 벡터가 0보다 크면 입력이 발생하고 있는 중
        bool isMove = moveInput.magnitude != 0;
        bool isRun = movingSpeed >= 5;
        // 입력이 발생하는 중이라면 이동 애니메이션 재생
        animator.SetBool("isMove", isMove);
        animator.SetBool("isRun", isRun);
        if (isMove && moveSwitch)
        {
            time = 0.0f;
            animator.SetBool("conversation", false);
            animator.SetBool("dance", false);
            animator.SetBool("victory", false);
            animator.SetBool("lose", false);
            animator.SetBool("yes", false);
            animator.SetBool("no", false);
            animator.SetBool("attack1", false);
            
            // 카메라가 바라보는 방향
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            // 카메라의 오른쪽 방향
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            // 이동 방향
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            movingDirection = moveDir.normalized;
            // 이동할 때 카메라가 보는 방향 바라보기
            characterBody.forward = lookForward;
            // 이동할 때 이동 방향 바라보기
            characterBody.forward = moveDir;
            if (!isRun) PlayAudioWalk();
            else PlayAudioRun();
            // 이동
            transform.position += moveDir.normalized * Time.deltaTime * movingSpeed;
            
            

        }
        else if (!isMove || !moveSwitch)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isMove", false);
            movingDirection = Vector3.zero;
            audioFlagRun = false;
            audioFlagWalk = false;
            audioSourceWalk.Stop();
            audioSourceRun.Stop();
        }
        
    }

    public void LookAround(Vector3 inputDirection)
    {
        // 마우스 이동 값 검출
        Vector2 mouseDelta = inputDirection;
        // 카메라의 원래 각도를 오일러 각으로 저장
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // 카메라의 피치 값 계산
        float x = camAngle.x - mouseDelta.y;

        // 카메라 피치 값을 위쪽으로 60도 아래쪽으로 25도 이상 움직이지 못하게 제한
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 60f);
        }
        else
        {
            x = Mathf.Clamp(x, 345f, 361f);
        }

        // 카메라 암 회전 시키기
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    private void AttackAction()
    {
        if (!animator.GetBool("attack1")) 
        {
            animator.SetBool("attack1", true);

        }
    }

    public void ActivateHammerRPC()
    {
        PV.RPC("ActivateHammer", RpcTarget.AllBuffered);
    }
    public void DeactivateHammerRPC()
    {
        PV.RPC("DeactivateHammer", RpcTarget.AllBuffered);

    }
    [PunRPC]
    public void ActivateHammer()
    {
        hammer.SetActive(true);
    }
    [PunRPC]
    public void DeactivateHammer()
    {
        hammer.SetActive(false);
    }


    public void CallChangeMyAvatar(string hair, string top, string bottom) 
    {
        PV.RPC("ChangeMyAvatar",RpcTarget.AllBuffered, hair, top, bottom);
    }

    public Texture getHairColor(string hair)
    {
        switch (hair)
        {
            case "HC01":
                return HC01;
                break;
            case "HC02":
                return HC02;
                break;
            case "HC03":
                return HC03;
                break;
            case "HC04":
                return HC04;
                break;
            case "HC05":
                return HC05;
                break;
            case "HC06":
                return HC06;
                break;
            case "HC07":
                return HC07;
                break;
            case "HC08":
                return HC08;
                break;
            case "HC09":
                return HC09;
                break;
            case "HC10":
                return HC10;
                break;
            case "HC11":
                return HC11;
                break;
            case "HC12":
                return HC12;
                break;
            case "HC13":
                return HC13;
                break;
            case "HC14":
                return HC14;
                break;
            case "HC15":
                return HC15;
                break;
            case "HC16":
                return HC16;
                break;
            case "HC17":
                return HC17;
                break;
            case "HC18":
                return HC18;
                break;
            case "HC19":
                return HC19;
                break;
            case "HC20":
                return HC20;
                break;
            case "HC21":
                return HC21;
                break;
            case "HC22":
                return HC22;
                break;
            case "HC23":
                return HC23;
                break;
            case "HC24":
                return HC24;
                break;
            case "HC25":
                return HC25;
                break;
            case "HC26":
                return HC26;
                break;
            case "HC27":
                return HC27;
                break;
            case "HC28":
                return HC28;
                break;
            case "HC29":
                return HC29;
                break;
            case "HC30":
                return HC30;
                break;
            case "HC31":
                return HC31;
                break;
            case "HC32":
                return HC32;
                break;
            case "HC33":
                return HC33;
                break;
            case "HC34":
                return HC34;
                break;
            case "HC35":
                return HC35;
                break;
            case "HC36":
                return HC36;
                break;
            case "HC37":
                return HC37;
                break;
            case "HC38":
                return HC38;
                break;
            case "HC39":
                return HC39;
                break;
            case "HC40":
                return HC40;
                break;
            case "HC41":
                return HC41;
                break;
            case "HC42":
                return HC42;
                break;
            case "HC43":
                return HC43;
                break;
            case "HC44":
                return HC44;
                break;
            case "HC45":
                return HC45;
                break;
            case "HC46":
                return HC46;
                break;
            case "HC47":
                return HC47;
                break;
            case "HC48":
                return HC48;
                break;
            case "HC49":
                return HC49;
                break;
            case "HC50":
                return HC50;
                break;
            case "HC51":
                return HC51;
                break;
            case "HC52":
                return HC52;
                break;
            case "HC53":
                return HC53;
                break;
            case "HC54":
                return HC54;
                break;
            case "HC55":
                return HC55;
                break;
            case "HC56":
                return HC56;
                break;
            case "HC57":
                return HC57;
                break;
            case "HC58":
                return HC58;
                break;
            case "HC59":
                return HC59;
                break;
            case "HC60":
                return HC60;
                break;
            case "HC61":
                return HC61;
                break;
            case "HC62":
                return HC62;
                break;
            case "HC63":
                return HC63;
                break;
            case "HC64":
                return HC64;
                break;
            case "HC65":
                return HC65;
                break;
            case "HC66":
                return HC66;
                break;
            case "HC67":
                return HC67;
                break;
            case "HC68":
                return HC68;
                break;
            case "HC69":
                return HC69;
                break;
            case "HC70":
                return HC70;
                break;
            default:
                return null;
        }

    }
    public Texture getTop(string top)
    {
        switch (top)
        {
            case "C01":
                return C01;
                break;
            case "C02":
                return C02;
                break;
            case "C03":
                return C03;
                break;
            case "C04":
                return C04;
                break;
            case "C05":
                return C05;
                break;
            case "C06":
                return C06;
                break;
            case "C07":
                return C07;
                break;
            case "C08":
                return C08;
                break;
            case "C09":
                return C09;
                break;
            case "C10":
                return C10;
                break;
            default:
                return null;
        }
    }
    public Texture getBottom(string bottom)
    {
        switch (bottom)
        {
            case "P01":
                return P01;
                break;
            case "P02":
                return P02;
                break;
            case "P03":
                return P03;
                break;
            case "P04":
                return P04;
                break;
            case "P05":
                return P05;
                break;
            case "P06":
                return P06;
                break;
            case "P07":
                return P07;
                break;
            case "P08":
                return P08;
                break;
            case "P09":
                return P09;
                break;
            case "P10":
                return P10;
                break;
            default:
                return null;
        }
    }

    [PunRPC]
    public void ChangeMyAvatar(string hair, string top, string bottom)
    {
        Debug.Log(hair+top+bottom);
        if (!(hair==""))
            characterModel.materials[2].SetTexture("_MainTex",getHairColor(hair));
        if (!(top == ""))
            characterModel.materials[0].SetTexture("_MainTex", getTop(top));
        if (!(bottom == ""))
            characterModel.materials[1].SetTexture("_MainTex", getBottom(bottom));
    }
}
