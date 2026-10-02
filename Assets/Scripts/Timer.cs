using UnityEngine; 
public class Timer : MonoBehaviour 
{ 
public float MaxTime = 60f; 
[SerializeField] 
private float CountDown = 0; 
void Start () 
{ 
CountDown = MaxTime; 
} 
void Update () 
{ 
CountDown -= Time.deltaTime; 
if(CountDown <= 0) 
{ 
Application.LoadLevel(Application.loadedLevel); 
} 
} 
}