# MHW Custom challenges bot BETA

A Discord.Net bot that was made to manage custom challenges in Monster Hunter: World for me and my friends. The idea is to challenge your friends in an arena quest with a specific weapon that neither of you are proficient with. For me it gives a competitive drive, and also motivates me to become better to that specific weapon.

Currently it is a big bunch of spaghetticode because it was rushed, but it will (hopefully) get refactored in the future. Files with the fileName **\_template** .ext are meant to be copied. Afterwards remove the **\_template** part and then edit according to the commented guide.

# Todo:
* Refactor ALOT!
* Put commands into their own class
* Validate image exist on !addscore
* Make sure (most) commands and args are case-insensitive
* Make .txt files as summary for each command
* Add !commands, !rm, !rms
* Add an singleton application class
* Put database into the application class. Possible bad practice: exposes db to the whole project.
* Put emotes into their own class
* Save a list of bot mods in singleton

#NuGet packages:
* Discord.Net
* sqlite-net-pcl
* Microsoft-Extensions.Configuration.Json
