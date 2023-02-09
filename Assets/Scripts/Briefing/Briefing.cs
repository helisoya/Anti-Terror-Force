using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Briefing : MonoBehaviour
{
    private List<string> data;

    private string nextMap = null;
    private string nextBriefingToLoad = null;

    public Text title;
    public Text desc;
    public RawImage img;
    public Text objectifs;
    void Start()
    {
        string[] slicedLine;
        data = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Briefings/"+BirefingHolder.nextBriefing)); 
        foreach(string line in data){
            slicedLine = line.Split('=');
            if(slicedLine.Length != 2){
                print("ERREUR SUR "+line);
                return;
            }
            switch (slicedLine[0]){
                case "Title":
                    title.text = slicedLine[1];
                    break;
                case "Image":
                    img.texture = Resources.Load("Images/"+slicedLine[1]) as Texture2D;
                    break;
                case "Desc":
                    desc.text = ReadText(slicedLine[1]);
                    break;
                case "Objectifs":
                    objectifs.text = ReadText(slicedLine[1]);
                    break;
                case "Next":
                    if(slicedLine[1].Contains("|")){ // Prochain -> Briefing
                        string[] parts = slicedLine[1].Split('|');
                        if(parts.Length != 2){
                            print("ERREUR SUR "+line); 
                            return;
                        }
                        nextMap = parts[0];
                        nextBriefingToLoad = parts[1];
                    }else{
                        nextMap = slicedLine[1];
                    }
                    break;
            }
        }
    }

    public void LoadNextMap(){
        BirefingHolder.nextBriefing = nextBriefingToLoad;
        Loading.LoadScene(nextMap);
    }

    string ReadText(string txt){
        string res = "";
        foreach(char letter in txt){
            if(letter=='|'){
                res+="\n";
            }else{
                res+=letter;
            }
        }
        
        return res.Replace("PLAYERNAME",SaveSystem.save.playername);
    }
}
