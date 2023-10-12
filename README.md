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

<br/><p>Bomb<p/>
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

<br/><p>Point<p/>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/e9717aef-db2c-495c-b515-3ae3e6b465e1" height="30%" width="30%">

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

<br/><p>Restart UI<p>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/9d527010-df23-42ce-99a5-5a044377e685" height="30%" width="30%">
<p>Setup animation for Restart UI</p>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/f9101ecb-17d9-4ca1-89c1-8daef2298cc1" height="30%" width="30%">


<p>Fruit<p/>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/170d2ed8-84c1-4660-b21c-613749185f3b" height="30%" width="30%">

```C#
    //Slice fruit will be trigger when player slash it
    if (other.gameObject.CompareTag("Player"))
    {
        fruitCollider.enabled = false;
        g_whole.SetActive(false);
        g_sliced.SetActive(true);

        Blade blade = other.GetComponent<Blade>();
        Slice(other, blade.force);
        GameManager.Instance.point += point;
    }

    //Slice logic function
    private void Slice(Collider other, float force)
    {
        fruitEffect.Play();
        //Grab player position
        Vector3 playerPosition = other.gameObject.transform.position;

        //Find the angle for slicing the fruit
        Vector2 difference = transform.position - playerPosition;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        g_sliced.transform.eulerAngles = Vector3.forward * angle;

        //Slice force
        float distance = difference.magnitude;
        Vector3 direction = difference / distance;
        rbs = g_sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.AddForceAtPosition(direction * force, other.transform.position,ForceMode.Impulse);
        }
    }
```

<br/><p>Spawn<p/>
<img src="https://github.com/ChristopherAngrico/FruitNinja/assets/87889745/15c1e020-6978-4698-ad5c-09ea514df352" height="30%" width="30%">

```C#
//Spawn will activate when the game start
spawnerCollider = GetComponent<BoxCollider>();

StartCoroutine(Spawn());

minBoundsX = spawnerCollider.bounds.min.x;
maxBoundsX = spawnerCollider.bounds.max.x;

// Spawn logic
//Delay for 2 second when start the game
yield return new WaitForSeconds(1f);
while (enabled)
{
    yield return new WaitForSeconds(1f);
    //Spawn fruit
    int index = Random.Range(0, fruits.Length);
    GameObject prefab = fruits[index];

    //Spawn bomb
    if(Random.value < spawnBombChance)
    {
        prefab = bomb;
    }

    //Grab original position
    Vector3 newPosition = transform.position;
    newPosition.x = Random.Range(minBoundsX, maxBoundsX); //clamp random spawn position

    float angle = Random.Range(minAngle, maxAngle);
    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    GameObject g = Instantiate(prefab, newPosition, rotation);
    Rigidbody rb = g.GetComponent<Rigidbody>();

    float force = Random.Range(minForce, maxForce);

    rb.AddForce(force * g.transform.up, ForceMode.Impulse);

    Destroy(g, maxLifeTime);
}

//Spawn will stop if game change to another scene or quit the game
StopAllCoroutines();
```

## Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| Left Click        | Slashing blade      |


