using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndOfMap : MonoBehaviour
{
    public string nextLevel;
    public string briefingToLoad = "";

    void Start(){
        BirefingHolder.nextBriefing = briefingToLoad=="" ? null : briefingToLoad;
        PlayerUI.instance.FadeToBlackBeforeLoading(nextLevel);
        this.enabled = false;
    }

}
