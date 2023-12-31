using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Person : MonoBehaviour
{
    public bool shouldMove = true;
    private PersonManager manager;
    private string title, dialogue; //unity was getting mad that i had a variable named name
    private int skinColor;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private List<Sprite> clouds = new();
    private void Start()
    {
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Count)];
        StartCoroutine(nameof(LateStartCoroutine));
        moveDirection = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
    }
    IEnumerator LateStartCoroutine()
    {
        yield return null;
        LateStart();
    }
    void LateStart()
    {
        manager = PersonManager.Instance;
        manager.People.Add(this.gameObject);

        title = manager.RandomName();
        dialogue = manager.RandomDialogue();
        var bodyparts = manager.RandomPerson();
        var renderer = transform.GetChild(0);
        for (int i = 0; i < renderer.childCount; i++)
            renderer.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = bodyparts[i];
        if (Random.value > 0.5f)
        {
            transform.Rotate(0, 180, 0);
            transform.GetChild(2).Rotate(0, 180, 0);
        }
        //remove later
        //SaySomething();
    }

    void Update()
    {
        if (shouldMove) 
            transform.localPosition += moveDirection * Time.deltaTime * moveSpeed;
    }

    public void SaySomething() => StartCoroutine(nameof(SaySomethingCoroutine));
    private IEnumerator SaySomethingCoroutine()
    {
        dialogueBox.SetActive(true);
        dialogueBox.transform.localScale = Vector3.zero;
        dialogueBox.transform.DOScale(Vector3.one, 0.2f)/*.OnComplete(() => dialogueBox.transform.DOPunchScale(Vector3.one * 1.25f, 0.1f))*/;
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.PlaySoundEffect(SoundType.Honk);
        var d = "";
        var x = 0;
        while (d.Length < dialogue.Length)
        {
            x++;
            d = dialogue.Substring(0, x);
            dialogueText.text = d.ToLower();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        dialogueText.text = "";

        dialogueBox.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => dialogueBox.SetActive(false));
    }
}
