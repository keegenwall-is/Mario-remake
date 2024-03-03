using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour{
    public static UIManager Instance;
    public Text goldText;
    public int goldNum;

    public GameObject prefabCoin;

    public int GoldNum{ get => goldNum;
    set {
        goldNum = value;
        goldText.text = "X" +goldNum;
    }
    }
    private void Awake(){
        Instance = this;
        //goldText = transform.Find("GoldNum/Text").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void addGoldScore(int addScore){
        this.goldNum += addScore;
        this.goldText.text = "X " + this.goldNum.ToString();
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);

        // 摧毁预制体
        Destroy(obj);
    }

    public void boxToCoin(GameObject gameObject){
        Vector3 boxPosition = gameObject.transform.position;
        float coinPosY = boxPosition.y + 1;
        float coinPosX = boxPosition.x;
        System.Random random = new System.Random();

        // 生成一个介于 1 到 5 之间的随机整数
        int randomNumber = random.Next(1, 6);
        //gameObject.active = false;
        for (int i = 0; i < randomNumber; i++){
            GameObject spawnedPrefab = Instantiate(prefabCoin, new Vector3(coinPosX + i,coinPosY, 0), Quaternion.identity);
            StartCoroutine(DestroyAfterDelay(spawnedPrefab, 1f));
        
        }
        this.addGoldScore(randomNumber);

        

        Debug.Log(randomNumber);

    }
}
