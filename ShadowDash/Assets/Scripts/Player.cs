using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("키눌리고있음");
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("키뗌");
        }
    }
}
