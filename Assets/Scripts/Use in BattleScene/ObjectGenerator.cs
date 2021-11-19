using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject[] playerItems = new GameObject[6];

    public static int killcount = 0;

    private void Start()
    {
        //for (int i = 1; i < playerItems.Length; i++)
        //{
        //    // InGameShopManager�� ���������� DontDestroyOnLoad�� �����Ѵٸ�, 
        //    if (InGameShopManager.instance != null)
        //    {
        //        // playerItems�� sprite��, ���ŵ� sprite�� index ���� ������ ����.
        //        playerItems[i].GetComponent<Image>().sprite = InGameShopManager.instance.iL.ISprite[InGameShopManager.instance.HaveItemSpriteNumber[i]];
        //        // �ʱ���´� ���� ��Ȱ���̹Ƿ�, sprite�� ����� gameobject�� Ȱ��ȭ.
        //        if (playerItems[i].GetComponent<Image>().sprite != null)
        //        {
        //            playerItems[i].SetActive(true);
        //        }
        //    }
        //}
        
        List<string> HaveItemNumber = InGameShopManager.HaveItemSpriteNumber2;
        for (int i = 1; i < HaveItemNumber.Count; i++)
        {
            Debug.Log("���õ� ������ �ѹ� : " + PlayerPrefs.GetInt(HaveItemNumber[i]));
            playerItems[i].GetComponent<Image>().sprite = GetComponent<ItemsList>().ISprite[PlayerPrefs.GetInt(HaveItemNumber[i])];
            GetComponent<ItemsList>().ItemAbility(PlayerPrefs.GetInt(HaveItemNumber[i]));
        }
        SpawnEnemy();
        Invoke("SpawnEnemy", 2);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(player);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Instantiate(enemy);
        }
        if (killcount >= 2 && SceneManager.GetActiveScene().name!="Battle")
        {            
            Invoke("InGameShopLoad", 2);
            killcount = 0;
        }
        else if (killcount >= 2 && SceneManager.GetActiveScene().name == "Battle")
        {
            Invoke("MainLoad", 2);
            killcount = 0;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy);
    }

    void InGameShopLoad()
    {
        SceneManager.LoadScene("InGameStore");
    }

    void MainLoad()
    {
        SceneManager.LoadScene("Main");
    }
}