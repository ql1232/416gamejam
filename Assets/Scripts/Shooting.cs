using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab name is bullet
    public float shootForce = 10f;
    public int maxAmmo = 50;
    private int currentAmmo;
    private float nextShotTime = 0f;

    public float lifespan = 0.5f; // Changeable lifespan of bullets

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time >= nextShotTime)
        {
            ShootBullet();
            nextShotTime = Time.time + 0.2f;
        }
    }

    void ShootBullet()
    {
        // Define where the bullet will spawn (directly above the player)
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        // Instantiate the bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Add upward force to the bullet
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.useGravity = false; // No gravity for the bullet
            rigidbody.AddForce(Vector3.up * shootForce, ForceMode.Impulse);
        }

        // Destroy the bullet after the specified lifespan
        Destroy(bullet, lifespan);

        // Decrease ammo after shooting
        currentAmmo--;
    }
}
