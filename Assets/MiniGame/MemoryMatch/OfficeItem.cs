using UnityEngine;

public class OfficeItem : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private float speedIncrease;
    private Rigidbody2D rb;

    public void Initialize(float startSpeed, float increase)
    {
        speed = startSpeed;
        speedIncrease = increase;
        direction = Random.insideUnitCircle.normalized;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().sharedMaterial = new PhysicsMaterial2D
        {
            bounciness = 0.7f,
            friction = 0.1f
        };
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb != null)
        {
            rb.AddForce(collision.contacts[0].normal * 0.1f, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            speed += speedIncrease;
        }
        else if (collision.gameObject.GetComponent<OfficeItem>() != null)
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            speed += speedIncrease * 0.5f;
        }
    }
}