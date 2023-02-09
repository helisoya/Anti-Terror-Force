using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristShoot : MonoBehaviour
{
    bool canShoot = false;
    public Animator animator;

    public float MaxCooldownBeetweenShot;
    public float MaxCooldownReload;
    public int maxBullets;
    int bullets;
    float cooldown = 0;

    public int damage;

    private Transform player;

    public Transform head;

    void Start(){
        bullets = maxBullets;
        player = GameObject.Find("Player").transform;
    }

    public void SetCanShoot(bool val){
        canShoot = val;
    }

    void Update()
    {
        if(!canShoot){
            return;
        }

        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }


        if(Vector3.Distance(transform.position,player.position) <= 200){
            LookAtPlayer();
            if(cooldown <= 0){

                RaycastHit hit;
                if(Physics.Raycast(head.position
                ,(PlayerHealth.instance.transform.position-head.position).normalized + transform.up * Random.Range(-0.1f,0.1f) + transform.right * Random.Range(-0.1f,0.1f)
                ,out hit,200)){
                        if(hit.collider.tag=="Player"){
                            PlayerHealth.instance.AddHealth(-damage);
                        }
                }

                bullets--;
                if(bullets==0){
                    bullets = maxBullets;
                    cooldown = MaxCooldownReload;
                    animator.SetBool("Shooting",false);
                }else{
                    animator.SetBool("Shooting",true);
                    cooldown = MaxCooldownBeetweenShot;
                } 
            }
        }
    }


    public void LookAtPlayer(){
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
    }
}
