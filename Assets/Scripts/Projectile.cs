using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody2D body;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        body.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
