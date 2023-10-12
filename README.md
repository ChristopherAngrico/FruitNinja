# Fruit ninja
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/4487c494-8582-4e13-b643-f8c89beeaf00" height="70%" width="70%">

## Description
Remake simple 3D Fruit ninja game

## Game Mechanic
<p>Blade<p/><br/>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/bb725caa-e1cb-4d18-a9fe-19af2d2d915a" 30%" width="30%">

```C#
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartSlicing();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopSlicing();
        }
        else if (onSlicing)
        {
            ContinueSlicing();
        }

    }
    private void StartSlicing()
    {
        //Add new position for avoid bug trail
        Vector3 newPosition = Input.mousePosition;
        newPosition.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
        transform.position = worldPosition;

        sliceTrail.enabled = true;
        bladeCollider.enabled = true;
        onSlicing = true;
        sliceTrail.Clear();

    }
    private void StopSlicing()
    {
        bladeCollider.enabled = false;
        sliceTrail.enabled = false;
        onSlicing = false;
    }
    private void ContinueSlicing()
    {


        Vector3 mousePosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;

        //Blade collider will enable / disable depend on velocity of blade 
        Vector3 difference = worldPosition - transform.position;
        float velocity = difference.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minVelocity;

        transform.position = worldPosition;
    }
```

<p>Bomb<p/><br/>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/8de4090f-2a2e-49f4-b3f2-52e91ef9a64a" height="30%" width="30%">


```c#
//Part of bomb class logic
private void OnTriggerEnter(Collider other)
{
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Fading();
            UIGameOver.SetEnableGameOver();
        }
}

//Part of fading class logic
if (GameManager.Instance.fadeIn == true)
        {
            _group.alpha += Time.deltaTime;
            if ((1 - _group.alpha) < threshold)
            {
                _group.alpha = 1;
                GameManager.Instance.fadeIn = false;
                fadeOut = true;
                GameManager.Instance.ClearScene();
            }
        }
        if (fadeOut == true)
        {
            _group.alpha -= Time.deltaTime;
            if ((_group.alpha) < threshold)
            {
                _group.alpha = 0;
                fadeOut = false;
            }
}
```

<p>Point<p/><br/>
<img src="" height="30%" width="30%">


```C#
 private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.text = GameManager.Instance.point.ToString();
    }
```

<p>Restart UI<p/><br/>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/9d527010-df23-42ce-99a5-5a044377e685" height="30%" width="30%">
<p>Setup animation</p>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/f9101ecb-17d9-4ca1-89c1-8daef2298cc1" height="30%" width="30%">


```C#
private void FlippingSprite()
    {
        //Flipping sprite by following mouse direction
        if (differenceXPosition < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
```


<p>Boss1 Attack<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/86130c7f-67c9-4ae1-b9ad-826727822875" height="30%" width="30%">

```C#
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {

            damagePlayer = true;
            if (GameObject.FindWithTag("Clone") != null)
            {
                damageClone = true;
            }
        }
    }
```

<p>Boss1 Walk<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/475a1834-14e0-4b3a-a736-9e804c5007c4" height="30%" width="30%">

```C#
private void MoveTowardPlayer()
    {
        float speed = 5f;
        if (playerDetect != null && !triggerAttack && !idleState)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerDetect.transform.position, speed * Time.deltaTime);
            isRunning = true;
        }
        else
        {
            transform.position += Vector3.zero;
            isRunning = false;
        }
    }
```

<p>Boss1 die<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/feb9907e-a63e-4fd9-bd3c-7950b8e6c5ad" height="30%" width="30%">

```C#
private void FixedUpdate()
    {
        if (healthSystem.GetHealth() == 0)
        {
            detectPlayer.enabled = false;
            StartCoroutine(Animated());
        }
    }

    private IEnumerator Animated()
    {
        isDying = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        if (gameObject.tag == "Boss2")
        {
            yield return new WaitForSeconds(1);
            GameManager.instance.Finished();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
```

<p>Point<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/86071b8f-42d8-4625-88bb-73ae58c17b82" height="30%" width="30%">

```c#
foreach (GameObject g_Enemy in g_Enemies)
        {
            if (g_Enemy != null)
            {
                if (g_Enemy.GetComponentInChildren<HitPlayer>().damagePlayer)
                {
                    int enemyDamage = damage;
                    if (shieldSystem.GetHealth() <= 0)
                    {
                        DecreaseHealth(healthSystem, enemyDamage);
                        g_health.transform.localScale = GetHealtBar(healthSystem);
                    }
                    else
                    {
                        DecreaseHealth(shieldSystem, enemyDamage);
                        g_shield.transform.localScale = GetHealtBar(shieldSystem);
                        UpdateTheUpgradeShield();
                        lastValueShield = shieldSystem.GetHealth();
                    }
                    g_Enemy.GetComponentInChildren<HitPlayer>().damagePlayer = false;
                    //Player will received point when getting hit by enemy
                    GameManager.instance.playerPoint += healthSystem.GetPointFromEnemyHit(enemyDamage);
                }
            }
        }
```

<p>Upgrade UI<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/e365da18-d551-4479-9c82-08d5834c2566" height="30%" width="30%">

```c#
public class Upgrade_Button : MonoBehaviour
{
    [SerializeField]private GameObject g_UpgradeButton;
    public void Upgrade_UI(){
        Time.timeScale = 0;
        g_UpgradeButton.SetActive(true);
    } 
    
}
```

<p>Boss2 Attack<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/b8d06361-696d-49d4-b44d-1479694e5e6a" height="30%" width="30%">

```C#
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Clone"))
        {

            damagePlayer = true;
            if (GameObject.FindWithTag("Clone") != null)
            {
                damageClone = true;
            }
        }
    }
```

<p>Boss2 walk<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/38b54c18-e735-4d8e-91b5-4d98b6f5f35f" height="30%" width="30%">

```C#
private void MoveTowardPlayer()
    {
        float speed = 5f;
        if (playerDetect != null && !triggerAttack && !idleState)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerDetect.transform.position, speed * Time.deltaTime);
            isRunning = true;
        }
        else
        {
            transform.position += Vector3.zero;
            isRunning = false;
        }
    }
```

<p>Boss2 die<p/><br/>
<img src="https://github.com/ChristopherAngrico/Purgatory/assets/87889745/ed864e71-65b6-41ca-9d96-c6f778c7a968" height="30%" width="30%">

```C#
private void FixedUpdate()
    {
        if (healthSystem.GetHealth() == 0)
        {
            detectPlayer.enabled = false;
            StartCoroutine(Animated());
        }
    }

    private IEnumerator Animated()
    {
        isDying = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        if (gameObject.tag == "Boss2")
        {
            yield return new WaitForSeconds(1);
            GameManager.instance.Finished();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
```

## Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| Left Click        | Slashing blade      |


