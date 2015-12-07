
function OnDrawGizmos () 
{

	Gizmos.color = Color(1,0,0,0.5);
	Gizmos.DrawCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	Gizmos.color = Color.red;
	Gizmos.DrawWireCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	
}