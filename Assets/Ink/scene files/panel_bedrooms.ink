INCLUDE globals.ink
{s5objsDone == "false": -> main| -> final}

=== main ===
What’s this? Your handywork? #speaker:Endo #audio:endo #portrait:endo_mad
No no, I didn’t touch anything over there! #speaker:Arlo #audio:arlo #portrait:arlo_surprised
Weird, I’ll take a look later. #speaker:Endo #audio:endo #portrait:endo_thinking
~bedroom = "true"
{s5objsDone == "true" : -> final | -> END}
=== final ===
Well, that concludes the 2nd floor tour. Let’s head back down so INTRA can explain the rest. #speaker:Arlo #audio:arlo #portrait:arlo_default
-> END