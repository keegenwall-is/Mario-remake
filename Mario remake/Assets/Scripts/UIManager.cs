using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
    public static UIManager Instance;
    private Text goldText;
    private int goldNum;

    public int GoldNum{ get => goldNum;
    set {
        goldNum = value;
        goldText.text = "X" +goldNum;
    }
    }
    private void Awake(){
        Instance = this;
        goldText = transform.Find("GoldNum/Text").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
