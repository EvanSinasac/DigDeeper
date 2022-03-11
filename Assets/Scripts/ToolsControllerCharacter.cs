using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Matthew Pizzo - 
 * Last Worked On: January 17, 2021
 * */

public class ToolsControllerCharacter : MonoBehaviour
{
    [SerializeField] PlayerMovement character;
    [SerializeField] Rigidbody2D rb;
    ToolbarController toolbarController;
    //[SerializeField] float offsetDistance = 1f;
    //[SerializeField] float sizeOfInteractableArea = 1.2f;

    Animator animator;

    private void Awake()
    {
        character = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.timeScale == 1f && !this.gameObject.GetComponent<PlayerMovement>().shopping)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                UseTool();
            }

        }
    }

    private bool UseTool(){
        //Vector2 position = rb.position + character.lastMotionVector;
        //Debug.Log("Position Location: x=" + position.x + " y=" + position.y);
        Item item = toolbarController.GetItem;
        if (item == null) { return false; }
        if (item.OnAction == null) { return false; }

        
        if (!item.consumable && !item.stackable)
        {
            animator.SetTrigger("Attack");
        } /*else if (item.consumable && item.stackable)
        {
            //trigger animation for bomb/consumable (prolly gunna need a new boolean to track the bomb/what is being eaten/drunk)
        }*/

        bool complete = item.OnAction.OnApply(rb.position);

        if (item.consumable) {
            GameManager.instance.inventoryContainer.Remove(item, 1);
        }

        return false;
    }

    /*public void OnDrawGizmos()
    {
        Vector2 positionG = rb.position + character.lastMotionVector * offsetDistance;
        //Debug.Log("Position Gizmo Location: x=" + positionG.x + " y=" + positionG.y);
        Gizmos.DrawWireSphere(positionG, sizeOfInteractableArea);
    }*/
}
