using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public int maxAmmo = 50;
    private int currentAmmo;
    private float nextShotTime = 0f;
    private GameUI gameUI;

    void Start()
    {
        currentAmmo = maxAmmo;
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI != null)
        {
            gameUI.UpdateAmmo(currentAmmo);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time >= nextShotTime)
        {
            ShootProjectile();
            nextShotTime = Time.time + 0.2f;
        }
    }

    void ShootProjectile()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.useGravity = false;
            rigidbody.AddForce(Vector3.up * shootForce, ForceMode.Impulse);
        }

        currentAmmo--;
        if (gameUI != null)
        {
            gameUI.UpdateAmmo(currentAmmo);
        }
    }
}
