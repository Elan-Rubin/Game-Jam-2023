using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    #endregion
    [SerializeField] private GameObject scorePanel, oopsPanel, timepanel;
    [SerializeField] private GameObject stillfish, animfish;
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private GameObject player, line;
    [SerializeField] private GameObject objectSpawner;
    private bool inround;
    public bool InRound { get { return inround; } }
    private bool gameOver;
    public bool GameOver { get { return gameOver; } }

    private int oopsCounter = 0;
    public int OopsCounter { get { return oopsCounter; } }

    [SerializeField] private TextMeshProUGUI oopsText,moneyText,timeText;
    [SerializeField] private List<Image> oopsImages = new();   

    private int money;
    public int Money { get {  return money; } set { money = value; } }

    

    void Start()
    {
        scorePanel.SetActive(false);
        oopsPanel.SetActive(false);
        timepanel.SetActive(false);
        player.SetActive(false);
        line.SetActive(false);
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(nameof(StartGameCoroutine));
    }
    private IEnumerator StartGameCoroutine()
    {
        scorePanel.SetActive(true);
        oopsPanel.SetActive(true);
        stillfish.SetActive(false);
        SoundManager.Instance.PlaySoundEffect(SoundType.Swoosh);
        animfish.SetActive(true);
        yield return new WaitForSeconds(1.1f);

        player.SetActive(true);
        line.SetActive(true);
        musicPlayer.GetComponent<AudioSource>().Play();
        timepanel.SetActive(true);
        inround = true;

        yield return new WaitForSeconds(4f);
        objectSpawner.SetActive(true);
    }

    public void Oops(Sprite sprite) 
    {
        oopsImages[oopsCounter].sprite = sprite;
        oopsImages[OopsCounter].gameObject.SetActive(true);
        oopsImages[OopsCounter].transform.Rotate(0,0,Random.Range(-15f, 15f));
        oopsImages[oopsCounter].transform.DOPunchScale(Vector3.one * 1.15f, 0.2f);
        oopsText.text = $"{++oopsCounter}/4";
        //if (oopsCounter == 4) gameOver = true;
        if (oopsCounter == 4)
        {
            foreach (var x in oopsImages)
                x.gameObject.SetActive(false);
            oopsText.text = $"{oopsCounter = 0}/4";
            money = (int)(money / 2);
            moneyText.text = $"${money}";
            SoundManager.Instance.PlaySoundEffect(SoundType.Scream);
        }
    }

    public void MakeMoney(int amount)
    {
        SoundManager.Instance.PlaySoundEffect(SoundType.Cash);
        money += amount;
        moneyText.text = $"${money}";
        moneyText.transform.DOPunchScale(Vector3.one * 1.15f, 0.2f).OnComplete(()=>moneyText.transform.localScale = Vector3.one);
    }
}
