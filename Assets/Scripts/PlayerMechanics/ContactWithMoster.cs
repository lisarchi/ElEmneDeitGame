using UnityEngine;

public class ContactWithMoster : MonoBehaviour
{
    [SerializeField] private bool _contacted;
    private ShowMessage _message;
    private PlayerHealth _playerHealth;
    private ShowMonsterEyes _showEyes;

    private void Awake()
    {
        _contacted = false;
        _message = gameObject.GetComponentInChildren<ShowMessage>();
        _playerHealth = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerHealth>();
        _showEyes = gameObject.GetComponentInChildren<ShowMonsterEyes>();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _playerHealth.Damage();
            if (_showEyes !=null)
            {
                _showEyes.ShowEyes();
            }
            else
            {
                return;
            }
            _message.ShowRandomMessage();
        }
    }
        
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("ContactMonster");

            if (_contacted == true)
                return;

            if (_contacted == false)
            {
                
                _contacted = true;
            }
        }
    }
}
