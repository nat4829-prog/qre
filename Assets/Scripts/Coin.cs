using UnityEngine; 
public class Coin : MonoBehaviour 
{ 
 public static int CoinCount = 0; 
 void Awake () 
 { 
 ++Coin.CoinCount; 
 } 
 void OnTriggerEnter(Collider Col) 
 { 
 if(Col.CompareTag("Player")) 
    Destroy(gameObject); 
 } 
 void OnDestroy() 
 { 
 --Coin.CoinCount; 
 if(Coin.CoinCount <= 0) 
 { 
 GameObject Timer = GameObject.Find("LevelTimer"); 
 Destroy(Timer); 
 GameObject[] FireworkSystems = 
 GameObject.FindGameObjectsWithTag("Fireworks"); 
 foreach(GameObject GO in FireworkSystems) 
 GO.GetComponent<ParticleSystem>().Play(); 
 } 
 } 
}