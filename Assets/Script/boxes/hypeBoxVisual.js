
function OnDrawGizmos () 
{

	Gizmos.color = Color(1,0,0.5,0.5);
	Gizmos.DrawCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	Gizmos.color = Color.magenta;
	Gizmos.DrawWireCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	
}