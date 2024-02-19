using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicSwitcher : MonoBehaviour
{
    //switches music track
    public AudioSource _Music1;
    public AudioSource _Music2;
    public AudioSource _Music3;
     void Update () 
     {
         if (Input.GetKeyDown(KeyCode.M))
         {
             if (_Music1.isPlaying)
             {
                 _Music1.Stop();
                 _Music2.Play();
 
             }
             else if (_Music2.isPlaying)
             {
                 _Music2.Stop();
                 _Music3.Play();
             }
             else if (_Music3.isPlaying)
             {
                 _Music3.Stop();
                 _Music1.Play();
             }
         }
     }
}
