using System.Collections;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    public float moveDistance; // Tile size
    public float moveSpeed;
    public Transform move_point;
    public LayerMask stop_movement, BOX;
    public Sprite sprite_normal_front, sprite_normal_back, sprite_normal_left, sprite_normal_right;
    public Sprite sprite_diving_front, sprite_diving_back, sprite_diving_left, sprite_diving_right;
    public bool is_diving;
    public GameObject sprite_obj;

    private bool isMoving = false;

    void Start()
    {
        is_diving = false;
        // move_point.position = SnapToGrid(transform.position);
        transform.position = move_point.position;
        move_point.parent = null;
        moveDistance = 0.64f;
        moveSpeed = 5.0f;
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                StartCoroutine(Move(Vector3.up));

            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                StartCoroutine(Move(Vector3.down));

            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                StartCoroutine(Move(Vector3.left));

            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                StartCoroutine(Move(Vector3.right));
        }
    }

    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        
        Vector3 targetPosition = move_point.position + direction * moveDistance;
        // targetPosition = SnapToGrid(targetPosition);

        // Check if the tile is free
        if (!Physics2D.OverlapCircle(targetPosition, 0.15f, stop_movement) &&
            !Physics2D.OverlapCircle(targetPosition, 0.15f, BOX))
        {
            move_point.position = targetPosition;
            UpdateSprite(direction);
            
            // Move the player over time to create a step-by-step effect
            while (Vector3.Distance(transform.position, move_point.position) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, move_point.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = move_point.position; // Ensure final position is exact
            yield return new WaitForSeconds(0.05f); // Pause before allowing another move
        }

        isMoving = false;
    }

    // Vector3 SnapToGrid(Vector3 position)
    // {
    //     float snappedX = Mathf.Round(position.x / moveDistance) * moveDistance;
    //     float snappedY = Mathf.Round(position.y / moveDistance) * moveDistance;
    //     return new Vector3(snappedX, snappedY, position.z);
    // }

    void UpdateSprite(Vector3 direction)
    {
        if (is_diving)
        {
            if (direction == Vector3.right) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_diving_right;
            else if (direction == Vector3.left) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_diving_left;
            else if (direction == Vector3.up) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_diving_back;
            else if (direction == Vector3.down) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_diving_front;
        }
        else
        {
            if (direction == Vector3.right) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_normal_right;
            else if (direction == Vector3.left) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_normal_left;
            else if (direction == Vector3.up) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_normal_back;
            else if (direction == Vector3.down) sprite_obj.GetComponent<SpriteRenderer>().sprite = sprite_normal_front;
        }
    }
}
