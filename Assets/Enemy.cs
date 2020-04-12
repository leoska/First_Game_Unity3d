using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int health = 100;
    [SerializeField]
    public GameObject fillHpBar;

    private bool _takeDmg = false;
    private float _takeDmgTime = 0f, _takeDmgTimeCoolDown = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_takeDmg)
        {
            _takeDmgTime -= Time.deltaTime;
            if (_takeDmgTime <= 0)
            {
                _takeDmg = false;
            }
        }
    }

    public void Damage(int dmg)
    {
        if (!_takeDmg)
        {
            _takeDmg = true;
            _takeDmgTime = _takeDmgTimeCoolDown;

            health -= dmg;
            fillHpBar.transform.localScale = new Vector3(health / 100f, 1f, 0f);
            //var fillHp = healthGroup.GetComponent<>
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
