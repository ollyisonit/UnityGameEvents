# Unity Game Events
A framework for organizing and executing events in Unity.

## Features
- Execute simple actions without needing to write more scripts
- Run groups of GameEvents in order or simultaneously
- Fast-forwarding feature allows you to skip directly to the part of your game you're working on
- Support for commonly executed Unity actions, as well as the ability to call any no-argument method or Coroutine on any object by name
- GameEvents attach to objects in the Unity editor and can be reordered with drag-and-drop
- Easy-to-implement framework for creating your own custom GameEvents, GameEventTriggers, and more

## What is a GameEvent?
A GameEvent is an action that is attached to an object in the Unity editor. It can either be executed instantly or take place over multiple frames. This library comes with a variety of GameEvents to execute frequently used actions, such as enabling and disabling objects, setting animation triggers, and loading scenes.

## How to use GameEvents
In order to use a GameEvent, you need to attach it to a GameObject. Then, you need to create a GameEventTrigger, which will tell the GameEvent when to execute. You can also use parallel and sequence events to execute a series of events in order based on how they're organized in the editor.

## GameEventTriggers
GameEventTriggers are objects that start the execution of GameEvents. In order to use a GameEventTrigger, you need to attach it to a GameObject and tell it which GameEvent to start. This library comes with two GameEventTriggers:

 - TriggerOnStart: Triggers its GameEvent when the game starts
 - TriggerOnButton: Triggers its GameEvent when its UI button is pressed

You can create your own GameEventTrigger by extending the GameEventTrigger class and calling the StartEvent() method when you want your trigger to start its associated event.

## Parallel and Sequence GameEvents
GameEventParallel and GameEventSequence are GameEvents that will execute GameEvents attached to their in-editor child objects. 

### GameEventSequence
A GameEventSequence is a GameEvent that will execute all of the GameEvents attached to its children in order from top to bottom, one after another, starting with the GameEvents attached to the same GameObject as itself. If multiple GameEvents are attached to the same object, it will execute them in the order they appear in the inspector from top to bottom. 
When using a GameEventSequence, you need to specify how it decides which GameEvents to execute. This is done using the 'Execute On Parent' tickbox, which specifies whether the GameEvents attached to the same object as the GameEventSequence should be executed, and the 'Recursion Mode' dropdown, which gives three options:

 - TopLevelOnly: Only adds GameEvents that are the sequence's direct children
 - OnlyIfEmpty: Adds GameEvents that are the sequence's direct children or the children of objects that have no GameEvents themselves. This means that GameEvents that are the children of other GameEvents will be excluded
 - Complete: Adds all GameEvents from the sequence's children, both direct and indirect

### GameEventParallel
A GameEventParallel is a GameEvent that will run its child GameEvents at the same time, and wait for all of them to finish before moving on. It has two modes:

 - OnSelf: Executes all of the GameEvents attached to the same object as the GameEventParallel at the same time
 - InChildren: Takes each of its direct children and executes its GameEvents as if the child object has a GameEventSequence attached. You are able to specify the Recursion Mode that the sequences should use when deciding which GameEvents to run

## Setters
Setters are GameEvents that instantly set objects' values. They also let you decide whether you want to overwrite the values you are setting or add to them. This library includes the following scripts to set values:

 - SetBool
 - SetColor
 - SetFloat
 - SetInt
 - SetString
 - SetVector2
 - SetVector3

Each of these scripts uses an [Accessor](https://github.com/dninosores/UnityValueAccessors) to allow you to potentially set the value of any field in the Unity editor. You can create your own Setter by extending the SetValueEvent class.

## Interpolators
Interpolators are GameEvents that work similar to Setters, but instead of changing the value instantly, they change it over time. The Interpolators that come with this library are:

 - ColorInterpolator
 - FloatInterpolator
 - IntInterpolator
 - Vector2Interpolator
 - Vector3Interpolator

You can create your own interpolators by extending the AbstractInterpolator class.

## General Use GameEvents
This library also contains a variety of GameEvents that execute common actions. Each of these GameEvents uses [Accessors](https://github.com/dninosores/UnityValueAccessors) to allow you to reference values on other objects, generate random values, and more.

 - LoadSceneAsync: Starts loading a scene in the background
 - ActivateAsyncScene: Tells a LoadSceneAsync to activate the scene that it is loading
 - CallReflected: Executes any no-argument method or coroutine on an object by name. If given a Coroutine, it will wait until the Coroutine finishes before moving on.
 - ChangeAudioPlayback: Plays, pauses, or stops an AudioSource
 - LoadScene: Loads and activates a scene
 - LoopGameEvent: Runs a GameEvent a certain number of times
 - MixerSnapshotTransition: Tells an AudioMixer to transition to a different snapshot
 - PlayOneShot: Plays an AudioClip from an AudioSource
 - PrintMessage: Prints a message to the console
 - SetActive: Activates or deactivates a GameObject
 - SetEnabled: Activates or deactivates a component
 - SetAnimationParameter: Sets an animation bool, float, int, or trigger
 - StopGameEvent: Stops a GameEvent that is currently running
 - WaitForGameEvent: Waits until the given GameEvent is finished before continuing
 - WaitSeconds: Waits for a certain number of seconds before continuing

## Fast-Forwarding
Fast-forwarding is a feature that allows you to skip over GameEvents in your scene. It works by executing a large chunk of GameEvents in a single frame, which puts your game in the same state that it would have been in if you waited for all of the GameEvents to execute on their own. In order to use fast-forwarding, you will need to use the ToggleFastForward GameEvent. Use the event to toggle fast-forwarding on when you want to start fast-forwarding, and toggle it off when you want to stop fast-forwarding. 
Note that this feature is only intended to be used for development, when it may be useful to skip over parts of your game so you can get to the part that you're testing.

## Creating your own GameEvents
GameEvents are designed to be easy to implement yourself. When you extend the GameEvent class, there are three methods you need to implement:

 - InstantInternal: A boolean value that indicates whether the GameEvent should be executed all in a single frame or take place over multiple frames
 - Stop: If your GameEvent runs over a period of multiple frames, this method should cancel it
 - RunInternal: A coroutine that represents the actual functionality of your GameEvent

Additionally, if you want your GameEvent to function differently when it's fast-forwarding vs running normally, you should override the ForceRunInstant() method. This is useful if you have a GameEvent that would ordinarily wait for the player to complete an action; trying to fast-forward an event like that would result in an infinite loop if it was forced to execute in a single frame.

### InstantGameEvents
For GameEvents that will always execute in a single frame (like Setters), custom creation is even easier. If you extend the InstantGameEvent class, there is only one method you need to override:

 - InstantEvent(): Executes the event in a single frame

## Installation
Download or clone this repository and add it to your Unity project's assets folder.

## Dependencies
This library requires that your Unity project also contains the [UnityValueAccessors](https://github.com/dninosores/UnityValueAccessors) and [UnityEditorAttributes](https://github.com/dninosores/UnityEditorAttributes) libraries.
