using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float angle;
    public float bulletSpeed = 20f;
    public SpriteRenderer gunSprite;
    public GameObject bullet;
    public Transform bulletSpawnPosRight;
    public Transform bulletSpawnPosLeft;
    public AudioSource audioSource;
    public AudioClip shoot;

    private void Update()
    {
        //MoveGun
        Vector3 mousePos = Input.mousePosition;

        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= objPos.x;
        mousePos.y -= objPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle > 90f || angle < -90f)
        {
            gunSprite.flipY = true;
        }
        else
        {
            gunSprite.flipY = false;
        }


        //Shoot
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bull;
            if (angle > 90f || angle < -90f)
            {
                bull = Instantiate(bullet, bulletSpawnPosLeft.position, transform.rotation);
                Rigidbody2D bulletRb = bull.GetComponent<Rigidbody2D>();
                bulletRb.velocity = bulletSpawnPosLeft.right * bulletSpeed;
            }
            else
            {
                bull = Instantiate(bullet, bulletSpawnPosRight.position, transform.rotation);
                Rigidbody2D bulletRb = bull.GetComponent<Rigidbody2D>();
                bulletRb.velocity = bulletSpawnPosRight.right * bulletSpeed;
            }
            PlayAudio(shoot);
            
            
        }
    }

    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
