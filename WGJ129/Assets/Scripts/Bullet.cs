using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particleSys;
    public GameObject bloodSplatter;
    public GameObject particleZombie;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Instantiate(particleSys, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Zombie")
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
            Instantiate(particleZombie, transform.position, Quaternion.identity);

            Zombie zombie = collision.GetComponent<Zombie>();
            zombie.HitByBullet(transform);

            Destroy(this.gameObject);
        }
    }

}
