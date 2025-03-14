INCLUDE globals.ink
{s5objsDone == "false": -> main| -> final}

=== main ===
The containment suit is for when you go outside the station. #speaker:Arlo #audio:arlo #portrait:arlo_default
~showers = "true"
{s5objsDone == "true" : -> final | -> END}
=== final ===
Well, that concludes the 2nd floor tour. Let’s head back down so INTRA can explain the rest. #speaker:Arlo #audio:arlo #portrait:arlo_default
-> END

