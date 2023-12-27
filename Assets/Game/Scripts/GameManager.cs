using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ get; private set; }
    
    [SerializeField] private TextMeshProUGUI bulletsRemainingTMP;
    [SerializeField] private TextMeshProUGUI gameOverTMP;
    [SerializeField] private TextMeshProUGUI killsTMP;
    [SerializeField] private TextMeshProUGUI xpTMP;
    [SerializeField] private TextMeshProUGUI gameWinTMP;
    [SerializeField] private Image HPBar;
    [SerializeField] private int numOfKillsNeededToWin;

    private GameObject attachedMagnet;

    public void SetAttachedMagnet(GameObject attachedMagnet){
        this.attachedMagnet = attachedMagnet;
    }
    public int xp = 0;
    private int kills = 0;
    private float _HP = 1;

    public void SetHP(float hp){
        _HP += hp;
    }
    private int _bulletsRemaining = 10;
    public void SetBulletsRemaining(int bulletsRemaining){
        _bulletsRemaining = bulletsRemaining;
    }

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(Instance);
        }
    }

    private void Update(){
        if (_HP <= 0){
            gameOverTMP.gameObject.SetActive(true);
        }
        xpTMP.text = "XP: " + xp.ToString();
        if (attachedMagnet != null && xp >= 30){
            gameWinTMP.gameObject.SetActive(true);
            Destroy(attachedMagnet);
        }
        else if (kills >= numOfKillsNeededToWin){
            gameWinTMP.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateBulletsRemaining(){
        bulletsRemainingTMP.text = "Bullets: " + _bulletsRemaining.ToString();
    }

    public void DecreaseHP(float decreaseAmount){
        _HP -= decreaseAmount;
        HPBar.fillAmount = _HP;
    }

    public void IncreaseKills(){
        kills++;
        killsTMP.text = "Kills: " + kills.ToString();
    }
}
