using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        var item = animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal).GetChild(0);
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
        
        item.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal));
        item.transform.localPosition = new Vector3(0.054f, 0.017f, -0.024f);
        item.transform.localEulerAngles = new Vector3(49.253f, 184.153f, -67.47601f);
        
        crosshair.SetActive(true);
    }

    private void SwapCollider()
    {
        gameObject.GetComponent<CapsuleCollider>().direction = 2;
    }

    private void Reload()
    {
        SceneManager.LoadScene(4);
    }
}
