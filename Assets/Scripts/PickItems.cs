using UnityEngine; 
public class PickItems : MonoBehaviour 
{ 
public string itemName = "Some Item"; 
public Texture itemPreview; 
void Start() 
{ 
    gameObject.tag = "Respawn"; 
 } 
 public void PickItem() 
 { 
 Destroy(gameObject); 
 } 
}