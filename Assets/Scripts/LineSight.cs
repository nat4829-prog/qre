using UnityEngine; 
public class LineSight : MonoBehaviour 
{ 
public enum SightSensitivity {STRICT, LOOSE}; 
public SightSensitivity Sensitity = SightSensitivity.STRICT; 
public bool CanSeeTarget = false; 
public float FieldOfView = 45f; 
 private Transform Target = null; 
 public Transform EyePoint = null; 
 private Transform ThisTransform = null; 
 private SphereCollider ThisCollider = null; 
 public Vector3 LastKnowSighting = Vector3.zero; 
 void Awake() 
 { 
 ThisTransform = GetComponent<Transform>(); 
 ThisCollider = GetComponent<SphereCollider>(); 
 LastKnowSighting = ThisTransform.position; 
 Target = 
GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
 } 
 bool InFOV() 
 { 
 Vector3 DirToTarget = Target.position - EyePoint.position; 
 float Angle = Vector3.Angle(EyePoint.forward, DirToTarget); 
 if(Angle <= FieldOfView) 
    return true; 
 return false; 
 } 
 bool ClearLineofSight() 
 { 
 RaycastHit Info; 
    if(Physics.Raycast(EyePoint.position, (Target.position - 
EyePoint.position).normalized, out Info, ThisCollider.radius)) 
 { 
 if(Info.transform.CompareTag("Player")) 
 return true; 
 } 
 return false; 
 } 
 void UpdateSight() 
 { 
 switch(Sensitity) 
 { 
 case SightSensitivity.STRICT: 
 CanSeeTarget = InFOV() && ClearLineofSight(); 
    break; 
 case SightSensitivity.LOOSE: 
 CanSeeTarget = InFOV() || ClearLineofSight(); 
 break; 
    } 
 } 
 void OnTriggerStay(Collider Other) 
 { 
 UpdateSight(); 
 if(CanSeeTarget) 
LastKnowSighting = Target.position; 
} 
void OnTriggerExit(Collider Other) 
{ 
if(!Other.CompareTag("Player"))return; 
CanSeeTarget = false; 
} 
}