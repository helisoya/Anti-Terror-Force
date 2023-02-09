using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{   
    public Animation animation;
    public GameObject gunModel;
    public GameObject barrel;
    public int damage;
    public int maxDistance;
    int currAmmo;
    int ammoInMag;
    public int maxAmmoInMag;
    public int maxAmmo;

    public float maxCooldownReload;
    public float maxCooldown;
    float cooldown = 0;
    public bool autoFireMode;

    public void ResetGun(){
        currAmmo = maxAmmo;
        ammoInMag = maxAmmoInMag;
    }

    public void Reload(){
        while(ammoInMag < maxAmmoInMag && currAmmo > 0){
            ammoInMag++; 
            currAmmo--;
        }
        cooldown = maxCooldownReload;
    }

    public bool IsMagasineEmpty(){
        return ammoInMag == 0;
    }

    public void Shoot(){
        ammoInMag--;
        cooldown = maxCooldown;
    }

    public void AddBullets(int val){
        currAmmo+=val;
        if(currAmmo > maxAmmo){
            currAmmo = maxAmmo;
        }
    }

    public void Cooldown(){
        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }
    }

    public bool CanAddBullets(){
        return currAmmo != maxAmmo;
    }

    public bool CanShoot(){
        return cooldown <= 0 && !IsMagasineEmpty();
    }


    public int GetTotalAmmo(){
        return currAmmo;
    }

    public int GetAmmoInMag(){
        return ammoInMag;
    }

}
