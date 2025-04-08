using UnityEngine;

public class Enemy_Skeleton : Entity
{
    const float ATTACK_RANGE = 1f;
    bool isAttacking;

    #region Movement
    [Header ("이동 정보")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float chaseSpeed;
    #endregion

    #region Player Detection
    [Header ("Player Detection")]
    [SerializeField]
    private float playerCheckDistance;
    [SerializeField]
    private LayerMask whatIsPlayer;
    private RaycastHit2D isPlayerDetected;
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(isPlayerDetected)
        {
            if(isPlayerDetected.distance > ATTACK_RANGE)
            {
                // 추적
                chaseSpeed = 1.5f;

                Debug.Log("플레이어를 봤다.");
                isAttacking = false;
            }
            else
            {
                Debug.Log($"공격 : {isPlayerDetected.collider.gameObject.name}");
                isAttacking = true;
            }
        }
        else
        {
            chaseSpeed = 1f;
        }

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }

        Movement();
    }

    private void Movement()
    {
        if (!isAttacking)
        {
            rb.linearVelocity = new Vector2 (moveSpeed * chaseSpeed * facingDir, rb.linearVelocity.y);
        }
    }

    protected override void CheckColl()
    {
        base.CheckColl();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}
