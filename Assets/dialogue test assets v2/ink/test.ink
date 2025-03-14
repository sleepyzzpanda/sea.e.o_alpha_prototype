INCLUDE globals.ink 
//all ink files need to include this ink file if they need to check flags from dialogue options

{ test_var == "": -> test2 | -> dialogue_played }

=== test ===
Test dialogue #speaker:arlo #portrait:arlo_default #layout:left #audio:arlo
    + [Yes]
        Great #portrait:arlo_happy
        ~ test_var = "Great"
    + [No]
        Ok
- Well anyways #speaker:Endo #portrait:mc_default
So #speaker:Arlo #portrait:arlo_default #audio:arlo
How was your day #portrait:arlo_happy
Okay I suppose #speaker:Endo #portrait:mc_default #audio:default
I see #speaker:Arlo #portrait:arlo_default #audio:arlo
-> END

=== test2 ===
Test dialogue #speaker:Arlo #portrait:arlo_default #layout:left #audio:arlo
    + [Yes]
        Great #portrait:arlo_happy
        -> chosen("Yes")
    + [No]
        Ok
        -> chosen("No")
        
=== chosen(response) ===
~ test_var = response
Well anyways #speaker:Endo #portrait:mc_default #audio:default
So #speaker:Arlo #portrait:arlo_default #audio:arlo
How was your day #portrait:arlo_happy 
Okay I suppose #speaker:Endo #portrait:mc_default #audio:default
I see #speaker:Arlo #portrait:arlo_default #audio:arlo
Wow super long sentence to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see #audio:intra
Wow super long sentence 2 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see #audio:default
Wow super long sentence 3 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see
Wow super long sentence 4 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see #audio:arlo
Wow super long sentence 5 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see #audio:esther
Wow super long sentence 6 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see #audio:default
Wow super long sentence 7 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see 
Wow super long sentence 8 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see 
Wow super long sentence 9 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see
Wow super long sentence 10 to test out how the dialogue sentence looks. Does the text properly wrap around in the box? Let's see
-> END

=== dialogue_played ===
You already talked
-> END