using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public TMP_InputField field;
    public RectTransform scrollView;
    public GameObject userPopup;
    public GameObject botPopup;

    private void Update()
    {
        if (field.text.Contains("\n"))
        { ChooseSmth(); field.text = ""; }
    }

    public void ChooseSmth()
    {
        string txt = field.text;

        if (!string.IsNullOrEmpty(txt))
        {
            SpawnUserPopup(txt);

            string res = Simplify(txt);

            string answer;

            switch (res)
            {
                case "привет" or "здарова" or "вечер в хату": { answer = "И тебе не хворать"; break; }
                case "как дела" or "ты как": { answer = "Да держусь потихоньку"; break; }
                case "который час" or "время": { answer = System.DateTime.Now.Hour.ToString()+":"+System.DateTime.Now.Minute.ToString(); break; }
                case "команды"or"список команд"or"help" or "помощь": { answer = "Список команд:\n-Привет\n-Как дела?\n-Который час?\n-Камень/Ножницы/Бумага\n-Рандомное число"; break; }
                case "камень": { answer = "Бумага!"; break; }
                case "ножницы": { answer = "Камень!"; break; }
                case "бумага": { answer = "Ножницы!"; break; }
                case "рандомное число" or "число": { answer = Random.Range(0,100).ToString(); break; }
                default: { answer = "Не понял вопрос, перефразируй"; break; }
            }

            StartCoroutine(SpawnBotPopup(answer));

            field.interactable = false;
        }
    }

    public string Simplify(string str)
    {
        str=str.ToLower();

        string res = Regex.Replace(str, @"[^а-яёa-z\s]", "");

        res = Regex.Replace(res, @"\s+"," ");

        return res.Trim();
    }


    public void SpawnUserPopup(string txt)
    {
        GameObject newUserPopup = Instantiate(userPopup, scrollView);
        newUserPopup.GetComponentInChildren<TMP_Text>().text = txt;
    }

    IEnumerator SpawnBotPopup(string txt)
    {
        yield return new WaitForSeconds(1);

        GameObject newBotPopup = Instantiate(botPopup, scrollView);
        newBotPopup.GetComponentInChildren<TMP_Text>().text = txt;

        field.interactable = true;
        field.Select();
    }
}
