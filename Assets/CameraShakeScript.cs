using UnityEngine;
using System.Collections;
 
public class CameraShakeScript : MonoBehaviour {
    // remember the correct starting camera position so we can revert back once shaking is complete
    private Vector3 originPosition;
    private Quaternion originRotation;
 
   public float originalDecay = 0.006f;
   public float originalIntensity = 0.04f;
   float shake_decay;
   float shake_intensity;
   private bool shaking; // is the camera supposed to be shaking at the moment
   GameObject player;
   Transform cameraTransform;
 
   void Start() {
       player = GameObject.FindWithTag("Player");
       cameraTransform = player.transform;       
   }
 
   void Update (){
       if(!shaking)            
           return;        
 
       if (shake_intensity > 0f)
       {
           player.GetComponent<BoxCollider2D>().isTrigger = true;
            cameraTransform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
            cameraTransform.localRotation = new Quaternion(                
            originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,                
            originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,                
            originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,                
            originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
            shake_intensity -= shake_decay;
       }
 
      else
      {
          player.GetComponent<BoxCollider2D>().isTrigger = false;
            shaking = false;
            // reset the camera to its original state
            cameraTransform.localPosition = originPosition;            
            cameraTransform.localRotation = originRotation;            
      }
   }
 
   public void Shake(){
 
      if(!shaking) {            
        originPosition = cameraTransform.localPosition;            
        originRotation = cameraTransform.localRotation;
      }
 
      shaking = true;        
      shake_intensity = originalIntensity;        
      shake_decay = originalDecay;
  }
	
	
	

}
