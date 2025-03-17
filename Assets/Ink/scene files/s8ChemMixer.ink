INCLUDE globals.ink
{scene20Done == "true": -> scene21 | -> scene8}
=== scene8 ===
The unneeded chemicals are to be neutralized. Please do so when you can! #speaker:Intra #audio:intra #portrait:intra_default
~chem_mixer = "true"
-> END

=== scene21 ===
Done! AI room to destroy INTRA first, let’s go! #speaker:Endo #audio:endo #portrait:endo_mad
~scene21Done = "true"
-> END