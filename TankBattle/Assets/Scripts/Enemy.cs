using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float moveSpeed; //主角速度
    private SpriteRenderer plyerImage; //主角图片
    public Sprite[] TankSprites; //主角上下左右的图片组
    public GameObject bullet;
    private Vector3 bulletAulerAngles; //子弹每次要转的一个度数
    private float timeWait; //实例化子弹的时间间隔
    public GameObject explosionPrefab; //爆炸特效
    private float changeDirectionTime = 2; //改变方向的等待时间
    private float h; //代表横轴方向
    private float v; //代表竖轴方向


    void Start()
    {
        plyerImage = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 敌人坦克产生子弹
    /// </summary>
    void Attack()
    {
        Instantiate(bullet, transform.position,
            Quaternion.Euler(transform.eulerAngles + bulletAulerAngles));
        timeWait = 0;
    }

    /// <summary>
    /// 敌人坦克的随机移动
    /// </summary>
    void KeyboardMove()
    {
        if (changeDirectionTime >= 1.5f)
        {
            int number = Random.Range(0, 8); //这个随即数，是为了让敌人朝下走的机会更多点，给它掏家的机会
      
            if (number > 5)
            {
                v = -1; //往下走
                h = 0;
            }
            else if (number == 0)
            {
                v = 1; //往上走
                h = 0;
            }
            else if (number > 0 && number <= 2)
            {
                h = -1; //往左走
                v = 0;
            }
            else if (number > 2 && number <= 4)
            {
                h = 1; //往右走
                v = 0;
            }
            changeDirectionTime = 0;
        }
        else
        {
            changeDirectionTime += Time.fixedDeltaTime;
        }

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
        }
    }


    void CollisionTurn()
    {
        int number = Random.Range(0, 8); //这个随即数，是为了让敌人朝下走的机会更多点，给它掏家的机会
        Debug.LogWarning(number);
        if (number > 5)
        {
            v = -1; //往下走
            h = 0;
        }
        else if (number == 0)
        {
            v = 1; //往上走
            h = 0;
        }
        else if (number > 0 && number <= 2)
        {
            h = -1; //往左走
            v = 0;
        }
        else if (number > 2 && number <= 4)
        {
            h = 1; //往右走
            v = 0;
        }


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
        }
    }

    /// <summary>
    /// 这是我自己补的代码，让敌人碰到墙或者自己人，就立刻切换方向
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.collider.tag == "MetalWall"||other.collider.tag == "Enemy"||other.collider.tag =="Wall")
        if (other.collider.CompareTag("MetalWall") || other.collider.CompareTag("Enemy") ||
            other.collider.CompareTag("Wall") || other.collider.CompareTag("Tank") ||
            other.collider.CompareTag("River") || other.collider.CompareTag("AirWall"))
        {
            CollisionTurn();
        }
    }

    /// <summary>
    /// 敌人坦克的死亡触发
    /// </summary>
    void EnemyDie()
    {
        PlayerManager.Instance.playerScore += 1;
        Instantiate(explosionPrefab, transform.position, transform.rotation); //爆炸特效
        Destroy(gameObject); //死亡
    }

    private void Update()
    {
        float index = Random.Range(0.8f, 2f); //随机一个开枪时间的间隔
        if (timeWait >= index)
        {
            Attack();
        }
        else
        {
            timeWait += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        KeyboardMove();
    }
}