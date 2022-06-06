using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public bool isPlayerBullet; //是主角发射的子弹吗

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Tank":
                if (!isPlayerBullet) //如果不是主角发的，就打死主角
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (isPlayerBullet) //如果是主角发的，就打死敌人
                {
                    other.SendMessage("EnemyDie");
                    Destroy(gameObject);
                }
                break;

            case "Heart":
                other.SendMessage("DieHeart");
                Destroy(gameObject); //然后也消灭子弹自己
                break;

            case "Wall":
                Destroy(other.gameObject); //碰到土墙，消灭土墙
                Destroy(gameObject); //然后也消灭子弹自己
                break;
            case "MetalWall":
                other.SendMessage("PlayAudio");
                Destroy(gameObject); //然后也消灭子弹自己
                break;
            case "Bullet":
                Destroy(gameObject);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate
            (transform.up * bulletSpeed * Time.deltaTime, Space.World);
    }
}