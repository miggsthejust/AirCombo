
function OnDrawGizmos () 
{
	
	Gizmos.color = Color.green;
	Gizmos.DrawWireCube(transform.position, Vector3(GetComponent.<Collider>().size.z,GetComponent.<Collider>().size.y,GetComponent.<Collider>().size.x));
	
}