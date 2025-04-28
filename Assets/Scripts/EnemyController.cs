using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 moveDirection;

    public float boundMargin = 1f; // ”ÍˆÍ

    private Vector2 minVisible;
    private Vector2 maxVisible;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float timeInMarginZone = 0f;
    private float destroyTime = 3f;

    private bool isAttached = false;
    private bool isBadEnemy = false;

    public string enemyShape;

    void Start()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        SetMoveBoundsFromCamera();

        if (CompareTag("BadEnemy"))
        {
            isBadEnemy = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached) return;

        if(isBadEnemy)
        {
            transform.Rotate(0,0, -100 * Time.deltaTime);
        }

        Vector3 newPos = transform.position + (Vector3)(moveDirection * moveSpeed * Time.deltaTime);

        if (newPos.x < minBounds.x || newPos.x > maxBounds.x)
        {
            moveDirection.x *= -1;
            newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        }
        if (newPos.y < minBounds.y || newPos.y > maxBounds.y)
        {
            moveDirection.y *= -1;
            newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);
        }

        transform.position = newPos;

        if (IsInsideVisibleBounds(transform.position))
        {
            timeInMarginZone = 0f;
        }
        else
        {
            timeInMarginZone += Time.deltaTime;

            if (timeInMarginZone >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void SetMoveBoundsFromCamera()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector3 camPos = cam.transform.position;

        minVisible = new Vector2(camPos.x - width / 2f, camPos.y - height / 2f);
        maxVisible = new Vector2(camPos.x + width / 2f, camPos.y + height / 2f);

        minBounds = new Vector2(minVisible.x - boundMargin, minVisible.y - boundMargin);
        maxBounds = new Vector2(maxVisible.x + boundMargin, maxVisible.y + boundMargin);
    }

    bool IsInsideVisibleBounds(Vector3 pos)
    {
        return pos.x >= minVisible.x && pos.x <= maxVisible.x &&
               pos.y >= minVisible.y && pos.y <= maxVisible.y;
    }

    public void AttachToPlayer(Transform player)
    {
        isAttached = true;
        transform.SetParent(player);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
}
