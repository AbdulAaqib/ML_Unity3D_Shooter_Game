using UnityEngine;
using TMPro;
public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake cameraShake;
    public float cameraShakeMagnitude, cameraShakeDuration;
    public TextMeshProUGUI text;
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    //This method checks for user input and performs the appropriate action.
    private void MyInput()
        {
            //Checks if the user is holding down the mouse button and sets shooting to true if they are.
            if (allowButtonHold) shooting = Input.GetKeyDown(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);
            //Checks if the user has pressed R and that there are bullets left in the magazine, and calls Reload() if both conditions are true. 
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
            //Checks if the gun is ready to shoot, that the user is pressing the mouse button, that it isn't reloading, 
            //and that there are bullets left in the magazine, then calls Shoot() if all conditions are true. 
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }
    //Shoot method to fire a bullet from the gun
    private void Shoot()
    {
        //Set readyToShoot to false
        readyToShoot = false;

        //Generate random x and y values between -spread and spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction vector by adding random x and y values to the forward direction of the camera
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //Check if raycast hits an enemy within range and whatIsEnemy layermask
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            //Log name of hit object in console
            Debug.Log(rayHit.collider.name);

            //Check if hit object has Enemy tag and deal damage if true
            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<EnemyAi>().TakeDamage(damage);
            }
        }

        //Instantiate bullet hole graphic at hit point with 180 degree rotation on Y axis 
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

        //Instantiate muzzle flash at attackPoint position with no rotation 
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        //Decrement bulletsLeft and bulletsShot by 1 
        bulletsLeft--; 
        bulletsShot--;

        //Invoke ResetShot method after timeBetweenShooting seconds 
        Invoke("ResetShot", timeBetweenShooting);

        //If there are still bullets left and shots to be fired invoke Shoot method after timeBetweenShots seconds 
        if(bulletsShot > 0 && bulletsLeft > 0) 
        Invoke("Shoot", timeBetweenShots); 
    }
    //ResetShot() resets the readyToShoot boolean to true, allowing the player to shoot again.
    private void ResetShot()
    {
        //Set readyToShoot to true, allowing the player to shoot again.
        readyToShoot = true;
    }

    //Reload() sets the reloading boolean to true and invokes ReloadFinished after a set amount of time. 
    private void Reload()
    {
        //Set reloading boolean to true. 
        reloading = true;

        //Invoke ReloadFinished after reloadTime has passed. 
        Invoke("ReloadFinished", reloadTime);
    }

    //ReloadFinished() sets bulletsLeft to magazineSize and sets reloading boolean to false. 
    private void ReloadFinished()
    {
        //Set bulletsLeft to magazineSize. 
        bulletsLeft = magazineSize;

        //Set reloading boolean to false. 
        reloading = false; 
    }
}