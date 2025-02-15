﻿using Assets.Scripts.Shooter;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Shooter.Powerup;

namespace Assets.Scripts.Runner
{
    public class RunnerGuy : MonoBehaviour
    {
        public float BaseSpeed = 1f;
        private float Speed = 0f;
        public float SpeedBoostIncrease = 2f;
        public float SpeedBoostDuration = 1f;
        public GameObject RunnerCamera;
        private CameraZoom cameraZoom;
        private const int RUNNER_LAYER = 19;

        private Rigidbody2D rb;
        private SpriteRenderer sr;

    
        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            if(RunnerCamera == null)
            {
                RunnerCamera = GameObject.Find("RunnerCamera");
            }
            cameraZoom = RunnerCamera.GetComponent<CameraZoom>();
        }

        private void FixedUpdate() {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }

        internal void RestartLevel()
        {
            rb.gravityScale = 1;
            sr.flipY = false;
        }

        public void Jump(float strength) {
            rb.AddForce(Vector2.up * strength, ForceMode2D.Impulse);
        }

        public void ActivatePowerup(PowerupType type, Powerup power) {
            switch (type) {
                case PowerupType.Jump:
                    Jump(power.Strength * rb.gravityScale);
                    break;
                case PowerupType.Antigrav:
                    rb.gravityScale *= -1;
                    sr.flipY = rb.gravityScale == -1;
                    break;
                case PowerupType.Dash:
                    StartCoroutine(SpeedBoost());
                    break;
                default:
                    break;
            }
        }

        IEnumerator SpeedBoost() {
            Speed = SpeedBoostIncrease;
            yield return new WaitForSeconds(SpeedBoostDuration);
            Speed = BaseSpeed;
        }

        public void Stop() {
            Speed = 0;
        }

        public void SetSpeed(float speed) {
            Speed = speed;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            //Debug.Log("Zoom now");
            if (collision.gameObject && collision.gameObject.layer == RUNNER_LAYER) { cameraZoom.quickZoom(); }
        }
    }
}
