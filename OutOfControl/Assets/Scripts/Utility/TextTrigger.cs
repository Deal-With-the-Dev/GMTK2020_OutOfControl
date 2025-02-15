﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class TextTrigger : MonoBehaviour
    {
        public AnimateText Text;
        public string Message = "";

        private void Start()
        {
            if(Text == null)
            {
                Text = GameObject.Find("Text").GetComponent<AnimateText>();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Runner"))
            {
                Text.ShowText(Message);
            }
        }

        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider2D>();
            Gizmos.DrawWireCube(this.transform.position, new Vector3(collider.size.x, collider.size.y, 1));
        }
    }
}
