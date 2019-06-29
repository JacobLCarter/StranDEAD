using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody playerRB;
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "antigrav")
        {
            playerRB.useGravity = false;
        }
    }
}
