using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject scorePanel, oopsPanel, timepanel, gameoverScreen;
    [SerializeField] private GameObject stillfish, animfish;
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private GameObject player, line;
    [SerializeField] private GameObject objectSpawner;
    [SerializeField] private GameObject tro1, tro2, tro3;
    [SerializeField] private Material mat;
    private bool inround;
    public float timeTotal, timeLeft;
    public bool timerOn = false;
    public bool InRound { get { return inround; } }
    private bool gameOver;
    public bool GameOver { get { return gameOver; } }

    private int oopsCounter = 0;
    public int OopsCounter { get { return oopsCounter; } }

    [SerializeField] private TextMeshProUGUI oopsText,moneyText,timeText,moneyText2;
    [SerializeField] private List<Image> oopsImages = new();

    [SerializeField] private GameObject overlay;

    private int money;
    public int Money { get {  return money; } set { money = value; } }

    

    void Start()
    {
        scorePanel.SetActive(false);
        oopsPanel.SetActive(false);
        timepanel.SetActive(false);
        player.SetActive(false);
        line.SetActive(false);
        gameoverScreen.SetActive(false);
        timeTotal = timeLeft = musicPlayer.GetComponent<AudioSource>().clip.length;
    }

    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                EndGame();
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime++;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void StartGame()
    {
        StartCoroutine(nameof(StartGameCoroutine));
    }
    private IEnumerator StartGameCoroutine()
    {
        timerOn = true;
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
        StartCoroutine(nameof(MakeOverlay));
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
            //EndGame();
        }
    }
    private IEnumerator MakeOverlay()
    {
        overlay.SetActive(true);
        yield return new WaitForSeconds(0.34f);
        overlay.SetActive(false);
    }
    public void MakeMoney(int amount)
    {
        SoundManager.Instance.PlaySoundEffect(SoundType.Cash);
        money += amount;
        moneyText.text = $"${money}";
        moneyText.transform.DOPunchScale(Vector3.one * 1.15f, 0.2f).OnComplete(()=>moneyText.transform.localScale = Vector3.one);
    }

    public void EndGame()
    {
        moneyText2.text = $"your score:\n${money}";
        Debug.Log("end game");
        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        if (money > 1000000)
            tro1.GetComponent<Image>().material = mat;
        if (money > 2000000)
            tro2.GetComponent<Image>().material = mat;
        if (money > 3000000)
            tro3.GetComponent<Image>().material = mat;

        yield return null;
        /*float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(0, 20)
                , timeElapsed / 2);
            timeElapsed += Time.deltaTime;
            yield return null;
        }*/
        gameoverScreen.SetActive(true);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
