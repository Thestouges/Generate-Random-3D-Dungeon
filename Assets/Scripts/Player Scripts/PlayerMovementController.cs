using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovementController : MonoBehaviour
{
    public float PlayerSpeed = 1;
    Rigidbody Rigidbody;
    bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody.velocity += -transform.right * PlayerSpeed;
            //transform.Translate(-Vector2.right * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody.velocity += transform.right * PlayerSpeed;
            //transform.Translate(Vector2.right * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody.velocity += -transform.forward * PlayerSpeed;
            //transform.Translate(-Vector3.forward * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody.velocity += transform.forward * PlayerSpeed;
            //transform.Translate(Vector3.forward * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime * PlayerSpeed/4);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -200) * Time.deltaTime * PlayerSpeed/4);
        }
        if (Input.GetMouseButtonDown(0) && clicked == false)
        {
            SceneManager.LoadScene("SampleScene");
            clicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }
    }
}
