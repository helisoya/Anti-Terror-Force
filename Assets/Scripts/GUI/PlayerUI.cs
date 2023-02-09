using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Text dialogText;

    private Coroutine dialogInProgress = null;

    [SerializeField]
    private Image fadeImg;


    public bool dialogIsInProgress {get{return dialogInProgress !=null;}}

    void Awake(){
        instance = this;
    }

    public void SetAmmoText(int ammoInMag,int totalAmmo){
        if(totalAmmo == -1){
            ammoText.text = "";
        }else{
            ammoText.text=ammoInMag.ToString()+"/"+totalAmmo.ToString();
        }
    }

    public void SetHealthBarLength(int health,int maxHealth){
        fillImage.fillAmount = (float)health/(float)maxHealth;
    }

    public void FadeToBlackBeforeLoading(string nextLevel){
        StartCoroutine(FadeToBlack(nextLevel));
    }

    IEnumerator FadeToBlack(string nextLevel){
        while(fadeImg.color != Color.black){
            fadeImg.color = Color.Lerp(fadeImg.color,Color.black,Time.deltaTime*5);
            yield return new WaitForEndOfFrame();
        }
        Loading.LoadScene(nextLevel);
    }

    public void SetDialog(string[] texts,Color[] colors){
        if(dialogInProgress != null){
            StopCoroutine(dialogInProgress);
        }

        dialogInProgress = StartCoroutine(Dialog(texts,colors));
    }


    IEnumerator Dialog(string[] texts,Color[] colors){
        Color col = new Color(0,0,0,0);
        dialogText.color = col;
        for(int i = 0;i<texts.Length;i++){
            dialogText.text = texts[i];
            while(dialogText.color != colors[i]){
                dialogText.color = Color.Lerp(dialogText.color,colors[i],Time.deltaTime*5);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2);
            while(dialogText.color != col){
                dialogText.color = Color.Lerp(dialogText.color,col,Time.deltaTime*5);
                yield return new WaitForEndOfFrame();
            }

        }

        dialogInProgress = null;
    }


    public void SetHint(string hint,Color col){
        if(dialogInProgress != null){
            StopCoroutine(dialogInProgress);
        }
        dialogInProgress = null;
        
        dialogText.color = col;
        dialogText.text = hint;
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
