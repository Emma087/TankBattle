using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; //主角速度
    private SpriteRenderer plyerImage; //主角图片
    public Sprite[] TankSprites; //主角上下左右的图片组
    public GameObject bullet1;
    private Vector3 bulletAulerAngles; //子弹每次要转的一个度数
    private float timeWait; //实例化子弹的时间间隔
    public GameObject explosionPrefab; //爆炸特效
    private bool isDefended = true; //主角的无敌状态
    private float timeDefend = 3f; //主角的无敌时间
    public GameObject defendEffect; //无敌的特效

    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    void Start()
    {
        plyerImage = GetComponent<SpriteRenderer>();
        // timeWait = 0.3f;
    }

    /// <summary>
    /// 坦克的攻击方法，包括发射子弹
    /// </summary>
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(123);
            GameObject.Instantiate(bullet1, transform.position,
                Quaternion.Euler(transform.eulerAngles + bulletAulerAngles)); //实例化出来子弹预制体
            timeWait = 0;
        }
    }

    /// <summary>
    /// 键盘控制主角移动
    /// </summary>
    void KeyboardMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 && v == 0 || v != 0 && h == 0)
        {
            transform.Translate(new Vector3(h, v, 0) * Time.fixedDeltaTime * moveSpeed);
            if (h < 0) //面朝右
            {
                plyerImage.sprite = TankSprites[3];
                bulletAulerAngles = new Vector3(0, 0, 90);
            }
            else if (h > 0) //面朝左
            {
                plyerImage.sprite = TankSprites[1];
                bulletAulerAngles = new Vector3(0, 0, -90);
            }
            else if (v < 0) //面朝下
            {
                plyerImage.sprite = TankSprites[2];
                bulletAulerAngles = new Vector3(0, 0, -180);
            }
            else if (v > 0) //面朝上
            {
                plyerImage.sprite = TankSprites[0];
                bulletAulerAngles = new Vector3(0, 0, 0);
            }
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    /// <summary>
    /// 主角的死亡触发
    /// </summary>
    void Die()
    {
        if (isDefended)
        {
            return;
        }

        PlayerManager.Instance.isDead = true;

        Instantiate(explosionPrefab, transform.position, transform.rotation); //爆炸特效
        Destroy(gameObject); //死亡
    }

    private void Update()
    {
        if (isDefended) //如果是无敌
        {
            defendEffect.SetActive(true);
            timeDefend -= Time.deltaTime;
            if (timeDefend <= 0)
            {
                isDefended = false;
                defendEffect.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }

        KeyboardMove();

        if (timeWait >= 0.3f)
        {
            Attack();
        }
        else
        {
            timeWait += Time.fixedDeltaTime;
        }
    }
}