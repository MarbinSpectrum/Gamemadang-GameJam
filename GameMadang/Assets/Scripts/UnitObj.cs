using UnityEngine;

public class UnitObj : MonoBehaviour
{
    public float    moveSpeed;
    public float    maxDelay;
    private float   moveDelay;
    private Vector2 moveDir;
    private int unitKey;
    private int gameSeed;
    private int time = 0;
    private bool init = false;
    private Vector3 defaultScale;
    [SerializeField] private Rigidbody2D rigidbody2D;

    public void SetUnit(int pUnitKey, int pGameSeed)
    {
        unitKey = pUnitKey;
        gameSeed = pGameSeed;
        time = 0;

        if (init == false)
        {
            init = true;
            defaultScale = transform.localScale;
        }

        GetComponent<SpriteRenderer>().sortingOrder = 0;
        transform.localScale = defaultScale;
    }

    private void FixedUpdate()
    {
        time++;

        moveDelay -= Time.fixedDeltaTime;
        if (moveDelay <= 0)
        {
            //이동 방향 변경
            SetSeed();
            moveDelay = UnityEngine.Random.Range(maxDelay / 2f, maxDelay);

            SetSeed();
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

    private void SetSeed()
    {
        UnityEngine.Random.InitState(gameSeed + unitKey + time);
    }
}
