using UnityEngine;

public class Player : MonoBehaviour
{
    #region Movement
    [Header("이동 정보")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [SerializeField] private float dashCooldown;
    private float dashTimer;
    public float dashSpeed = 14f;
    public float dashDuration = 0.4f;
    public float dashDir { get; private set; }
    #endregion

    #region Collision
    [Header("충돌 정보")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    #endregion

    #region Animation State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion

    #region Components
    public Animator anim { get; private set; }
    [HideInInspector] public Rigidbody2D rb;
    #endregion

    #region Flip
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    #endregion
    

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    public void AnimationTrigger()
    => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        dashTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCooldown;   
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            { 
                dashDir = facingDir; 
            }
            stateMachine.ChangeState(dashState);
        }
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected()
    => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallDetected()
    => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y, 0));
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float paramX)
    {
        if (paramX > 0 && !facingRight)
        {
            Flip();
        }
        else if (paramX < 0 && facingRight)
        {
            Flip();
        }
    }
}


// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     // 플레이어의 상태를 관리하는 상태 머신
//     public PlayerStateMachine stateMachine { get; private set; }

//     // 플레이어의 상태 (대기 상태, 이동 상태)
//     public PlayerIdleState idleState { get; private set; }
//     public PlayerMoveState moveState { get; private set; }

//     private void Awake()
//     {
//         // 상태 머신 인스턴스 생성
//         stateMachine = new PlayerStateMachine();

//         // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
//         idleState = new PlayerIdleState(this, stateMachine, "Idle");
//         moveState = new PlayerMoveState(this, stateMachine, "Move");
//     }

//     private void Start()
//     {
//         // 게임 시작 시 초기 상태를 대기 상태(idleState)로 설정
//         stateMachine.Initialize(idleState);
//     }
// }
