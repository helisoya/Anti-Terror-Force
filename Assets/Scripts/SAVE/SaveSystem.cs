using UnityEngine.SceneManagement;

public class SaveSystem
{
    public static SAVEFILE save = new SAVEFILE();

    public static void Save(){
        save.currentBriefing = BirefingHolder.nextBriefing;
        FileManager.SaveJSON(FileManager.savPath+"save.sav",save);
    }

    public static void NewSave(string name){
        save = new SAVEFILE();
        save.playername = name;
    }

    public static void Load(){
        if(SaveFileExists()){
            save = FileManager.LoadJSON<SAVEFILE>(FileManager.savPath+"save.sav");
            BirefingHolder.nextBriefing = save.currentBriefing;
            Loading.LoadScene(save.currentLevel);
        }
        
    }

    public static bool SaveFileExists(){
        return System.IO.File.Exists(FileManager.savPath+"save.sav");
    }
}
