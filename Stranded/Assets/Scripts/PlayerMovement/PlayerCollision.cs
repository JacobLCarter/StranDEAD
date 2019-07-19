using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody playerRB;
    public Rigidbody portal1;
    public Rigidbody portal2;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Portal1")
        {
            playerRB.transform.position = portal2.position;
        }
    }
}
