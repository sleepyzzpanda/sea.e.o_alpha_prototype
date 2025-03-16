INCLUDE globals.ink
{s5objsDone == "false": -> main| -> final}

=== main ===
I left your boxes outside, wasn’t sure where you wanted them. #speaker:Arlo #audio:arlo #portrait:arlo_default
[press “z” to push the boxes into the room] #speaker:none
{s5objsDone == "true" : -> final | -> END}
=== final ===
Well, that concludes the 2nd floor tour. Let’s head back down so INTRA can explain the rest. #speaker:Arlo #audio:arlo #portrait:arlo_default
-> END
~objective = "Return to the 1st floor"