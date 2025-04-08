using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    protected Animator anim;
    protected Rigidbody2D rb;
    #endregion

    #region Ground
    [Header ("Ground")]
    protected bool isGrounded;
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected float groundCheckDistance;
    [SerializeField]
    protected LayerMask whatIsGround;
    #endregion
    #region Wall
    [Header ("Wall")]
    protected bool isWallDetected;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected float wallCheckDistance;
    [SerializeField]
    protected LayerMask whatIsWall;
    #endregion
    
    #region Flip
    protected int facingDir = 1;
    protected bool facingRight = true;

    #endregion

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        CheckColl();
    }

    protected virtual void CheckColl()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsWall);
    }

    protected virtual void Flip()
    {
        facingDir = -facingDir;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
}
