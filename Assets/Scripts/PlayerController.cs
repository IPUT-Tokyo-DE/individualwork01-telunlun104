using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private float minX, maxX, minY, maxY;

    [SerializeField] private string shapeType = "circle";
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        SetMoveBoundsFromCamera();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;

        // âÊñ ì‡Ç…êßå¿
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rb.MovePosition(newPosition);
    }

    void SetMoveBoundsFromCamera()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Collider2D playerCollider = GetComponent<Collider2D>();
        float halfWidth = playerCollider.bounds.size.x / 2f;
        float halfHeight = playerCollider.bounds.size.y / 2f;

        minX = -width / 2f + halfWidth;
        maxX = width / 2f - halfWidth;
        minY = -height / 2f + halfHeight;
        maxY = height / 2f - halfHeight;
    }

    public string GetShapeType()
    {
        return shapeType;
    }
}