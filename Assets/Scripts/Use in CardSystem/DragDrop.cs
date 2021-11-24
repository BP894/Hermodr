﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragDrop : MonoBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
    public GameObject Card4;
    public GameObject Card5;

    public GameObject Effect_01;
    public GameObject Effect_02;
    public GameObject Effect_03;
    public GameObject Effect_04;

    public GameObject Player;
    public GameObject Enemy;

    public float delayTime = 2f;
    private GameObject[] target;

    private bool isDragging = false;
    private bool isOverDropArea = false;
    private GameObject dropArea;
    private Vector2 startPosition;


    // Update is called once per frame sub
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > 9)
            {
                SceneManager.LoadScene(7);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropArea = true;
        dropArea = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropArea = false;
        dropArea = null;
    }

    public void StartDrag()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropArea)
        {
            transform.SetParent(dropArea.transform, false);

            if (this.gameObject==Card1||this.gameObject==Card2)
            {
                DrawCards.handCount--;

                Vector3 localPosition = GetComponent<RectTransform>().position;
                GetComponent<RectTransform>().position = localPosition + new Vector3(0, 0, -1000);

                GameObject p = Instantiate(Player);
                Effect_01.transform.position = Player.transform.position;
                Effect_01.transform.position -= new Vector3(-0.8f, 1.14f, 0f);

                StartCoroutine(CardEffect(Effect_01, p, 0.6f));
            }
            else if (this.gameObject == Card3)
            {
                DrawCards.handCount--;

                Vector3 localPosition = GetComponent<RectTransform>().position;
                GetComponent<RectTransform>().position = localPosition + new Vector3(0, 0, -1000);

                target = GameObject.FindGameObjectsWithTag("Enemy");
                float damage = target[0].GetComponent<EnemyCombat>().startHealth / 8;
                for(int i = 0; i < target.Length; i++)
                {
                    target[i].GetComponent<EnemyCombat>().OnDamage(damage);
                    Effect_02.transform.position = target[i].transform.position;
                    Effect_02.transform.position -= new Vector3(-1.0f, 0.6f, 0f);
                    StartCoroutine(CardEffect(Effect_02, target[i], 0.9f));
                    target[i] = null;
                }
            }
            else if(this.gameObject == Card4)
            {
                DrawCards.handCount--;

                Vector3 localPosition = GetComponent<RectTransform>().position;
                GetComponent<RectTransform>().position = localPosition + new Vector3(0, 0, -1000);

                target = GameObject.FindGameObjectsWithTag("Player");
                for(int i = 0; i <target.Length; i++)
                {
                    target[i].GetComponent<PlayerCombat>().RestoreHearth(50.0f);
                    Effect_03.transform.position = target[i].transform.position;
                    Effect_03.transform.position -= new Vector3(-0.8f, 1.14f, 0f);
                    StartCoroutine(CardEffect(Effect_03, target[i], 0.7f));
                    target[i] = null;
                }           
            }
            else if(this.gameObject == Card5)
            {
                DrawCards.handCount--;
                Vector3 localPosition = GetComponent<RectTransform>().position;
                GetComponent<RectTransform>().position = localPosition + new Vector3(0, 0, -1000);
                StartCoroutine(Card5_Delay(2.0f));
            }

        }
        else
        {
            transform.position = startPosition;
        }
    }
    public IEnumerator Card5_Delay(float delay)
    {
        target = GameObject.FindGameObjectsWithTag("Enemy");
        float[] normalSpeeds = new float[target.Length];
        if (target.Length != 1)
        {
            for (int i = 1; i < target.Length; i++)
            {
                normalSpeeds[i] = target[i].GetComponent<EnemyCombat>().moveSpeed;
                target[i].GetComponent<EnemyCombat>().moveSpeed = 0;

                Effect_04.transform.position = target[i].transform.position;
                Effect_04.transform.position -= new Vector3(-1.3f, 0.7f, 0f);
                GameObject e = Instantiate(Effect_04);
                e.transform.SetParent(target[i].transform);
            }
            yield return new WaitForSeconds(delay);

            for (int i = 1; i < target.Length; i++)
            {
                target[i].GetComponent<EnemyCombat>().moveSpeed = normalSpeeds[i];
                Destroy(target[i].transform.GetChild(2).gameObject);
                target[i] = null;
            }
        }
        Destroy(this.gameObject);
    }
    IEnumerator CardEffect(GameObject effect, GameObject target, float delay)
    {

        GameObject e = Instantiate(effect);
        e.transform.SetParent(target.transform);
       
        yield return new WaitForSeconds(delay);

        Destroy(target.transform.GetChild(2).gameObject);
        Destroy(this.gameObject);
    }
}
