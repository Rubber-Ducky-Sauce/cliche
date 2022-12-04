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

<h2>GameManager</h2>
<p> </P>
