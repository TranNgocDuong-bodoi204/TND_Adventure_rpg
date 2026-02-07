using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D rb;

    private Coroutine fallBackRoutine;
    private float timeFallBack = 0.8f;        // Thời gian bay fallback (không bị control)
    private float timeStun = 0.4f;             // Thời gian stun thêm (không di chuyển được) - tùy chỉnh nếu muốn
    private bool canFallBack = true;

    public PlayerStatContainer statContainer;

    private bool isPressTest = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [ContextMenu("FallBackTest")]
    public void FallBackTest()
    {
        if (canFallBack)
        {
            StartCoroutine(FallBackCoroutine());
        }
    }

    private IEnumerator FallBackCoroutine()
    {
        canFallBack = false;

        // Áp dụng knockback: đẩy ngược hướng mặt + nhảy lên
        Vector2 directionFallBack = new Vector2(-transform.right.x * 5f, 5f); // Bạn có thể chỉnh force ở đây
        SetVelocity(directionFallBack); // Giả sử SetVelocity set rb2d.velocity = ...

        // Thời gian bay tự do (gravity vẫn ảnh hưởng)
        yield return new WaitForSeconds(timeFallBack);

        // Optional: Dừng ngang nếu muốn character "hạ cánh" mượt (giữ y velocity để rơi tự nhiên)
        SetVelocity(new Vector2(0f, rb.linearVelocity.y)); // Chỉ dừng trục X

        // Nếu muốn stun thêm (không cho input di chuyển)
        yield return new WaitForSeconds(timeStun);

        // Reset sẵn sàng dùng lại
        canFallBack = true;
    }

    private void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;

    }

    [ContextMenu("TestVelocity")]
    private void TestVelocity()
    {
        if(!isPressTest)
        {
            isPressTest = true;
            SetVelocity(new Vector2(5f, 5f));
        }
    }
    [ContextMenu("ResetTest")]
    private void ResetTest()
    {
        isPressTest = false;
    }
}

