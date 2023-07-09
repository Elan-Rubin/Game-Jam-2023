using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject scorePanel, oopsPanel;
    [SerializeField] private GameObject stillfish, animfish;
    [SerializeField] private GameObject musicPlayer;
    private bool inround;
    public bool InRound { get { return inround; } }

    void Start()
    {
        scorePanel.SetActive(false);
        oopsPanel.SetActive(false);
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
        musicPlayer.GetComponent<AudioSource>().Play();
        inround = true;
    }
}
