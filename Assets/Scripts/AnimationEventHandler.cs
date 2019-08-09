using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationEventHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    public GameObject crosshair;

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Footstep()
    {
        audioSource.Play();
    }

    private void ThrowPoint()
    {
        var item = animator.GetBoneTransform(HumanBodyBones.RightHand).GetChild(5);
        //TheInventoryItem myItem = gameObject.GetComponent<ItemClick>().Item;

        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.GetComponent<Rigidbody>().AddForce(transform.forward * 100f  + transform.up * 100f);
        crosshair.SetActive(false);
        //inventory.RemoveItem(myItem);
    }

    private void PickupPoint()
    {
        var item = GameObject.FindGameObjectWithTag("HeldItem");
        
        item.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal));
        item.transform.localPosition = new Vector3(-0.021f, -0.021f, -0.061f);
        item.transform.localEulerAngles = new Vector3(31.741f, -119.892f, 14.563f);
        
        crosshair.SetActive(true);
    }
}
