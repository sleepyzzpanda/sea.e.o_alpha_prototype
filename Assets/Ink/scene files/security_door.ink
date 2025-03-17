INCLUDE globals.ink
~security_door = "true"
~objective = "Return to Main Lab"
{scene20Done == "false":-> main | -> END}
=== main ===
Alright, I’ve locked them out for a while! #speaker:Arlo #audio:arlo #portrait:arlo_uneasy 
Do you think we should just leave it here?
    * [Kill monster]
        [You kill the monster with the harpoon gun from earlier] #speaker:none #portrait:none
        There. That'll stop it from chasing us. #speaker:Endo #audio:endo #portrait:endo_emotionless
        You - #speaker:Arlo #audio:arlo #portrait:arlo_uneasy
        Hey! That was a person too! We could have saved them!
        We need to focus on saving everyone else first. There has to be more of them in the building.#speaker:Endo #audio:endo #portrait:endo_emotionless 
        Lets go back to the lab. 
        ~ending = "bad"
        ~scene20Done = "true"
        -> END
    * [Stun monster]
        [You shine your flashlight at the monster] #speaker:none #portrait:none
        [it screeches and skitters away]
        Uh- #speaker:Arlo #audio:arlo #portrait:arlo_uneasy
        Where did it go?
        It ran off I think #speaker:Endo #audio:endo #portrait:endo_scared
        Come on, we need to get back to the lab. #speaker:Endo #audio:endo #portrait:endo_thinking
        ~scene20Done = "true"
        ~ending = "good"
        -> END