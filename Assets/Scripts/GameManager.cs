﻿using UnityEngine;

public class GameManager : MonoBehaviour{

    [Header("Enemies Prefabs")]
    public int zombieCount;
    public EnemyController zombie;
    public float limitX = 22;
    public float limitZ = 22;
    public float playerSafeSpawnX = 4; 
    public float playerSafeSpawnZ = 4; 

    //Private
    private float positionX = 22;
    private float positionZ = 22;


    void Start(){
        for (int count = 0; count <= zombieCount; count++){

            do{
                positionX = Random.Range(limitX * (-1), limitX);
            } while (positionX <= playerSafeSpawnX && positionX >= (playerSafeSpawnX*-1));

            do{
                positionZ = Random.Range(limitZ * (-1), limitZ);
            } while (positionZ <= playerSafeSpawnZ && positionZ >= (playerSafeSpawnZ*-1));

            EnemyController newEnemie = Instantiate(zombie, new Vector3(positionX, zombie.transform.position.y, positionZ), zombie.transform.rotation) as EnemyController;
        }
    }
   
    void Update(){
        
    }
}
