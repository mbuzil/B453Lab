using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [Tooltip("Name of the weapon.")]
    [SerializeField] protected string gunName;
    [Tooltip("Current bullets loaded into the weapon.")]
    [SerializeField] protected int currentBullets;
    [Tooltip("The maximum amount of bullets this weapon can hold at once.")]
    [SerializeField] protected int maxBullets;
    [SerializeField] protected int inventory;
    protected int bulletsUsed = 0;
    [SerializeField] TMP_Text fullText;
    [SerializeField] TMP_Text otherText;
    [SerializeField] GameObject enemy;

    [Header("References")]
    [Tooltip("Decal to use for bullet holes.")]
    [SerializeField] protected GameObject bulletHole;
    [SerializeField] GameObject muzzleFlash;

    private float flashTime = 0.05f;
    private bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        fullText.SetText(inventory.ToString());
        otherText.SetText(currentBullets.ToString() + "/" + maxBullets.ToString());

        if(isFlashing)
        {
            flashTime -= Time.deltaTime;
            if(flashTime <= 0)
            {
                muzzleFlash.SetActive(false);
                isFlashing = false;
                flashTime = 0.05f;
            }
        }

    }

    // Fire a bullet from the weapon.
    public virtual void Shoot()
    {
        // Check to make sure there are bullets loaded into the gun.
        if (currentBullets > 0)
        {
            // Define a RaycastHit object (this stores data about an object that a raycast hits.)
            RaycastHit hit;
            
            // Shoot the raycast from the main camera, in the forward direction the camera is facing, store the data in "hit" and the ray distance is infinite.
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                // Instantiate the decal gameobject for the bullet hole at the location the ray hit and save a reference to it.
                GameObject decal = Instantiate(bulletHole, hit.point, Quaternion.identity);
                // Make the forward (Z) axis of the decal face in the opposite direction of the normal vector of the thing that it hit.
                decal.transform.forward = -hit.normal;
                // Move the decal backwards 1 centimeters away from the surface that it hit.
                decal.transform.Translate(Vector3.back * 0.01f);

                // Save the world position of the decal.
                Vector3 worldPosition = decal.transform.position;
                // Save the world rotation of the decal.
                Quaternion worldRotation = decal.transform.rotation;
                // Make the object the ray hit the parent of the decal. This is necessary to make sure the bullethole stays on the surface of the object if the object moves.
                // NOTE: This will automatically move the bullethole to the incorrect spot because its location and rotation are now relative to the parent.
                decal.transform.SetParent(hit.transform);
                // Move the bullethole back to the right position it was in before.
                decal.transform.position = worldPosition;
                // Move the bullethole back to the right rotation it was in before.
                decal.transform.rotation = worldRotation;

               
                hit.collider.gameObject.GetComponent<Enemy>().Die();

                // Subtract one bullet from the bullets that are currently loaded in the weapon.
                currentBullets--;
                bulletsUsed++;
                flashTimer();
            }
        }
    }

    public void flashTimer()
    {

        muzzleFlash.SetActive(true);
        isFlashing = true;

    }
    // Reload the weapon.
    // NOTE: Change all of this to instead return the number of rounds used so when the player reloads it can subtract that many rounds from their inventory.
    public virtual void Reload()
    {
        if (bulletsUsed > 0)
        {
            if (inventory > bulletsUsed)
            {
                inventory -= bulletsUsed;
                currentBullets += bulletsUsed;
            }
            else
            {
                currentBullets += inventory;
                inventory -= inventory;
            }
        }
        bulletsUsed = 0;
    }
}