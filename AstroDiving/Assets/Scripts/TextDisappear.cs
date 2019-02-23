using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisappear : MonoBehaviour {
      public float time = 5f; 
      
      void Start()
      {
          Invoke("Disappear", time);
      }
 
      void Disappear(){
          Destroy(gameObject);
      }
      
 }
