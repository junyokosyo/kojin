using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moovGound : MonoBehaviour
{
    [SerializeField]
    Transform[] _targets;　//チェックポイント
    [SerializeField]
    float _movesped = 2f;
    [SerializeField]
    float _stoppingDistance = 0.05f;
    int _currentTargetIndex = 0;
    [SerializeField]
    private string _playerTag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        float distance = Vector2.Distance(this.transform.position, _targets[_currentTargetIndex].position);

        if (distance > _stoppingDistance)
        {
            Vector3 dir = (_targets[_currentTargetIndex].transform.position - this.transform.position).normalized * _movesped;
            this.transform.Translate(dir * Time.deltaTime);
        }

        else
        {
            _currentTargetIndex++;
            _currentTargetIndex %= _targets.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(_playerTag))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_playerTag))
        {
            collision.transform.parent = null;
        }
    }
}
