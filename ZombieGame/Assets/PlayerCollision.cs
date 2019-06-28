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
        if (other.collider.tag == "tree")
        {
            playerRB.useGravity = false;
            playerRB.AddForce(0,5000f * Time.deltaTime, 0);
        }
    }
}
