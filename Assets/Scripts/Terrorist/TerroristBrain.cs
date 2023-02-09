using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristBrain : MonoBehaviour
{
    public FieldOfView fov;
    public TerroristMovement move;

    public TerroristShoot shoot;

    public bool hasSeenPlayer = false;

    public AudioSource source;

    public AudioClip[] alertClips;

    void Update()
    {

        if(hasSeenPlayer){
            return;
        }

        if(fov.canSeePlayer){
            SetToAlertMode();
        }
    }

    public void SetToAlertMode(){
        SetSeePlayer(true);
        source.clip = alertClips[Random.Range(0,alertClips.Length)];
        source.Play();
        shoot.LookAtPlayer();
    }

    public void SetSeePlayer(bool val){
        hasSeenPlayer = val;
        move.SetCanMove(!val);
        shoot.SetCanShoot(val);
    }
}
