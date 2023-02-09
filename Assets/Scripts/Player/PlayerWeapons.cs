using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public Weapon[] weapons;

    int currentWeapon = 0;

    public Camera cam;

    public ParticleSystem muzzleFlare;

    public static PlayerWeapons instance;

    void Start()
    {
        instance = this;
        ChangeWeapon(0);
        foreach(Weapon weapon in weapons){
            weapon.ResetGun();
        }
        UpdateGunUI();
    }

    public bool CanAddAmmoToMainGun(){
        return weapons[currentWeapon].CanAddBullets();
    }

    public void AddClipToMainGun(){
        weapons[currentWeapon].AddBullets(weapons[currentWeapon].maxAmmoInMag);
        UpdateGunUI();
    }

    void UpdateGunUI(){
        PlayerUI.instance.SetAmmoText(weapons[currentWeapon].GetAmmoInMag(),weapons[currentWeapon].GetTotalAmmo());
    }

    void ChangeWeapon(int newWeapon){
        weapons[currentWeapon].gunModel.SetActive(false);
        currentWeapon = newWeapon;
        weapons[currentWeapon].gunModel.SetActive(true);
        muzzleFlare.transform.parent = weapons[currentWeapon].barrel.transform;
        muzzleFlare.transform.localPosition = Vector3.zero;
        UpdateGunUI();
    }


    void Update(){
        if(Input.mouseScrollDelta.y > 0){
            ChangeWeapon( (currentWeapon + 1 + weapons.Length) % weapons.Length);
        }
        else if(Input.mouseScrollDelta.y < 0){
            ChangeWeapon( (currentWeapon - 1 + weapons.Length) % weapons.Length);
        }

        if(Input.GetKeyDown(KeyCode.R)){
            weapons[currentWeapon].Reload();
            UpdateGunUI();
        }

        weapons[currentWeapon].Cooldown();
        if(weapons[currentWeapon].CanShoot()){
            if( (weapons[currentWeapon].autoFireMode && Input.GetMouseButton(0)) ||
            ( !weapons[currentWeapon].autoFireMode && Input.GetMouseButtonDown(0) ) ){
                Shoot(); 
                UpdateGunUI();
            }
        }
    }

    void Shoot(){
        weapons[currentWeapon].Shoot();
        muzzleFlare.Play();
        weapons[currentWeapon].animation.Play();

        RaycastHit hit;
        if(Physics.Raycast(transform.position
        ,cam.transform.forward 
        ,out hit,weapons[currentWeapon].maxDistance)){
                if(hit.collider.tag=="Ennemy" && hit.collider.GetComponent<TerroristHealth>() != null){
                    hit.collider.GetComponent<TerroristHealth>().TakeDamage(weapons[currentWeapon].damage);
                }
        }
    }

}
