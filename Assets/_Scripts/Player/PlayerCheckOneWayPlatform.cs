using System.Collections;
using UnityEngine;

public class PlayerCheckOneWayPlatform : MonoBehaviour
{
    [Header("Check Settings")]
    [SerializeField] private LayerMask oneWayPlatformLayer;
    [SerializeField] private float offsetY = 0.05f;
    [SerializeField] private float rayLength = 0.1f;

    [Header("Drop Settings")]
    [SerializeField] private float dropDuration = 0.25f;

    private CapsuleCollider2D csCollider;
    private PlayerContext playerContext;
    private bool dropping = false;
    private Collider2D currentPlatform;

    public Vector2 checkPosition;

    private void Awake()
    {
        csCollider = GetComponent<CapsuleCollider2D>();
        playerContext = GetComponent<PlayerContext>();
    }

    void Update()
    {
        HandleDropDown();
    }

    private void HandleDropDown()
    {
        if (dropping) return;

        RaycastHit2D hit = CheckStandingOnPlatform();

        if (hit)
        {
            if(playerContext.activeGroundCheck == true)
            {
                playerContext.activeGroundCheck = false;
            }
            playerContext.onGround = true;
            currentPlatform = hit.collider;

            if (playerContext.g_moveInput.y < 0)
            {
                StartCoroutine(DropCoroutine(currentPlatform));
            }
        }
        else
        {
            if(playerContext.activeGroundCheck == false)
                playerContext.activeGroundCheck = true;
            currentPlatform = null;
        }
    }

    IEnumerator DropCoroutine(Collider2D platform)
    {
        dropping = true;
        Physics2D.IgnoreCollision(csCollider, platform, true);
        playerContext.activeGroundCheck = false;
        playerContext.onGround = false;

        yield return new WaitForSeconds(dropDuration);

        Physics2D.IgnoreCollision(csCollider, platform, false);
        dropping = false;
        playerContext.activeGroundCheck = true;
    }

    private RaycastHit2D CheckStandingOnPlatform()
    {
        float bottomY = csCollider.bounds.min.y - offsetY;
        float centerX = csCollider.bounds.center.x;

        checkPosition = new Vector2(centerX, bottomY);

        return Physics2D.Raycast(
            checkPosition,
            Vector2.down,
            rayLength,
            oneWayPlatformLayer
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(checkPosition, 0.05f);
    }
}
