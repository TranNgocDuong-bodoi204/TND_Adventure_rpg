using System.Collections;
using UnityEngine;

public class EnemyContext : EntityContext
{
    public bool testButton;
    // parameters string
    public string PARAM_ATTACKINDEX {get; private set;} = "AttackIndex";
    public string TAG_PLAYER {get; private set; } = "Player";
    [Header("Enemy Context variables and info")]

    // layers
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask ignoreLayer;

    // float variables
    [SerializeField] private float offsetGroundCheck = 0;
    public float minDistanceNeedFallBack = 1.5f;
    public float attackTimeCooldown;

    // others
    public int attackIndex {get; set;} = 0;

    // cooldown timer
    public float timer {get;private set;} = 0f;
    public float cooldownLeaveBattleTimer {get;set;} = 0;
    public float timeToLeaveBattle = 4f;
    // boolen value
    public bool PlayerInAttackRange {get; set;}
    public bool isCanAttack {get; set;} = true;
    public bool isInBattle {get; set;} = false;
    public bool isSetBattleCoolDown{get;set;} = false;

    public Transform eyePosition;

    
    [Header("Enemy stats")]
    public Transform playerReference;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float g_chaseSpeedMultiplier = 1.5f;
    [Range(0.1f,5f)]
    public float attackRange;
    public float attackRate = 1f;

    // states
    public float g_ChaseSpeed {get; private set;} 
    public Enemy_Move move {get; private set;}
    public Enemy_Idle idle {get; private set;}
    public EnemyChase chase {get; private set;}
    public EnemyAttack attack {get; private set;}
    public Enemy_FallBack fallBack {get;private set;}

    public bool isPlayerDetected {get; set;}
    public Vector2 moveDirection {get; set;}

    protected override void Awake()
    {
        base.Awake();
        SetInitialState(); 

        g_ChaseSpeed = g_moveSpeed * g_chaseSpeedMultiplier;  
    }
    private void SetInitialState()
    {
        idle = new Enemy_Idle(mStateMachine, "Idle", this);
        move = new Enemy_Move(mStateMachine, "Walk", this);
        chase = new EnemyChase(mStateMachine, "Walk", this);
        attack = new EnemyAttack(mStateMachine, "Attack", this);
        fallBack = new Enemy_FallBack(mStateMachine,"FallBack",this);

        
        mStateMachine.SetInitialState(move);
    }
    protected override void Update()
    {
        base.Update();
        CheckInBattle();
        UpdateTimer();
        UpdateBattleTimer();
    }

    private void CheckInBattle()
    {
        if(cooldownLeaveBattleTimer != 0) return;
        if(isInBattle && DoCheckPlayerInRange() == false && cooldownLeaveBattleTimer <= 0)
        {
            // cooldown battle
            SetBattleCoolDown(timeToLeaveBattle);
            isPlayerDetected = true;
            isSetBattleCoolDown = true;
            return;
        }
        isPlayerDetected = DoCheckPlayerInRange();
    }
    private bool DoCheckPlayerInRange()
    {
        Vector2 origin = this.transform.position;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction,detectionRange, playerLayer);

        return hit;
    }
    private void SetBattleCoolDown(float cooldown)
    {
        cooldownLeaveBattleTimer = cooldown;
    }
    private void UpdateBattleTimer()
    {
        if(!isSetBattleCoolDown) return;
        if(cooldownLeaveBattleTimer <= 0) 
        {
            cooldownLeaveBattleTimer = 0;
            isSetBattleCoolDown = false;
            isInBattle = false;
            return;
        }
        cooldownLeaveBattleTimer -= Time.deltaTime;
    }

    protected override bool GroundCheck()
    {
        Vector2 originPos = (this.facingDirection.x > 0)
                            ? (Vector2)transform.position + new Vector2(offsetGroundCheck,0)
                            : (Vector2)transform.position - new Vector2(offsetGroundCheck,0);
        
        onGround = Physics2D.Raycast(originPos,Vector2.down,this.groundCheckDistance,groundLayer);
        return onGround;
    }

    protected override void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        Vector2 originPos = (this.facingDirection.x > 0)
                            ? (Vector2)transform.position + new Vector2(offsetGroundCheck,0)
                            : (Vector2)transform.position - new Vector2(offsetGroundCheck,0);
        // draw check ground
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originPos, originPos - new Vector2(0,groundCheckDistance));
        // draw check wall
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + new Vector2(wallCheckDistance,0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3)(facingDirection * detectionRange));

        Gizmos.DrawSphere(eyePosition.position, 0.1f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        mStateMachine.currentState.OnTriggerEnterS(collision);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        mStateMachine.currentState.OnTriggerExitS(collision);
    }

    public void SetTimer(float time) => this.timer = time;
    private void UpdateTimer()
    {
        if(timer <= 0) return;
        timer -= Time.deltaTime;
    }

    // các hàm dùng trong object con
    public void OnPlayerInAttackRange()
    {
        PlayerInAttackRange = true;
    }
    public void OnPlayerOutAttackRange()
    {
        PlayerInAttackRange = false;
    }

}
