using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10;
    public float lifeSeconds = 4f;
    private float currSeconds;
    private void OnEnable()
    {
        StartCoroutine(LifeBulletRoutine());
    }

    IEnumerator LifeBulletRoutine()
    {
        currSeconds = lifeSeconds;
        while (gameObject.activeSelf)
        {
            currSeconds -= Time.deltaTime;
            if (currSeconds < 0)
                ResetBullet();

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ResetBullet();
    }

    private void ResetBullet()
    {
        gameObject.SetActive(false);
        currSeconds = lifeSeconds;
    }
}
