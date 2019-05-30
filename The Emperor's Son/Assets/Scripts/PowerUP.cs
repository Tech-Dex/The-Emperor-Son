﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    public bool hpPowerUp = false;
    public bool speedPowerUp = false;
    public bool jumpPowerUp = false;
    public bool regenHpUp = false;

    public int regenHp = 1;
    public int multiplyHP = 3;
    public int multiplyMaxHP = 3;
    public float multiplySpeed = 2;
    public float multiplyFirstJump = 4;
    public float multiplySecondJump = 2;
    public float duration = 20f;

	[SerializeField] private AudioClip collectSound;
	[SerializeField] private AudioClip bounceSound;
	[SerializeField] private AudioSource audioSource;

	
    void Start(){
        audioSource = GetComponent<AudioSource> ();
    }
    void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == NewPlayer.Instance.gameObject) {
			StartCoroutine(PickUp (col));
		}
	}

    IEnumerator PickUp(Collider2D player){

        NewPlayer stats = player.GetComponent<NewPlayer>();

        if(hpPowerUp == true){
            stats.health += multiplyHP;
            stats.maxHealth += multiplyMaxHP;

            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(duration);
            if(stats.health - multiplyHP >= 1)
                stats.health -= multiplyHP;
            else
                stats.health = 1;

            stats.maxHealth -= multiplyMaxHP;

        GameManager.Instance.audioSource.PlayOneShot (collectSound);
		if (transform.parent.GetComponent<Bouncer> () != null) {
			Destroy (transform.parent.gameObject);
		} else {
			Destroy (gameObject);
		}
        }


        if(speedPowerUp == true){
            stats.maxSpeed += multiplySpeed;

            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(duration);

            stats.maxSpeed -= multiplySpeed;

        GameManager.Instance.audioSource.PlayOneShot (collectSound);
		if (transform.parent.GetComponent<Bouncer> () != null) {
			Destroy (transform.parent.gameObject);
		} else {
			Destroy (gameObject);
		}
        }

        if(jumpPowerUp == true){
            stats.jumpTakeOffSpeed += multiplyFirstJump;
            stats.secondJumpTakeOffSpeed += multiplySecondJump;

            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(duration);

            stats.jumpTakeOffSpeed -= multiplyFirstJump;
            stats.secondJumpTakeOffSpeed -= multiplySecondJump;

        GameManager.Instance.audioSource.PlayOneShot (collectSound);
		if (transform.parent.GetComponent<Bouncer> () != null) {
			Destroy (transform.parent.gameObject);
		} else {
			Destroy (gameObject);
		}

        }

        if(regenHpUp == true){
            if(stats.maxHealth >= stats.health + regenHp){
                stats.health += regenHp;
        GameManager.Instance.audioSource.PlayOneShot (collectSound);
		if (transform.parent.GetComponent<Bouncer> () != null) {
			Destroy (transform.parent.gameObject);
		} else {
			Destroy (gameObject);
		}
            }

        }
    }
}