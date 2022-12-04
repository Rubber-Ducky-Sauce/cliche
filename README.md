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
