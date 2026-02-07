using UnityEngine;

public class EntityContext : MonoBehaviour
{
    private const int GRAVITY_SCALE = 4;

    public StateMachine mStateMachine{get;set;}
    public Rigidbody2D rb{get;set;}
    public Animator animator {get;set;}
    [Header("ground and wall detector")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;
    public bool onGround = false;
    public bool onWall = false; 

    public Vector2 facingDirection{get; private set;}
    private bool isFacingRight = true;
    public bool animTrigger {get;set;}
    public bool attackTrigger {get;set;}
    public bool attackBuffered{get;set;}

    public bool activeGroundCheck {get; set;}
    public bool activeWallCheck {get; set;}

    // các biến dùng chung
    [Header("Set entity stat")]
    public float g_moveSpeed = 3;
    // ***
    protected virtual void Awake()
    {
        this.mStateMachine = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        facingDirection = new Vector2(transform.localScale.x , 0);
    }
    protected virtual void Start()
    {
        activeGroundCheck = true;
        activeWallCheck   = true;

        rb.gravityScale = GRAVITY_SCALE;
        animTrigger = true;

    }
    protected virtual void Update()
    {
        if(mStateMachine.currentState != null) 
            mStateMachine.UpdateActiveState();
    }
    protected virtual void FixedUpdate()
    {
        if(activeGroundCheck) GroundCheck();
        if(activeWallCheck) WallCheck();
        if(mStateMachine.currentState != null) 
            mStateMachine.FixedUpdateActiveState();
    }
    protected virtual bool WallCheck()
    {
        onWall = Physics2D.Raycast(transform.position,facingDirection,wallCheckDistance,groundLayer);
        return onWall;
    }
    protected virtual bool GroundCheck()
    {
        onGround = Physics2D.Raycast((Vector2)transform.position,Vector2.down,groundCheckDistance,groundLayer);
        return onGround;
    }
    private void HandleFlipCharacter()
    {
        if(rb.linearVelocityX < 0 && isFacingRight)
        {
            facingDirection = new Vector2(-1,0);
            FlipCharacter();
        }
        else if(rb.linearVelocityX > 0 && !isFacingRight)
        {
            facingDirection = new Vector2(1,0);
            FlipCharacter();
        }
    }
    private void HandleFlipCharacter(bool forceFlip)
    {
        if(!forceFlip) return;
        if(isFacingRight)
        {
            facingDirection = new Vector2(-1,0);
            FlipCharacter();
        }
        else
        {
            facingDirection = new Vector2(1,0);
            FlipCharacter();
        }
    }
    public void FlipCharacter()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1,1);
        isFacingRight = !isFacingRight;
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlipCharacter();
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
        HandleFlipCharacter();
    }
    public void ForceFlipCharacter()
    {
        HandleFlipCharacter(true);
    }

    public void SetFacingDirection(Vector2 dir)
    {
        if(dir == Vector2.zero) return;
        facingDirection = dir.normalized;
    }

    protected virtual void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        Vector2 originPos = pos;
        // draw check ground
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originPos, originPos - new Vector2(0,groundCheckDistance));
        // draw check wall
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + facingDirection * wallCheckDistance);
    }
}
