using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLasers : MonoBehaviour
{
    [SerializeField] GameObject laserProjectilePrefab;
    [SerializeField] GameObject laserTurret;
    [SerializeField] GameObject projectileCollector;
    [SerializeField] bool isHardMode = false;
    
    void Update()
    {
        FirePlayerLasers();
    }

    public void FirePlayerLasers(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(laserProjectilePrefab, laserTurret.transform.position, laserTurret.transform.rotation, projectileCollector.transform);
        }
    }

    public void SwitchLasers(){
        isHardMode = !isHardMode;
    }
}
