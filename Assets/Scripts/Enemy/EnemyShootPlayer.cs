using UnityEngine;

public class EnemyShootPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public static float BulletSpeed;

    const float MinInterval = 0.05f;
    public static float MaxInterval;

    float currentInterval;
    float timeSinceLastShot = -1;

    private Transform player;
    private SoundManager soundManager;

    public Animator mainAnimator;
    public Animator muzzleAnimator;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
	}
	
	void Update ()
    {
        if (player != null)
        {
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= currentInterval)
            {
                {
                    Shoot();
                    timeSinceLastShot = 0;
                    currentInterval = Random.Range(MinInterval, MaxInterval);
                    muzzleAnimator.Play("Muzzle");
                }
            }

            transform.rotation = Quaternion.FromToRotation(Vector3.up, GetDirectionToPlayer());
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = GetDirectionToPlayer() * BulletSpeed;
        soundManager.Play("EnemyShot");
    }

    Vector2 GetDirectionToPlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        return direction;
    }


}