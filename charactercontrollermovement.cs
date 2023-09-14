using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charactercontrollermovement : MonoBehaviour
{
    public CharacterController ourCharacterController;
    public Vector3 MoveDirection_PlayerInput = Vector3.zero;
    public Vector3 MoveDirection_InternalForce = Vector3.zero;
    public Vector3 MoveDirection_ExternalForce  = Vector3.zero;
    public Vector3 MoveDirection_Final = Vector3.zero;
    private Vector3 PlayerInput_XZ = Vector3.zero;

    public bool isGrounded;
    public bool isGrounded_PreviousFrame;
    [SerializeField] public CollisionFlags CollisionDirection;

    private float gravity_lerped = 0.05f;
    private float jumpvelocity = 20f;
    private float gravity_clamp = 2f;
    private float velocity_y_clamp = -5f;
    private float movespeed = 10;
    private float maxMoveSpeed = 10;

    private int WallJumpsLeft = 3;
    private float respawntimer = 4;

    [Header("Visuals")]
    public Mesh_popscale ourMeshpopscale_ForTouchGround;
    public Animator ourAnimator;
    
    private void Awake()
    {
        if (ourCharacterController == null) ourCharacterController = GetComponent<CharacterController>();
    }

    private void  FX_OnTouchGround()
    {
        ourMeshpopscale_ForTouchGround.Popscale();
    }
    private void Update()
    {
        if (Manager_Game.Instance.currentGameState != Manager_Game.State.Action) return;
        isGrounded = ourCharacterController.isGrounded;
        if (isGrounded == true && isGrounded_PreviousFrame == false)
        {
            FX_OnTouchGround();
            isGrounded_PreviousFrame = true;
        }
        if (Input.GetButtonDown("Jump")) jump();

        if (isGrounded)
        {
            Manager_Game.Instance.mostrecentgroundposition = transform.position;
            WallJumpsLeft = 3;
        }
        else
        {
            isGrounded_PreviousFrame = false;
            MoveDirection_PlayerInput.y = Mathf.Clamp(MoveDirection_PlayerInput.y -= gravity_lerped * Time.deltaTime, -20,20 );
        }
        if (gravity_lerped < gravity_clamp) gravity_lerped = Mathf.Clamp(gravity_lerped += 3f * Time.deltaTime, 0, gravity_clamp);

        PlayerInput_XZ = Manager_Game.Instance.ref_MainCamera.transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))* movespeed*Time.deltaTime);

        if (MoveDirection_PlayerInput.x != PlayerInput_XZ.x) { MoveDirection_PlayerInput.x = (Mathf.Lerp(MoveDirection_PlayerInput.x, PlayerInput_XZ.x, 5 * Time.deltaTime)); }
        if (MoveDirection_PlayerInput.z != PlayerInput_XZ.z) { MoveDirection_PlayerInput.z = (Mathf.Lerp(MoveDirection_PlayerInput.z, PlayerInput_XZ.z, 5 * Time.deltaTime)); }


        CollisionDirection = ourCharacterController.collisionFlags;
        MoveDirection_PlayerInput = new Vector3(Clampfloat(MoveDirection_PlayerInput.x), Clampfloat2(MoveDirection_PlayerInput.y), Clampfloat(MoveDirection_PlayerInput.z));
        if (PlayerInput_XZ != Vector3.zero && isGrounded) ourAnimator.SetFloat("Speed", 1);
        else ourAnimator.SetFloat("Speed", 0);
        MoveDirection_Final = (MoveDirection_PlayerInput + MoveDirection_InternalForce + MoveDirection_ExternalForce);
        if (MoveDirection_Final != Vector3.zero) ourCharacterController.Move(MoveDirection_Final); 


        if (!isGrounded) //timer for respawning, if in air
        {
            if (respawntimer > 0)
            {
                respawntimer -= 1*Time.deltaTime;
            }
            else
            {
                Manager_Game.Instance.respawntoground();
            }
        }
        else
        {
            respawntimer = 4;
        }
    }

    private float Clampfloat(float givenFloat)
    {
        return Mathf.Clamp(givenFloat, -maxMoveSpeed, maxMoveSpeed);
    }

    //velocity_y_clamp
    private float Clampfloat2(float givenFloat)
    {
        return Mathf.Clamp(givenFloat, -0.5f, 0.25f);
    }

    private void jump()
    {
        float extraHeight = 0;
        if (!isGrounded)
        {
            if (WallJumpsLeft < 1) return;
            else
            {
                if (CollisionDirection == CollisionFlags.None || CollisionDirection == CollisionFlags.Above) return;
                else
                {
                    WallJumpsLeft--;
                    extraHeight = 2f;
                }
            }
        }
        gravity_lerped = 0f;
        MoveDirection_PlayerInput.y = jumpvelocity + extraHeight;
        ourAnimator.SetTrigger("Jump");
    }
}
