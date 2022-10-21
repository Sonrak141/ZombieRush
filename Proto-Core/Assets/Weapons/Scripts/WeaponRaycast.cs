using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : WeaponBase
{
	[Header("Melee")]
	[SerializeField] float forwardRange;
	[SerializeField] float horizontalRange;
	[SerializeField] float vertcialRange;
	[SerializeField] Transform hitPoint;


	[Header("weapon info")]
	[SerializeField] Transform shootPoint;
	[SerializeField] bool debugShoot;
	[SerializeField] float scatterAngle;

	[SerializeField] bool canShoot;
	[SerializeField] bool canShootContinously;
	[SerializeField] Animator animator;
	

	[Header("Sounds")]
	[SerializeField] AudioSource ShootSound;

	bool isShootingContinuos;

	float timeForNextShoot = 0f;
	[SerializeField] float shotsPerSecond = 5f;

private void Update()
{
	if (isShootingContinuos){
		timeForNextShoot -= Time.deltaTime;
		if (timeForNextShoot <= 0f){
			internalShoot();
			timeForNextShoot += 1f/shotsPerSecond;
		}
	}
}

private void OnValidate() //En general no usar el OnValidate
{
	if(debugShoot){
		Shoot();
		debugShoot = false;
}}

public override void Shoot()
{
	if (canShoot){
		internalShoot();
			animator.SetTrigger("Shoot");
	}
}
protected void internalShoot()
	{
		RaycastHit hit; //
		
		// Tipos de cast
		// Physics.Boxcast
		// Physics.Spherecast
		// Physics.Capsulecast
		float horizontalScatterAngle = Random.Range(-scatterAngle, scatterAngle);
		float verticalScatterAngle = Random.Range(-scatterAngle, scatterAngle);
		Quaternion horizontalScatterShoot = Quaternion.AngleAxis(horizontalScatterAngle, shootPoint.up);
		Quaternion verticalScatterShoot = Quaternion.AngleAxis(verticalScatterAngle, shootPoint.right);
		Vector3 shootForward = verticalScatterShoot*(horizontalScatterShoot*shootPoint.forward);
		if (Physics.Raycast(shootPoint.position, shootForward, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) //Esto lanzara un rayo
			{
				Debug.Log("le he dado a " + hit.collider);
				Debug.DrawRay(hit.point, hit.normal, Color.red, 10f);
				Debug.DrawRay(shootPoint.position, hit.point, Color.cyan, 10f);
				//dawray es para debug no mas para que salga en el juego se una un Line Renderer11
				TargetBase targetBase = hit.collider.GetComponent<TargetBase>();
				
				targetBase?.NotifyShot();
				
			}
		ShootSound?.Play();
	}
    public override void StartShooting()
    {
		isShootingContinuos = canShootContinously;
        base.StartShooting();
    }

    public override void StopShooting()
    {
		isShootingContinuos = false;
        base.StopShooting();
    }
	public override void Swing()
	{

		Vector3 halfExtends = new Vector3(horizontalRange / 2f, vertcialRange / 2f, forwardRange / 2f);
		Collider[] colliders = Physics.OverlapBox(transform.position, halfExtends, transform.rotation);
		foreach (Collider c in colliders)
		{
			TargetBase target = c.GetComponent<TargetBase>();
			animator.SetTrigger("Melee");

			target?.NotifySwing();
		}
	}
}
