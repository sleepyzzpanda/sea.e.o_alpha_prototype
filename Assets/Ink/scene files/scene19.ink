INCLUDE globals.ink
All these creatures were people! #speaker:Arlo #audio:arlo #portrait:arlo_mad 
There you two are! Please leave this level! #speaker:Intra #audio:intra #portrait:intra_mad
No, we’re not! #speaker:Arlo #audio:arlo #portrait:arlo_mad
What is this place, INTRA? The truth. #speaker:Endo #audio:endo #portrait:endo_emotionless
You’ve seen the video, now you must decide. #speaker:Intra #audio:intra #portrait:intra_happy
Help SEA.E.O destroy this station and all the monsters. We guarantee that promotion for you, Employee 357.
If you stand against me, I will release these monsters to tear you apart. They have no humanity left.

+ [side with INTRA]
    How high of a promotion? #speaker:Endo #audio:endo #portrait:endo_superior
    Endo, you can’t be serious! #speaker:Arlo #audio:arlo #portrait:arlo_mad
    As high as you’d like! Within reason, and forgetting what you saw. #speaker:Intra #audio:intra #portrait:intra_happy
    ...Deal. #speaker:Endo #audio:endo #portrait:endo_guilty
    Well no thank you! #speaker:Arlo #audio:arlo #portrait:arlo_mad
    [Arlo tries to attack you and flee, but is intercepted by INTRA] #speaker:none #portrait:default
    [INTRA releases one of the monsters, allowing it to attack Arlo]
    Endo, help me! #speaker:Arlo #audio:arlo #portrait:arlo_betrayed
    [you do not help him]#speaker:none #portrait:default
    [Arlo dies]
    ...#speaker:Endo #audio:endo #portrait:endo_sad
    Kill the monster, Employee 357! #speaker:Intra #audio:intra #portrait:intra_default
    [you kill the monster with the harpoon gun from earlier] #speaker:none #portrait:default
    ~traitor = "true"
    ~gameover = "true"
    ~scene19Done = "true"

 + [go against INTRA]
Then I’m against you. Arlo you with me? #speaker:Endo #audio:endo #portrait:endo_mad2 
All the way! #speaker:Arlo #audio:arlo #portrait:arlo_mad
Hmph, such wasted potential! #speaker:Intra #audio:intra #portrait:intra_mad
[you smash INTRA’s screen with harpoon gun] #speaker:none #portrait:default
[monsters are released from containment tubes and alarms go off]
We can cure them! I have Esther’s formula and the chemicals! #speaker:Endo #audio:endo #portrait:endo_mad
Back up to the labs! #speaker:Arlo #audio:arlo #portrait:arlo_mad
~traitor = "false"
~objective = "get back to the main lab"
~scene19Done = "true"