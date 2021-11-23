using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject stageClear;
   
    public GameObject[] playerItems = new GameObject[6];
    public static int killcount = 0;

    List<string> HaveItemNumber = InGameShopManager.HaveItemSpriteNumber2;
    List<string> HaveItemForgeNumber = InGameShopManager.HaveItemForgeNumber;

    public Text timeText;
    float _sec;
    int _min;
    string timeString;

    public int totalEnemy;
    public int remainEnemy;
    private void Start()
    {
        for (int i = 1; i < HaveItemNumber.Count; i++)
        {
            Debug.Log("���õ� ������ �ѹ� : " + PlayerPrefs.GetInt(HaveItemNumber[i]));
            playerItems[i].GetComponent<Image>().sprite = GetComponent<ItemsList>().ISprite[PlayerPrefs.GetInt(HaveItemNumber[i])];
            playerItems[i].GetComponentInChildren<Text>().text = "+" + PlayerPrefs.GetInt(HaveItemForgeNumber[i]);
            playerItems[i].GetComponent<ItemsList>().ItemAbility(PlayerPrefs.GetInt(HaveItemNumber[i]), PlayerPrefs.GetInt(HaveItemForgeNumber[i]));

            int forge = int.Parse(playerItems[i].GetComponentInChildren<Text>().text.ToString().Substring(1));
            if(forge == 0)
            {
                playerItems[i].GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
            }
        }
        // �������� Ŭ����/���� UI �ʱ�ȭ
        stageClear.SetActive(false);
        //
      
        float damage = player.GetComponent<PlayerCombat>().damage;
        float attackRange = player.GetComponent<PlayerCombat>().attackRange;
        float timeBetAttack = player.GetComponent<PlayerCombat>().timeBetAttack;
        float startHealth = player.GetComponent<PlayerCombat>().startHealth;
        float moveSpeed = player.GetComponent<PlayerCombat>().moveSpeed;
        Debug.Log("�÷��̾��� ���ݷ� : " + damage + "\n" +
                    "�÷��̾��� ���ݹ��� : " + attackRange + "\n" +
                    "�÷��̾��� ���ݼӵ� : " + timeBetAttack + "\n" + 
                    "�÷��̾��� �ִ�ü�� : " + startHealth + "\n" + 
                    "�÷��̾��� �̵��ӵ� : " + moveSpeed );
        // �� ��������Ŭ���� Ƚ���� �����Ͽ� ��ȯ.
        if (enemy.name == "Hell Giant")
        {
            if (StageSelector.stageClear == 1)
            {
                totalEnemy = StageSelector.stageClear + 6;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnGiantRepeat());
            }
            else if (StageSelector.stageClear == 2)
            {
                totalEnemy = StageSelector.stageClear + 6;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnGiantRepeat());
            }
            else if (StageSelector.stageClear == 3)
            {
                totalEnemy = StageSelector.stageClear + 6;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnGiantRepeat());
            }
        }
        if(enemy.name == "Forest Wolf")
        {
            if(StageSelector.stageClear == 1)
            {
                totalEnemy = StageSelector.stageClear + 14;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnWolfRepeat());
            }
            else if (StageSelector.stageClear == 2)
            {
                totalEnemy = StageSelector.stageClear + 15;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnWolfRepeat());
            }
            else if (StageSelector.stageClear == 3)
            {
                totalEnemy = StageSelector.stageClear + 15;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnWolfRepeat());
            }
        }
        if(enemy.name == "Sea_Shell")
        {
            if(StageSelector.stageClear == 1)
            {
                totalEnemy = StageSelector.stageClear + 9;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnShellRepeat());
            }
            else if (StageSelector.stageClear == 2)
            {
                totalEnemy = StageSelector.stageClear + 10;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnShellRepeat());
            }
            else if (StageSelector.stageClear == 3)
            {
                totalEnemy = StageSelector.stageClear + 11;
                remainEnemy = totalEnemy;
                SpawnEnemy();
                StartCoroutine(SpawnShellRepeat());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        _sec += Time.deltaTime;
        timeString = string.Format("{0:D2}:{1:D2}", _min, (int)_sec);
        if ((int)_sec > 59)
        {
            _sec = 0;
            _min++;
        }
        timeText.text = timeString;
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(player);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Instantiate(enemy);
        }
        if (killcount >= totalEnemy && SceneManager.GetActiveScene().name!="Battle")
        {
            stageClear.SetActive(true);
            Text clearText = GameObject.Find("clearText").GetComponent<Text>();
            Text gainGold = GameObject.Find("gainGold").GetComponent<Text>();
            Text gainEnt = GameObject.Find("gainEnt").GetComponent<Text>();

            int stageClearGold = StageSelector.stageClear * 150;
            float stageClearEnt = stageClearGold * 0.1f;

            gainEnt.text = "+" + stageClearEnt + " E";

            stageClearEnt = PlayerPrefs.GetFloat("Stage Ent") + stageClearEnt;
            PlayerPrefs.SetFloat("Stage Ent", stageClearEnt);

            clearText.text = "! �������� Ŭ���� !";
            gainGold.text = "+" + stageClearGold + " G";

            InGameShopManager.coins += stageClearGold;

            //Invoke("InGameShopLoad", 3);
            killcount = 0;
        }
        else if (killcount >= totalEnemy && SceneManager.GetActiveScene().name == "Battle")
        {
            stageClear.SetActive(true);
            Text clearText = GameObject.Find("clearText").GetComponent<Text>();
            Text gainGold = GameObject.Find("gainGold").GetComponent<Text>();
            Text gainEnt = GameObject.Find("gainEnt").GetComponent<Text>();

            float stageClearEnt = 500.0f;

            stageClearEnt = PlayerPrefs.GetFloat("Stage Ent") + stageClearEnt;
            PlayerPrefs.SetFloat("Stage Ent", stageClearEnt);
            float totalClearEnt = PlayerPrefs.GetFloat("Total Ent") + stageClearEnt;
            PlayerPrefs.SetFloat("Total Ent", totalClearEnt);
            gainGold.text = "+0 G";
            Debug.Log(totalClearEnt);
            gainEnt.text = "+" + stageClearEnt + " E  >>> Total : " + totalClearEnt + " E";

            //Invoke("MainLoad", 3);
            killcount = 0;
        }
        
    }
    IEnumerator SpawnGiantRepeat()
    {
        int rand = (int)Random.Range(5, 10);
        for(int i = 0; i < totalEnemy - 1; i++)
        {
            yield return new WaitForSeconds(rand);
            SpawnEnemy();
            rand = (int)Random.Range(15, 20);
        }
    }
    IEnumerator SpawnWolfRepeat()
    {
        int rand = (int)Random.Range(5, 7);
        for (int i = 0; i < totalEnemy - 1; i++)
        {
            yield return new WaitForSeconds(rand);
            SpawnEnemy();
            rand = (int)Random.Range(5, 7);
        }
    }
    IEnumerator SpawnShellRepeat()
    {
        int rand = (int)Random.Range(7, 10);
        for (int i = 0; i < totalEnemy - 1; i++)
        {
            yield return new WaitForSeconds(rand);
            SpawnEnemy();
            rand = (int)Random.Range(10, 12);
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