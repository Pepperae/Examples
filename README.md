# Examples

Considering our previous chat, I decided to find examples in the game I am currently working on to clarify some of the things you asked me about. The code is a little on the messy side as it's still in early development, but I thought it best to use recent bits of code to answer your questions. It's a small top-down JRPG style game, where the player gathers friends to fight the evil boss that's threatening their town. The things I discuss obviously have their uses outside of gaming too, but the way they are implemented might differ slightly as Unity's C# uses Mono behind the scenes. Especially because part of the code is dealt with within the editor. Anyway, I'll just go down the list of things I remember you asking me about and try to add some code references alongside it.



**Scriptables**

In order to produce all sorts of possible loot, I use a Scriptable system where I simply made one Scriptable Class called "Item" to base my items on. The way it works in Unity is actually pretty neat, as by adding the line "CreateAssetMenu" on top, you can create the items inside Unity itself and from there add the information per item. You can pass all the usual things like strings, arrays, booleans, etc, and then simply add the values yourself or check a box to set booleans to true. Scriptable objects are invaluable in some cases for what they represent. Rather than being a true copy of the data, they are references. So if you have a bunch of crates in a room that can be smashed, rather than having all of those crates individually hold the data for the items inside, you just plug in the scriptable item. Especially in bigger games this can help you save a lot of memory as you are not needlessly duplicating data.



**Interfaces**

There's a ton of different objects to turn off and on in a game, having an interface can make things a little more manageable. I've set up an interface to open and close any object regardless of whether it's a treasurechest that needs to do more than just open, or a door that uses proximity without any further input from the player. I call on active and inactive with a range of different things too, making it impractical to inherit directly. 



**Polymorphism/Inheritance/Abstract**

I do use polymorphism for other things though, like my various types of text pop-ups. I have different types of pop-ups, but at their core, they merely need to appear and disappear at the players keypresses. It's a lot easier when you can simply have several classes inherit from one core class that has the base information already worked out in this case. Interactable serves for both interface and inherited classes, but this isn't too much of an issue since I don't actually try to use the Interactable class directly anyway. 



**Singleton**

Whilst quite similar to a static class, Singletons are objects that can be references and passed around. This means that a Singleton can be globally accessible, making it easier to access certain things, like specific positions in the current NPC dialog tree. This is necessary if you want to have NPCs be in specific spots at specific moments. 



**Dependency Injection**

As an alternative, I could have used Dependency Injection. To inject a dependency basically means that you ask the dic to check which dependency to use and then it checks if it's already in use. If it is, it will use that one. Otherwise, it'll create the dependency, store it and then use that one. 



**Factory Pattern**

Another alternative could have been the Factory Pattern where you have to deal with the injection yourself, rather than letting the dic deal with it for you.



**Lambda Operator**

I couldn't immediately find a lambda expression, but really all a lambda operator does is create and assign a delegate function with an anonymous method without having to create a separate function.



I hope this was alright, I couldn't really find any one specific piece of code that would answer your questions otherwise. If you need something longer or complete, let me know.
