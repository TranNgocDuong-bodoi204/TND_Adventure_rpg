using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext : EntityContext
{
    public string ANIMATION_MOVE_SPEED {get; } = "Idle_Run";
    public string MOVE_SPEED_PARAM {get; } = "movespeed";
    public string Y_VELOCITY_PARAM {get; } = "yVelocity";
    public string SWORD_ATTACK_INDEX {get;} = "SwordAttackIndex";
    // input Action //
    public PlayerInput p_inputActions {get; private set;}
    // ** Global Variables **
    public Vector2 g_moveInput {get; private set;}
    public bool g_isJumpPressed {get;private set;}
    public bool g_isDashPressed {get;private set;}
    public bool g_isJumping { get;set; }

    // ** state variables
    public bool didJump {get; set;} = false;
    public bool didAirJump {get; set;} = false;
    // state
    public Player_Idle idle {get;private set;}
    public Player_Run run{get;private set;}
    public PlayerJump jump{get;private set;}
    public Player_Dash dash {get; private set;}
    public PlayerWallSlide wallSlide {get; private set;}
    public Player_WallJump wallJump {get; private set;}
    public Player_Fall fall {get; private set;}
    public Player_SwordAttack swordAttack {get; private set;}
    public Player_AirJump airJump {get; private set;}
    
    [Range(0,1)]
    public float airMoveSpeedMultiplier;
    public float airMoveSpeed {get;private set;}
    public float jumpForce=3;
    public Vector2 wallJumForce;
    [Range(1,3)]
    public float dashSpeedMultiplier;
    public float dashMoveSpeed {get;private set;}
    public float dashCoolDown=1f;
    public float attackPushForce ;
    public int attackIndex {get; set;} = 0;

    /// <summary>
    /// Boolean to check
    /// </summary>
    public bool canDash {get; set;} = true;
    public bool canChangeState {get; set;} = true;
    public bool isDashing { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        p_inputActions = new PlayerInput();
        idle = new Player_Idle(mStateMachine, "Idle_Run", this);
        run = new Player_Run(mStateMachine, "Idle_Run", this);
        jump = new PlayerJump(mStateMachine,"Jump_Fall",this);
        fall = new Player_Fall(mStateMachine, "Jump_Fall", this);
        dash = new Player_Dash(mStateMachine, "Dash", this);
        wallSlide = new PlayerWallSlide(mStateMachine, "Wall_Slide", this);
        wallJump = new Player_WallJump(mStateMachine, "Jump_Fall", this);
        swordAttack = new Player_SwordAttack(mStateMachine, "SwordAttack", this);
        airJump = new Player_AirJump(mStateMachine, "Jump_Fall", this);
    }
    protected override void Start()
    {
        base.Start();
        airMoveSpeed = g_moveSpeed * airMoveSpeedMultiplier;
        dashMoveSpeed = g_moveSpeed * dashSpeedMultiplier;
        mStateMachine.SetInitialState(idle);
        animator.SetFloat(ANIMATION_MOVE_SPEED,1);
        SetMoveSPeed();
    }

    protected override bool GroundCheck()
    {
        bool grounded = base.GroundCheck();
        if (grounded)
        {
            g_isJumping = false;
        }
        return grounded;
    }
    protected override void Update()
    {
        base.Update();
    }

    void OnEnable()
    {
        p_inputActions.Enable();
        // setup input action
        PCAction();
        MobileAction();
    }
    private void PCAction()
    {
        p_inputActions.Player.Movement.performed += PerformMove;
        p_inputActions.Player.Movement.canceled += CancelMove;

        p_inputActions.Player.Dash.performed += ctx => g_isDashPressed = true;
        p_inputActions.Player.Dash.canceled += ctx => g_isDashPressed = false;
    }
    private void MobileAction()
    {
        p_inputActions.PlayerMobile.Movement.performed += PerformMove;
        p_inputActions.PlayerMobile.Movement.canceled += CancelMove;

        p_inputActions.PlayerMobile.Dash.performed += ctx => g_isDashPressed = true;
        p_inputActions.PlayerMobile.Dash.canceled += ctx => g_isDashPressed = false;
    }
    private void PerformMove(InputAction.CallbackContext context)
    {
        g_moveInput = context.ReadValue<Vector2>();
        if(Math.Abs(g_moveInput.x) < 1)
        {
            g_moveInput = new Vector2(Mathf.Round(g_moveInput.x),g_moveInput.y);
        }
    }
    private void CancelMove(InputAction.CallbackContext context)
    {
        g_moveInput = Vector2.zero;
    }
    void OnDisable()
    {
        p_inputActions.Disable();
    }
    // Calculate speed
    private void SetMoveSPeed() => this.g_moveSpeed = CalculateSpeed();
    private float CalculateSpeed()
    {
        float animClipCount = 10;
        float timePerClip = 0.04f;
        float unitPerWalkCycle = 0.65f;
        // mỗi 2 clip sẽ đi được 0.5 unit -> 0.1s đi được 0.5 unit
        float velocityS = ((animClipCount/2)* unitPerWalkCycle) / (animClipCount * timePerClip);
        return velocityS;
    }
}
