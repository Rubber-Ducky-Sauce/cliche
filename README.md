<h1>Another Castle</h1>
<hr>

<p>A Retro style Stealth platformer in which, our hero, The Princess, must escape another castle using tactics the like of Super Mario and Solid Snake</P>
<hr>

<h2>Player</h2>
<p> The player controller inherits from an Actor script, providing movement and base speed.</P>
<p> Player requires an audiosource, rigidBody component, animator and sprite renderer.</P>
<p> The player also requires an assignable "notification" sprite that is used to notify the user they may use the "interact" action</P>
<p> There are also several audioclips required for specific actions</P>
<p> The player is active when GameManager and the player's own playerActive booleans are set to true</P>
<p> Player can move horizontally, Jump up, climb, jump down from "climbables", interact with "interactables" and use certain "collectables".</P>
<p> For Jumping to occur, there must be a "ground" layer applied to surfaces for a raycast to percept</P>
<p> There are also several audioclips required for specific actions</P>
<h3>Hiding</h3>
<p> The player can interact with Hide Interactables that render her invisble to enemies</P>
<p> The player can can also sneak, slowing down her movement but allowing her to creep behind enemies without being heard</P>
<hr>

<h2>Enemies</h2>
<p> Enemies also require auidiosource, several audio clips, animator, an "alert" sprite object, and an Ignore layer set to "Interactable"</P>
<p> The enemy Script contains logic for movement, detecting walls/ground and detecting the player with a raycast. The enemy defaults to moving back and forth until they hit a wall or gap in the ground but may be set to pace back and forth a specific distance</P>
<p> The enemy script has public methods that can be called from other attached scripts. The methods handle the enemy being alerted, being distracted and settling down</P>
<h3>HearPlayer</h3>
<p> The HearPlayerScript sets the enemy to alert based on player movement and proximity</P>
<h3>ShowDistances</h3>
<p> The ShowDistances script allows a live visual of enemy movement, sight and hearing distance of enemies for easier modification</P>
<hr>

<h2>GameManager and Management</h2>
<p> The Game Manager is set as an instance, handles many tasks.</P>
<p> Plays oneshot sounds from objects no longer able to do so.</P>
<p> Handles Current Interactable, for player, as well as active key</P>
<p> Handles Current CheckPoint when player is caught. is a string value</P>
<p> Holds current collectable to store for refeerence in UI</P>
<p> Is connected to Instance of Music Manager</P>
<p> Handles scene management </P>

<h3>ButtonThroughKeySelection</3>
<p> Ensures a Start Menu button will always be selected</P>

<h3>CutSceneManagement</h3>
<p> Dialog System not yet complete</p>
<p> SceneDirector to be used with Timeline Component to trigger GameManager gameActive boolean and scene management</p>
<p> CutsceneTrigger triggers sceneDirectors/cutscene to active. Ther are two versions. A collider based, that will trigger on player collision and an interactable that will trigger on player's interact action of connect interactable</p>
<hr>

<h2>Level Objects</h3>
<p> Checkpoints set in the hierarchy, trigger state change. On level restart, game finds this object and location to set player position</p>
<p> SceneTrigger is a collider based scene management tool</p>
<p> Interactable, abstract class. Sets GM state. When player is within collider, player may "interact" using method of GM currentInteractable</p>
<p> Collectable, abstract class. populates UI element. Only one collectable can be held. When a second collectable is attained, the last is tossed aside, ready to be collected again, but now with a RigidBody</p>
<p> Climables are interactables that when activated(by going up) change gravity settings</p>
<p>Throwable, an abstract collectable that the player can toss forward away from them</p>
<p> The Coin is a throwable item that distracts an enemy, stopping them from being able to spot the player for a short time</p>
<p> EyeShield has been depracated</p>
<p> HidingPlace, allows player to hide from enemy view until moving from hiding</p>
<p> Key and Lock scripts contain strings, when matched set a trigger on player activation</p>
<p> Light Controller applies a light that follows player to help simulate hiding</p>
<p> Switch is a player activated trigger meant for a physical switch or button the player is to jump on</p>
<p> WANDERING EYE, almost an enemy, the wandering eye scans a distance with a thin raycast in order to spot the player. Other colliders can impede view. A light should be attached to this for the player to notice where the ray is pointing</p>
<hr>

<h2>Level Building</h2>
<p> Level make-up is being handled with Tilemaps using several layers</p>
<ul>
  <li>background</li>
  <li>backgrounddecoration</li>
  <li>interactables</li>
  <li>foreground</li>
  <li>foregrounddecoration</li>
</ul>
<hr>

<h2>Purchased/Borrowed Assets</h2>
<h3>Music</h3>
<p>JDB-GlintJDB-Glint by JDB Artist</P>
<p>Midnight Creeping Composed by Jonathan Shaw</P>

<h3>Visual Art</h3>

<p>key and diamond from Items by Simirk</P>
<p>Tiles and crates by Hamdirizal </P>
<p>smoke(mineral) by 7Soul</P>

<h3>Sound FX</h3>

<p>Female RPG Voice Starte Pack by cicifyre</P>
<p>goblin sound pack by artisticdude</P>
<p>various sounds by ZStriefel</P>
<p>footsteps sounds by Estudiocafofo</P>
<p>SteamWhistle sound by bart </P>
