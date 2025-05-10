# Unity Gesture Control V0.1

Unity Gesture Control is a Python + C# based library that enables gesture control in Unity game scenes. It allows for intuitive interaction through hand gestures, enabling developers to create immersive experiences.

**Version**: v0.1  
**Tested on**: macOS + Unity version 6000.0.45f1

<br>
<br>

## Demo 
[(Watch Full Demo on Youtube)](https://www.youtube.com/watch?v=fcrvQKrahTQ)

#### 1. **Scale (scale.cs)**
This script allows for scaling along the x, y, or z axes. You can choose to scale only one axis at a time.
![Scale Demo](https://github.com/user-attachments/assets/448c6b14-50a9-4f1e-ad91-0feb1665a0af)


#### 2. **Rotation (rotate.cs)**
This script can be applied to 3D objects, cameras, or lights, enabling rotation based on gesture input.
![Rotation Demo](https://github.com/user-attachments/assets/2d90004d-74e6-4102-a476-e0090b6dbdc9)


#### 3. **Control Object Movement (position_tracking.cs)**
This script allows object to follow hand position.
![Movement Demo](https://github.com/user-attachments/assets/81adc92d-0f22-453a-82c4-c5781674507a)

<br>
<br>

## Instructions

#### 1. **Download the Project**
Download the "Unity Gesture Control V0.1" folder from GitHub.

<br>

#### 2. **Install Python Dependencies**
Run the following command to install the required Python dependencies listed in `requirements.txt` (Unity Gesture Control V0.1/Python Runner/requirements.txt)

```bash
pip install -r path-to-requirements.txt
```

<br>

#### 3. **Run the Python Script**
Launch the Python runner by executing the script in the terminal:

```bash
python3 path-to-pythonrunner.py
```

<br>

#### 4. **Set Up Unity Scene**
Create your Unity scene and drag the desired C# script (e.g., `scale.cs`, `rotate.cs`, or `position_tracking.cs`) to the Unity Editor's control panel of the object you want to control.

![Tutorial](https://github.com/user-attachments/assets/ba2fe931-0b4f-48a7-874a-1f8f613c8ac1)

<br>

#### 5. **Run the Scene**
Once your scene is set up, run it in Unity, and the gesture control should be active.

<br>
<br>

## Notes

- **Single-Hand Tracking**: In v0.1, the library works best with one hand being tracked at a time. It can detect either the left or right hand, but performance may vary with multiple hands.
- **Single C# Script**: Only one C# script can be added to each scene in this version. Future updates will support multiple scripts.
- **Future Plans**: I'm working on integrating the Python code into a Python-Sharp package, eliminating the need to run the Python script separately in the terminal. Stay tuned for updates!

Have fun playing with gesture control!

<br>
<br>

## License

This project is open-source and available under the MIT License. See the [LICENSE](LICENSE) file for more details.

