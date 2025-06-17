 using System;
 using System.Collections.Generic;
 using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime;
    public float maxGameTime;
    public int level{ get; set; }

    public int killCount { get; set; } = 0;

    public int endTime;

    public float exp { get; set; }

    public int currentGameTime { get; set; }
    
    [SerializeField] private TextMeshProUGUI _levelTxt;
    [SerializeField] private TextMeshProUGUI _currentExptxt;
    [SerializeField] private GameObject _ballPrefab;

    public int nextLevel = 2;

    public float expValue;
    
    public float GetBallPercent { get; set; }
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private Slider _slider;
    
    public bool isEnd { get; set; } = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
      //  levelSystem.Show();
    }
    
       private void Update()
       {
           gameTime += Time.deltaTime;
           endTime += (int)Time.deltaTime;
    
           expValue = Mathf.Lerp(expValue, exp,  10 * Time.deltaTime);
       }
    
       public void GetExp()
       {
           exp += 1;
           _currentExptxt.text = $"Exp : {exp} : {nextLevel}";

           if (exp >= nextLevel)
           {
               level++;
               exp = 0;
               nextLevel += 2;
               _slider.maxValue = nextLevel;
               _levelTxt.text = $"Level : {level}";
               _currentExptxt.text = $"Exp : {exp} : {nextLevel}";
               levelSystem.Show();
           }
       }


      public void SpawnHpBall(Vector3 transform, Quaternion rotation)
      {
          Instantiate(_ballPrefab,transform,rotation);
      }
}
