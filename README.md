
# Unity Gesture Control V0.1
Unity Gesture Control is a python + C# based library that allows gesture control in game scenes.

For v0.1 it is only tested on Mac + Unity version 6000.0.45f1 on desktop apps, for other versions, feedbacks are welcome.


## Demo
#### Scale (scale.cs)
(demo scene)
(photo of interface)
possibility to turn on only x, y or z axis scaling

#### Rotation (rotate.cs)
(demo scene)
(photo of interface)
can be applied to 3D object, camera or light, for this effect

#### Control Object Movement (position_tracking.cs)
(demo scene)
(photo of interface)




## Instructions
1. Download "Unity Gesture Control V0.1" folder from Github
2. Install all python dependencies in requirments.txt
3. Run python runner script in terminal (It's in Unity Gesture Control V0.1/Python Runner/pythonrunner.py)
`python3 path-to-pythonrunner.py-file`

5. Create your unity scene, drag the C# effect script in xx folder to the control panel of the object you want to control with gesture.
![](demos/instruction.mp4)

6. Running the scene


## Notes

- For v0.1, although there is some level of auto hand detection, it works better with one single hand tracking, please try to use one hand at the time (left or right hand it doesn't matter).

- For v0.1, it is only possible to add one C# script for each scene.

- I'm currently working on baking it into a python-sharp package independent without having to run python script separitely in terminal, stay tuned!

Have fun playing!