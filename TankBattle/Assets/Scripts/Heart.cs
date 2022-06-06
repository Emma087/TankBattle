using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr; //获取当前的默认图片
    public Sprite BrokenSprite; //被破坏后的图片资源

    public GameObject explosionPrefab;
    public AudioClip dieAudio;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 心脏破坏函数
    /// </summary>
    public void DieHeart()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation); //爆炸特效
        sr.sprite = BrokenSprite; //切换图片
        PlayerManager.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio,transform.position);
    }
}