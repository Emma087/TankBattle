using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapCreation : MonoBehaviour
{
    public GameObject[] item; //用来装饰初始化地图所需物体的数组
    // 0老家，1墙，2障碍，3出生效果，4河流，5草，6空气墙

    private List<Vector3> itemPositionList = new List<Vector3>(); //此列表存放，已经有物体的位置

    private void Awake()
    {
        CreateMap();
    }

    /// <summary>
    /// 实例化地图上所有的东西，的函数，包括初始一波敌人
    /// </summary>
    void CreateMap()
    {
        //这坐标是测好的老家的位置
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        //实例化地图，并且全是随机产生的
        for (int i = 0; i < 40; i++)
        {
            // item[1]，土墙
            CreateItem(item[1], CreateRandomPosition(), quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            // item[2]，铁墙
            CreateItem(item[2], CreateRandomPosition(), quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            // item[4]，河流
            CreateItem(item[4], CreateRandomPosition(), quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            // item[1]，草丛
            CreateItem(item[5], CreateRandomPosition(), quaternion.identity);
        }

        // 初始化玩家  
        GameObject go =
            Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<BornEffect>().createrPlayer = true;

        // 实例化第一波敌人
        CreateItem(item[3], new Vector3(-10, 8, 10), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

        InvokeRepeating("CreateEnemy", 4, 5);
    }

    /// <summary>
    /// 实例化敌人，三个位置
    /// </summary>
    void CreateEnemy()
    {
        int number = Random.Range(0, 3);
        Vector3 enemyPosition = new Vector3();
        if (number == 0)
        {
            enemyPosition = new Vector3(-10, 8, 10);
        }
        else if (number == 1)
        {
            enemyPosition = new Vector3(0, 8, 0);
        }
        else
        {
            enemyPosition = new Vector3(10, 8, 0);
        }

        CreateItem(item[3], enemyPosition, Quaternion.identity);
    }

    /// <summary>
    /// 生成地图的函数
    /// </summary>
    /// <param name="createGameObject">生成哪一个物体</param>
    /// <param name="createPosition">生成物体的位置</param>
    /// <param name="createRotation">旋转信息</param>
    void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        //这里相当于把本脚本的物体当成父物体，实例化的都放父物体下面
        itemPositionList.Add(createPosition);
    }

    /// <summary>
    /// 产生随机敌人的位置，的函数
    /// </summary>
    /// <returns>返回一个新生成，并且不重复，可以用的坐标</returns>
    Vector3 CreateRandomPosition()
    {
        while (true) //这个死循环存在的必要性就是，因为不满足条件的情况下，有可能不走 if，所以要写死循环
        {
            Vector3 createPosition =
                new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    /// <summary>
    /// 查看地图中，新生成的哪个位置，是否已经在 itemPositionList中有这个重复的位置了
    /// </summary>
    /// <param name="createPisition">新生成的那个位置</param>
    /// <returns>是代表重复/否代表可以用</returns>
    bool HasThePosition(Vector3 createPisition)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPisition == itemPositionList[i])
            {
                return true;
            }
        }

        return false;
    }
}