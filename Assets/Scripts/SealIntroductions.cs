using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SealIntroductions
{
    public string Title { set; get; }
    public string Content { set; get; }
    public string Hardness { set; get; }
    public string Glossiness { set; get; }
    public string Difficulty { set; get; }
    public string Price { set; get; }
    public string Addvice { set; get; }
    public Sprite Image { set; get; }
    public SealIntroductions(string title, string content, string hardness, string glossiness, string difficulty, string price, string addvice)
    {
        this.Title = title;
        this.Content = content;
        this.Hardness = hardness;
        this.Glossiness = glossiness;
        this.Difficulty = difficulty;
        this.Price = price;
        this.Addvice = addvice;
    }

    public void ShowOnUI(GameObject img)
    {
        img.SetActive(true);
        img.transform.Find("Image").GetComponent<Image>().sprite = Image;
        img.transform.Find("Title").GetComponent<Text>().text = Title;
        img.transform.Find("IntroductionText").GetComponent<Text>().text = "<color=#FFFFFF00>jayw</color>" + Content;
        img.transform.Find("hardnessText").GetComponent<Text>().text = Hardness;
        img.transform.Find("glossinessText").GetComponent<Text>().text = Glossiness;
        img.transform.Find("difficultyText").GetComponent<Text>().text = Difficulty;
        img.transform.Find("priceText").GetComponent<Text>().text = Price;
        //img.transform.Find("adviceText").GetComponent<Text>().text = "<color=#FFFFFF00>jayw</color>" + Addvice;
    }
}
