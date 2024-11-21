using UnityEngine;

public class UnitObj : MonoBehaviour
{
    public float    moveSpeed;
    public float    maxDelay;
    private float   moveDelay;
    private Vector2 moveDir;
    [SerializeField] private Rigidbody2D rigidbody2D;

    private void Update()
    {
        moveDelay -= Time.deltaTime;
        if (moveDelay <= 0)
        {
            //이동 방향 변경
            moveDelay = UnityEngine.Random.Range(maxDelay / 2f, maxDelay);

            var quaternion = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
            Vector2 newDirection = quaternion * Vector2.right;

            moveDir = newDirection;
        }

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (moveDir.x < 0 ? -1 : +1), transform.localScale.y, transform.localScale.z);
        rigidbody2D.linearVelocity = new Vector3(moveDir.x, moveDir.y) * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            moveDir = -moveDir;
    }
}
