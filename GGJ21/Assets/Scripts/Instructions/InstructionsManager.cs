using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsManager : MonoBehaviour
{
    public Sprite[] instrSprites;
    public string[] instrString; 
    public Image imgInst;
    public TextMeshProUGUI tmpInstr;
    public Button btnNext;
    public Button btnPrev;

    private int index;
    private int spritesLength;

    void Start()
    {
        index = 0;
        spritesLength = instrSprites.Length;
    }

    void Update()
    {
        
    }

    public void NextInstruction()
    {
        if (index < spritesLength - 1)
        {
            index++;
            imgInst.sprite = instrSprites[index];
            tmpInstr.text = instrString[index];
            if (btnPrev.interactable == false)
                btnPrev.interactable = true;
            if (index == spritesLength - 1)
                btnNext.transform.GetChild(0).GetComponent<Text>().text = "Close";
        }
        else
        {
            index = 0;
            imgInst.sprite = instrSprites[index];
            tmpInstr.text = instrString[index];
            btnNext.transform.GetChild(0).GetComponent<Text>().text = "Next";
            btnPrev.interactable = false;
            this.gameObject.SetActive(false);
        }
        
    }

    public void PrevInstruction()
    {
        if (index > 0)
        {
            index--;
            imgInst.sprite = instrSprites[index];
            tmpInstr.text = instrString[index];
            if (index == 0)
                btnPrev.interactable = false;
            if (index == spritesLength - 2)
                btnNext.transform.GetChild(0).GetComponent<Text>().text = "Next";
        }
        
    }
}
