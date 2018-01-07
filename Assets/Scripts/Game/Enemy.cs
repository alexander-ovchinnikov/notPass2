using UnityEngine;
using UnityEngine.Events;


namespace Game
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        public UnityEvent OnDie = new UnityEvent();
        [SerializeField]
        private int _health = 10;

        public int Health
        {
            get { return _health; }
            set { 
                _health = value;
                if (_health <= 0)
                {
                    Die();
                }
            }
        }

        public void Die()
        {
            OnDie.Invoke();
            Destroy(this.gameObject);
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame

        void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Die();
            }
        }

        void Update()
        {
        }

        public void GetHit(int damage)
        {
            Health -= damage;

        }
    }
}