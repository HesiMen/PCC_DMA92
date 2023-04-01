using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    public int bulletPoolAmmount = 15;
    public List<Bullet> allBullets = new List<Bullet>();
    public Transform spawnPointBullets;
    public Transform bulletParent;
    private void Start()
    {
        for (int i = 0; i < bulletPoolAmmount; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletParent);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            allBullets.Add(bullet);
            newBullet.SetActive(false);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        foreach (var bullet in allBullets)
        {
            if (!bullet.gameObject.activeSelf)
            {
                UseBullet(bullet);
                return;
            }
        }
    }


    private void UseBullet(Bullet bullet)
    {
        bullet.transform.position = spawnPointBullets.position;
        bullet.gameObject.SetActive(true);
        bullet.rb.velocity = this.transform.forward * bullet.speed;
    }
}
