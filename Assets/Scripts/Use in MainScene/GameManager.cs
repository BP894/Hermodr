﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject mainHero;

    void Start()
    {        
        // 플레이어가 가진 아이템 수 초기화
        for (int i = 0; i < InGameShopManager.HaveItemSpriteNumber2.Count; i++)
        {
            PlayerPrefs.DeleteKey(InGameShopManager.HaveItemSpriteNumber2[i]);
            PlayerPrefs.SetInt(InGameShopManager.HaveItemSpriteNumber2[i], 0);
        }
        // 플레이어가 가진 아이템의 강화수치 초기화
        for (int i = 0; i < InGameShopManager.HaveItemForgeNumber.Count; i++)
        {
            PlayerPrefs.DeleteKey(InGameShopManager.HaveItemForgeNumber[i]);
        }
        // 메인 영웅 설정
        string heroSpritePath = PlayerPrefs.GetString("Main Hero");
        if(heroSpritePath != "")
        {
            int lastindex = heroSpritePath.LastIndexOf('.');
            string a1 = heroSpritePath.Substring(0, lastindex);
            string ResourceSpritePath = a1.Substring(17);
            mainHero.GetComponent<Image>().sprite = Resources.Load<Sprite>(ResourceSpritePath);
        }
    }
    public void Divine()
    {
        SceneManager.LoadScene("LobbyStore");
    }
}