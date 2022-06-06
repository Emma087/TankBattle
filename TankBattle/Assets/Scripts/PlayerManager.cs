using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //玩家的属性值
    public int lifeValue = 3; //玩家的血量
    public int playerScore; //总分
    public bool isDead; //玩家是否死亡
    public bool isDefeat; //玩家是不是失败了

    public GameObject born;
    public Text playerScoreText; //玩家分数字
    public Text PlayerLifeValueText; //玩家命 数字
    public GameObject isDefeatUi;

    //单例
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private void Awake()
    {
        _instance = this;
    }

    void RecoverLife()
    {
        if (lifeValue < 0)
        {
            //游戏失败了，返回主界面
            isDefeat = true;
            Invoke("RetrunToMainMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<BornEffect>().createrPlayer = true;
            isDead = false;
        }
    }

    void RetrunToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (isDefeat)
        {
            isDefeatUi.SetActive(true);
            Invoke("RetrunToMainMenu", 3);
            return;
        }

        if (isDead)
        {
            RecoverLife();
        }

        playerScoreText.text = playerScore.ToString();
        PlayerLifeValueText.text = lifeValue.ToString();
    }
}