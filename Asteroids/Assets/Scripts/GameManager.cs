using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int lives = 3;
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text lifeScore;


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position; 
        this.explosion.Play();

        if (asteroid.size < 0.75f) {
            this.score += 100;
        } else if (asteroid.size < 1.0f){
            this.score += 50;
        } else {
            this.score += 25;
        }

        scoreText.text = this.score.ToString();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position; 
        this.explosion.Play();

        this.lives--;
        if(this.lives < 0)
        {
            SceneManager.LoadScene("GameOver");
        } else {
            Invoke(nameof(Respawn), this.respawnTime);

        }

        lifeScore.text = this.lives.ToString();

    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

}
