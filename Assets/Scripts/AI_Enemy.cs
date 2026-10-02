using UnityEngine; 
using System.Collections; 
public class AI_Enemy : MonoBehaviour 
{ 
 public enum ENEMY_STATE {PATROL, CHASE, ATTACK}; 
 public ENEMY_STATE CurrentState 
 { 
 get{return currentstate;} 
 set 
 { 
 currentstate = value; 
 StopAllCoroutines(); 
 switch(currentstate) 
 { 
 case ENEMY_STATE.PATROL: 
 StartCoroutine(AIPatrol()); 
    break; 
 case ENEMY_STATE.CHASE: 
 StartCoroutine(AIChase()); 
 break; 
 case ENEMY_STATE.ATTACK: 
 StartCoroutine(AIAttack()); 
 break; 
 } 
 } 
 } 
 [SerializeField] 
 private ENEMY_STATE currentstate = ENEMY_STATE.PATROL; 
 private LineSight ThisLineSight = null; 
 private UnityEngine.AI.NavMeshAgent ThisAgent = null; 
 private Health PlayerHealth = null; 
 private Transform PlayerTransform = null; 
 private Transform PatrolDestination = null; 
 public float MaxDamage = 10f; 
 void Awake() 
 { 
 ThisLineSight = GetComponent<LineSight>(); 
 ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
 PlayerHealth = 
GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); 
 PlayerTransform = PlayerHealth.GetComponent<Transform>(); 
 } 
 void Start() 
 { 
 GameObject[] Destinations = GameObject. 
 FindGameObjectsWithTag("Dest"); 
    PatrolDestination = Destinations[Random.Range(0, 
Destinations.Length)].GetComponent<Transform>(); 
 CurrentState = ENEMY_STATE.PATROL; 
 } 
 public IEnumerator AIPatrol() 
 { 
 while(currentstate == ENEMY_STATE.PATROL) 
 { 
 ThisLineSight.Sensitity = LineSight.SightSensitivity.STRICT; 
 ThisAgent.Resume(); 
 ThisAgent.SetDestination(PatrolDestination.position); 
 while(ThisAgent.pathPending) 
    yield return null; 
 if(ThisLineSight.CanSeeTarget) 
 { 
 ThisAgent.Stop(); 
 CurrentState = ENEMY_STATE.CHASE; 
 yield break; 
 } 
 yield return null; 
    } 
 } 
 public IEnumerator AIChase() 
 { 
 while(currentstate == ENEMY_STATE.CHASE) 
 { 
 ThisLineSight.Sensitity = LineSight.SightSensitivity.LOOSE; 
 ThisAgent.Resume(); 
 ThisAgent.SetDestination(ThisLineSight.LastKnowSighting); 
 while(ThisAgent.pathPending) 
    yield return null; 
 if(ThisAgent.remainingDistance <= ThisAgent.stoppingDistance) 
 { 
 CurrentState = ENEMY_STATE.ATTACK; 
 ThisAgent.Stop(); 
 if(!ThisLineSight.CanSeeTarget) 
    CurrentState = ENEMY_STATE.PATROL; 
 else 
    CurrentState = ENEMY_STATE.ATTACK; 
 yield break; 
 } 
 yield return null; 
    } 
 } 
 public IEnumerator AIAttack() 
 { 
 while(currentstate == ENEMY_STATE.ATTACK) 
 { 
 ThisAgent.Resume(); 
 ThisAgent.SetDestination(PlayerTransform.position); 
 while(ThisAgent.pathPending) 
 yield return null; 
 if(ThisAgent.remainingDistance > ThisAgent.stoppingDistance) 
 { 
 CurrentState = ENEMY_STATE.CHASE; 
 yield break; 
 } 
 else 
 { 
 PlayerHealth.HealthPoints -= MaxDamage * Time.deltaTime; 
 } 
 yield return null; 
 } 
 yield break; 
 } 
} 