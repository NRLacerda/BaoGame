using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float firePointOffsetX;
    [SerializeField] private float firePointOffsetY;

    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool attackPressed; 

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }


    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (playerMovement.CanAttack() && cooldownTimer > attackCooldown)
        {
            if (attackPressed)
            {
                Shoot();
                cooldownTimer = 0f;
                attackPressed = false;
            }
        }
    }

    private void Shoot()
    {
        Vector3 spawnPosition = transform.position + new Vector3(
            firePointOffsetX * playerMovement.FacingDirection,
            firePointOffsetY,
            0f
        );

        GameObject projectileObject = Instantiate(
            projectilePrefab,
            spawnPosition,
            Quaternion.identity
        );

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        
        Vector2 direction = playerMovement.FacingDirection == 1 ? Vector2.right : Vector2.left;
        projectile.Launch(direction);
    }
}
