using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{

    public int edgeId;
    ParentEdgeDetector parentEdgeDetector;
    Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        parentEdgeDetector = transform.parent.GetComponent<ParentEdgeDetector>();
        playerRb=PlayerController.PlayerRB;
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

    }

    private void OnTriggerExit2D(Collider2D collider)
    {

    }


}
