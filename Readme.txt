a. Yupeng Yang(Matt)
b. YY3303
c. 02/22/2023
d. Mac OS Ventura 13.0
e. 2021.3.17f1
f. iOS/iOS 16.2/iPhone 14 Pro Max
g. Description of your project, what you did, and how you accomplished it. Please also mention mandatory functionalities you were not able to implement, if any.

For this project, I have implemented two scenes, multiple functions and two simple UI system. 

*******************************************************************************
*********************************  Scene  *************************************
*******************************************************************************


I created two scenes, one is the start game menu. The first scene included the game title, play button and quit button. The player can decide play or quit on this scene. If they hit play, then the main scene load.

The main scene included a spacecraft(cube), a planet(sphere), an orbital belt(Toru), moons(sphere), space trash(varies), two satellites(long rectangle), and an out-of-bound volume(cube). Objects are shown in different size/shape and color to indicate their uniqueness. The Planet is the biggest object in the scene, I created it by sphere and used a free asset Earth shape from the unity asset store. Then I use pro-builder to create a Toru and use semi-transparent purple to make it more like an orbital belt. Next, I made some smaller spheres in blue serve as moons. There are 4 kinds of trashes. Firstly, the planet trash, it's light blue in cylinder shape. Secondly, the moon trash, it's yellow in cylinder shape as well. Thirdly, I used a free asset coca-cola from the unity asset store, only four in the scene so higher value trash. The fourth one is irregular shape in deep green, it generate after the space craft collide with Planet/Moon/Satellite. The satellites are two red shape rectangle orbiting the Planet that are not following the orbit belt, in its own distance and orbital velocity. I also created an out-of-bound volume in cube shape. It is not rendered so player can't notice it. A trigger is with the out-of-bound volume, also not rendered. Once the player hit on, it will trigger and bring the player back to an respawn point and shown some text. 

Also, I attached two cameras on the player object, one front/main camera, one back camera with larger FOV. I also used two light sources, one Directional light and one spot light. The directional light cast shadows and can see the appearance of the spacecraft is affected by the light. Also the spot light is attached to the front of the spacecraft, it is able to cyclically rotate back and forth over a small angle to the left and right of the spacecraft’s direction of travel,


*******************************************************************************
*********************************  Functions  *********************************
*******************************************************************************


I created multiple functions. 

Firstly, I use rotation method to set up the moon trash orbit moon, and moon orbit the Planet. The rotation speed and radius are adjustable. So based on the distance difference, The farther an object’s orbit is from the planet, the slower its orbital velocity and the longer its orbital period. The moon trash are child of moon, they rotates around the moon no matter how the moon orbiting to the planet. And moons are the child of planet, they keeps orbiting around the planet. The same rotation method applied to the satellites as well, but they are rotating in different distance and velocity, not following the orbit belt. 

Secondly, for the lunar space trash, I used instantiate and coroutine methods, so that the moon can accumulate space trash over adjustable time. Also, I made sure the farther the orbit of a piece of lunar space trash is from its moon, the slower its orbital velocity and the longer its orbital period. Each piece of New/Old lunar space trash rotate separately around its own axis with an orientation and rotation period.

Thirdly, the playercontrol script contains the most number of methods. I extracted joystick input value to be able to control the movement of the player object. I enabled the touchscreen method so that I can simulate touch by mouse click. I made the timer method to keep track of time. I set count text to calculate trash collected value. By OnTriggerEnter method, I was able to detect collisions between player and objects, if trash, add value, if not trash, decrease value. Inside the OnTriggerEnter method, I used function to find the closest collision coordinate between player and object. If the player hit non-trash object, it will Instantiate new trash from the point of collision and Instantiate explosive effect while hit message is delivered to the player. Score and health bar will both decrease.

Inside the playercontrol script, a boost method also declared to accomplish accelerate/decelerate on the player object. If the player touches trash, the trash will become green indicating it success collected. Meanwhile, the score is incremented. However, I wasn't able to accomplish function to let the trash "beam" towards the spacecraft. I was only able to make the trash change color and got destroied. 

I also implemented audio function to play sound when successfully collecting and failed collecting. If the player touches planet/moon/satellites, failed sound will play, the object become red and score decrease. I used camera.screenpointtoray function and raycasting to accomplish touch struct.

For the out-of-bound volume, I used collider.ontriggerstay to detect if the player object is stay inside the volume. If it hits the volume, it will trigger function to bring player object back to a respawn point also decrease 50 points for go out too far. A return message also shown to alert player.

For the camera, I added a listener to the "back camera" button. The function temporarily enable the back camera and let user view objects that the spacecraft passes. I tried adding 'shake' effect to the camera when the player object hits moon, but it didn't work.

For the lights, directional light and spot light. I made spot light the children of the player object that it follows the player object. The Spot Light is attached to the front of the spacecraft, and cyclically rotate back and forth over a small angle to the left and right of the spacecraft’s direction of travel. The directional light is on the top and can show the shadow effect on the player object.


*******************************************************************************
*********************************  UI System  *********************************
*******************************************************************************

Regarding on the instruction, I created two UI systems. Main UI and start menu UI. The start menu contains the game title, a play button and a quit button. The play button load the main scene after user click on it. The quit button allow user to quit the game.

Once the player click on play button, the main scene load with the UI. The main UI system contains a timer, a health bar, score, joystick, and action buttons. Action buttons including the boost, the back camera, and the restart. 


h. How you applied the Nielsen usability heuristics.


#1. Visibility of system status

In my design, I deliver message and feedbacks to users to keep them understand. For example, if the user collide on moon/planet/satellite, an explosive effect will generate, a "hit!" Message delivered, and health/score decrease. The player then understand they should not collide on those. Instead, when the player hit on trashes, the score will go up. They then know these are trashes they should collect. Once the player touch on trashes, the trash will become green and score goes up indicating they collect the right trash. If they touches any non-trash object, the object will then become red with score decrement, indicating they should not touch on those. Once the timer runs out and not enough points was collected, player receives loose text telling that they lost the game. If they collected enough points before the timer runs out, they receives win text telling that they win the game. In my game design, I keep users informed about what's going on, through appropriate feedback within a reasonable amount of time.

#2. Match between system and the real world

In my design, the planet and the orbit belt should make users familiar. Also, I put in some coca-cola cans(free asset) and decorate the planet with earth shape. The explosive effect also reveal what's gonna happen if two object collide in the universe in real life. It is following real-world conventions, making information appear in a natural and logical order.

#3. User control and freedom

In my design, players has their freedom. For example, the restart button allows the player to restart the game at any time. If the player doesn't want to play the game, the have the option to quit the game at the start menu. Users can always switch between the back camera and front camera to check stuff.

#4. Consistency and standards

In my design, the first scene with start/quit button follows the industry conventions. If the player enter the game, they can soon find the joystick control familiar. Also the timer, health bar, and score is common for most games. The player won't have to wonder about the game. Users may not know what's trash in the beginning, but a learning curve of identifying trashes it' what I believe the fun it.

#5. Error prevention

In my design, I prevent the errors that player may experience. For example, the out-of-bound volume keeps the player stay inside the game. It can prevent them got errors flying away from the planet. 

#6. Recognition rather than recall

My UI system is very straightforward. Boost button show in 'boost', back camera button show in 'back camera'. The user doesn't have to memorize which button is which. By hit message, explosive effect, health bar, and score, I let users realize what's collectible objects and what's not collectible objects.

#7. Flexibility and efficiency of use

The boost button helps users to shortcut their way to collect trashes and shorten the time to wait. The back camera button allows users to see what they passes and make plans to collect pass by trashes.

#8. Aesthetic and minimalist design

The UI design is very minimalist. Start with the game title, play button, and quit button, to main UI contains all the action buttons and statue. There are no irrelevant information providing to the user. Also, the UI is simple and straightforward, which has a simple aesthetic.

#9. Help users recognize, diagnose, and recover from errors

Error message in my design are very simple and straightforward. For example, if the spacecraft hit on plant, a "Hit!" message shown. If they trigger the out-of-bound volume, a "Return to position" message shown. If they win the game, the "You Win!" message shown. All the error message are expressed in plain language and precisely indicate the problem.

#10. Help and documentation

For this assignment, I didn't get a chance to write a document for users to help them get familiar with the game faster. But I will keep it in mind, make sure to do something helpful for players.


i. Any problems you overcame (both coding and technical)

Problem Lists: 
1. Hierarchy(who's who's child?)
2. Joystick(couldn't figure out the input value and math for a long time)
3. Boost(didn't implement into the update(), it was not boosting in the beginning. But it's working now)
4. Accumulate moon trash(took me a while to understand the instantiate&coroutine)
5. The out-of-bound volume(my player object always got pushed away and could not trigger to respawn the player.)
6. The raycasting&touch(still can't figure out until this point)

j. A list of any free assets you used.
	1. Low Poly Earth
	2. Coca-cola Can
	3. Joystick spirit
	4. Fish(deleted)
	5. Natural sound pack

LINK TO VIDEO - https://youtu.be/c-stxsn5ZiY