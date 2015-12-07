
function OnDrawGizmos () 
{

	Gizmos.color = Color(0,0,8.8,0.5);
	Gizmos.DrawCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	Gizmos.color = Color.blue;
	Gizmos.DrawWireCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	
}