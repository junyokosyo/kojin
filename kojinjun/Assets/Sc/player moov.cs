using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermoov : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;//�ړ����x
    private Rigidbody rb; �@//Rigidbody�̊֐�

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Input.GetKeyDown("");
    }
}
