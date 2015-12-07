
function OnDrawGizmos () 
{

	//Gizmos.color = Color(1,1,0,0.5);
	//Gizmos.DrawCube(transform.position, collider.size);
	Gizmos.color = Color.yellow;
	Gizmos.DrawWireCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	
}