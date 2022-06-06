using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalWall : MonoBehaviour
{
   public AudioClip hitAudio;

   public void PlayAudio()
   {
      AudioSource.PlayClipAtPoint(hitAudio,Camera.main.transform.position);
   }
}
