using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        transform.DOShakeRotation(lifeSeconds);
        transform.localScale = Vector3.one * .073f;
        transform.DOShakeScale(lifeSeconds);
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
