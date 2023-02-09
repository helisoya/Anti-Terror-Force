using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private GameObject prefabSmoke;

    [SerializeField]
    private TerroristBrain brain;

    [SerializeField]
    private GameObject[] prefabsToSpawn;


    public void TakeDamage(int val){
        health-=val;
        if(!brain.hasSeenPlayer){
            brain.SetToAlertMode();
        }
        if(health<=0){
            GameObject.Instantiate(prefabsToSpawn[Random.Range(0,prefabsToSpawn.Length)],transform.position,new Quaternion());
            GameObject smoke = GameObject.Instantiate(prefabSmoke,transform.position,new Quaternion());
            GameObject.Destroy(smoke,2);
            GameObject.Destroy(gameObject);
            this.enabled = false;
        }
    }
}
