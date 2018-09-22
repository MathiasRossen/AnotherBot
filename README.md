# MHW Custom challenges bot BETA

A Discord.Net bot that was made to manage custom challenges in Monster Hunter: World for me and my friends. The idea is to challenge your friends in an arena quest with a specific weapon that neither of you are proficient with. For me it gives a competitive drive, and also motivates me to become better to that specific weapon.

Currently it is a big bunch of spaghetticode because it was rushed, but it will (hopefully) get refactored in the future. Files with the fileName **\_template** .ext are meant to be copied. Afterwards remove the **\_template** part and then edit according to the commented guide.

# Todo:
* Refactor ALOT! ✔
* Put commands into their own class ✔
* Validate image exist on !addscore ✔
* Make sure (most) commands and args are case-insensitive
* Make .txt files as summary for each command ✔
* Add !commands, !rm, !rms ✔
* Add an singleton application class ✔
* Put database into the application class. Possible bad practice: exposes db to the whole project. ✔
* Put emotes into their own class ✔
* Save a list of bot mods in singleton ✔
* Test thoroughly, furiously and intensely

* Rewrite rules for adding scores. 
I found a problem with MHW only saving the 5 best times you've had for a specific arena run. This means that if you have 5 runs with a DB, let's say 2 mins, 3 mins, 4 mins, 5 mins, 6 mins. If you then start a new challenge with another weapon for example Light Bowgun on 7 mins, then you wouldn't be able to retrieve the intended image for the score. So i'm rewriting the way that users enter scores, so that they will no longer contain an image, but instead the contender will keep a screenshot of their kill screen in case someone claims fake. This does not guarantee that times are genuine, but neither did the old method. After all, this is designed to be used in a small environment, where legitimacy is not a problem.

# NuGet packages:
* Discord.Net
* sqlite-net-pcl
* Microsoft-Extensions.Configuration.Json
