using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importing the UnityEngine.AI namespace, which contains the NavMeshAgent class
using TMPro; // Importing the TMPro namespace, which contains the TextMeshProUGUI class
using UnityEngine.SceneManagement; // Importing the UnityEngine.SceneManagement namespace, which contains the SceneManager class

public class EnemyAi : MonoBehaviour // Defining a public class called EnemyAi that inherits from the MonoBehaviour class
{
    //Machine Learning Position changes
    [Header("Machine Learning Variables")] // Adding a header to group related variables
    public NavMeshAgent agent; // Public variable to store a reference to the NavMeshAgent component attached to the enemy object
    public Transform AIPosition; // Public variable to store a reference to the Transform component representing the enemy's position
    public Transform player; // Public variable to store a reference to the Transform component representing the player's position
    //public Vector3 AIenemyPosition;

    //different LayerMasks within the game
    [Header("In-Game LayerMasks")] // Adding a header to group related variables
    public LayerMask whatIsGround; // Public variable to store a reference to the layer mask for detecting what is considered as ground in the game
    public LayerMask whatIsPlayer; // Public variable to store a reference to the layer mask for detecting the player in the game
    public LayerMask whatIsBullet; // Public variable to store a reference to the layer mask for detecting bullets in the game
    //public GameObject zombieDeathTime;


    //player health variables
    [Header("Player Health Variables")] // Adding a header to group related variables
    public float playerhealth; // Public float variable to store the player's health
    public int newplayerhealth; // Public int variable to store the updated player's health

    //zombie health variables
    [Header("Zombie Health Variables")] // Adding a header to group related variables
    public float health; // Public float variable to store the enemy's health
    public float maxhealth; // Public float variable to store the maximum health of the enemy

    //Scene variables
    [Header("Scene Variables")] // Adding a header to group related variables
    public string changeScene; // Public string variable to store the name of the scene to change to

    //score variables
    [Header("Score Variables")] // Adding a header to group related variables
    public float score; // Public float variable to store the current score
    public float finalscore; // Public float variable to store the final score
    public TextMeshProUGUI newscore; // Public TextMeshProUGUI variable to store a reference to the text UI element displaying the score
    public GameObject updateScoreToDatabase; // Public GameObject variable to store a reference to the game object responsible for updating the score to the database

    //playerprefs score variables
    [Header("PlayerPrefs Score Variables")] // Adding a header to group related variables
    public string scoreKey = "Score"; // Public string variable to store the key to access the score in the player prefs
    public float CurrentScore { get; set; } // Public float property to get and set the current score in the player prefs

    public GameObject playerPrefsEnemyAI; // Declare a public GameObject variable to store reference to the playerPrefsEnemyAI object
    public float playerPrefsScoreEnemyAI; // Declare a public float variable to store the playerPrefsScoreEnemyAI value
    public float newAdditionalScore; // Declare a public float variable to store the newAdditionalScore value

    //Patroling section
    [Header("Zombie Walk Variables")] // Create a header in the Inspector view with the text "Zombie Walk Variables"
    public Vector3 walkPoint; // Declare a public Vector3 variable to store the walk point for the zombie
    bool walkPointSet; // Declare a boolean variable to check if the walk point is set
    public float walkPointRange; // Declare a public float variable to store the walk point range

    //Attacking section
    [Header("Zombie Attack Variable")] // Create a header in the Inspector view with the text "Zombie Attack Variable"
    public float timeBetweenAttacks; // Declare a public float variable to store the time between attacks by the zombie
    bool alreadyAttacked; // Declare a boolean variable to check if the zombie has already attacked
    public GameObject projectile; // Declare a public GameObject variable to store reference to the projectile object

    //Range section
    [Header("Zombie Range Variables")] // Create a header in the Inspector view with the text "Zombie Range Variables"
    public float sightRange; // Declare a public float variable to store the sight range of the zombie
    public float attackRange; // Declare a public float variable to store the attack range of the zombie

    //States section
    [Header("Zombie State Variables")] // Create a header in the Inspector view with the text "Zombie State Variables"
    public bool playerInSightRange; // Declare a public boolean variable to check if the player is within the zombie's sight range
    public bool playerInAttackRange; // Declare a public boolean variable to check if the player is within the zombie's attack range
    public static EnemyAi Instance; // Declare a public static variable to store the instance of the EnemyAi class

    private void Start()
    {
        //Set the initial health of the enemy to the maximum health
        health = maxhealth;

        //Get the current active scene
        Scene scene = SceneManager.GetActiveScene();
        //Log the name of the current active scene
        Debug.Log(scene.name);

        //If the current scene is "LiveGame"
        if (scene.name == "LiveGame")
        {
            //Get the score of the enemy AI stored in playerPrefs
            playerPrefsScoreEnemyAI = playerPrefsEnemyAI.GetComponent<updateScore>().playerPrefsScore;
            //Add the current score to the stored score
            playerPrefsScoreEnemyAI += score;
            //Save the updated score in playerPrefs
            PlayerPrefs.SetFloat("databaseScore", playerPrefsScoreEnemyAI);
        }
    }

    private void Awake()
    {
        //Get the current score stored in playerPrefs
        CurrentScore = PlayerPrefs.GetFloat(scoreKey);
        //Get the player object
        player = GameObject.Find("PlayerObj").transform;
        //Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        //Set the Instance variable to this script
        Instance = this;
        //Prevent the object from being destroyed when changing scenes
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        //Check if the player is in sight range of the enemy
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //Check if the player is in attack range of the enemy
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //If the player is not in sight range or attack range, the enemy will patrol
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        //If the player is in sight range but not in attack range, the enemy will chase the player
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //If the player is in both sight range and attack range, the enemy will attack the player
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        //Get the current health of the player
        GameObject go = GameObject.Find("Player");
        PlayerHealth cs = go.GetComponent<PlayerHealth>();
        float newplayerhealth = cs.health;

        //Get the position of the enemy AI
        Vector3 AIenemyPosition = GameObject.Find("Sphere(AI_Agent)").transform.position;
        //Set the position of the enemy AI to the position found above
        transform.position = AIenemyPosition;
    }
    private void Patroling()
    {
        //check if the walkpoint is already set
        if (!walkPointSet) 
            SearchWalkPoint(); // search for a walkpoint if it's not set

        //set the destination of the agent to the walkpoint
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        //get the distance from the enemy to the walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //check if the enemy has reached the walkpoint
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false; //reset walkpoint to false if reached
    }
    private void SearchWalkPoint()
    {
        //Calculate a random point in range for the walkpoint
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //set the walkpoint to the new random position
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //check if the new walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true; //set walkpoint to true if it's on the ground
    }

    private void ChasePlayer()
    {
        //set the destination of the agent to the player's position
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //set the destination of the agent to its current position
        agent.SetDestination(transform.position);

        //make the enemy look at the player
        transform.LookAt(player);

        //check if the enemy has already attacked
        if (!alreadyAttacked)
        {
            alreadyAttacked = true; //set alreadyAttacked to true
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //call ResetAttack after timeBetweenAttacks time
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false; //reset alreadyAttacked to false
    }
    public void TakeDamage(int damageAmount)
    {
        //decrease the health by damageAmount
        health -= damageAmount;
        //check if the health is less than or equal to 0
        if (health <= 0)
        {
            DestroyEnemy(); //destroy the enemy
        }
    }
    private void DestroyEnemy()
    {
        //Destroy the enemy game object
        Destroy(gameObject);
        //Find the "HealthCanvas" game object and get the component "timer"
        GameObject zombieDeathTime = GameObject.Find("HealthCanvas");
        score = (zombieDeathTime.GetComponent<timer>().CountDown) * 2;
        //Add the score to the final score
        finalscore += Mathf.Round(score);
        //Store the final score to a key in PlayerPrefs
        SetScore(Mathf.Round(finalscore));
        //Get the current active scene
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene);
        //Get the score stored in PlayerPrefs with the key "databaseScore"
        playerPrefsScoreEnemyAI = PlayerPrefs.GetFloat("databaseScore");
        //Calculate the new additional score
        newAdditionalScore = finalscore + playerPrefsScoreEnemyAI;
        //Store the new additional score in PlayerPrefs with the key "databaseScore"
        PlayerPrefs.SetFloat("databaseScore", newAdditionalScore);
        //Find the "UpdateScoreToDatabase" game object
        updateScoreToDatabase = GameObject.Find("UpdateScoreToDatabase");
        //Get the component "updateScore" and call the method "PerformupdateScoreSQL"
        updateScoreToDatabase.GetComponent<updateScore>().PerformupdateScoreSQL();
        //Load the scene specified in "changeScene"
        SceneManager.LoadScene(changeScene);
    }
    private void OnDrawGizmosSelected()
    {
        //Set the color of the gizmo to red and draw a wire sphere with a radius of "attackRange"
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //Set the color of the gizmo to yellow and draw a wire sphere with a radius of "sightRange"
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    public void SetScore(float score)
    {
        //Store the score in PlayerPrefs with the key "scoreKey"
        PlayerPrefs.SetFloat(scoreKey, score);
    }
}
