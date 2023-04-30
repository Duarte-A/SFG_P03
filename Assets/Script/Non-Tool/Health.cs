using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 3;

    [SerializeField] private Shootable _shootable;
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private AudioSource _damagedSound;

    [SerializeField] private Text _currentScoreTextView;
    private int _currentScore;


    private void OnEnable()
    {
        // watch Shot event
        if(_shootable != null)
        {
        _shootable.Shot.AddListener(TakeDamage);
        }
    }

    private void OnDisable()
    {
        // stop watching shot event
        if (_shootable != null)
        {
            _shootable.Shot.RemoveListener(TakeDamage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (_impactParticle != null)
        {
            Instantiate(_impactParticle, transform.position, Quaternion.identity);
        }
        if (_damagedSound != null)
        {
            AudioSource newSound = Instantiate
                (_damagedSound, transform.position, Quaternion.identity);
            Destroy(newSound.gameObject, newSound.clip.length);
        }
        _currentHealth -= damageAmount;
        Debug.Log("Remaining Health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Die(5);
            Destroy(gameObject);
        }
    }

    public void Die(int scoreIncrease)
    {
        //Hide object
        gameObject.SetActive(false);

        //Add 5 points to player upon dying
        _currentScore += scoreIncrease;
        _currentScoreTextView.text =
            "Score: " + _currentScore.ToString();

        //If it is a new high score, then save it
        int highScore = PlayerPrefs.GetInt("High Score");
        if (_currentScore > highScore)
        {
            PlayerPrefs.SetInt("High Score", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }   
    }
}
