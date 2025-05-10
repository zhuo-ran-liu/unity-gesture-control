

# Unity Gesture Control V0.1

Unity Gesture Control is a Python + C# based library that enables gesture control in Unity game scenes. It allows for intuitive interaction through hand gestures, enabling developers to create immersive experiences.

**Version**: v0.1  
**Tested on**: macOS + Unity version 6000.0.45f1 (Desktop Apps)  
*For other versions, feedback is welcome!*


## Demo

#### 1. **Scale (scale.cs)**
<video src="demos/gesture_control_zoom.mp4" autoplay loop muted playsinline width="600"></video>  
This effect allows for scaling along the x, y, or z axes. You can choose to scale only one axis at a time.

#### 2. **Rotation (rotate.cs)**
<video src="demos/gesture_control_rotate.mp4" autoplay loop muted playsinline width="600"></video>  
This effect can be applied to 3D objects, cameras, or lights, enabling rotation based on gesture input.

#### 3. **Control Object Movement (position_tracking.cs)**
<video src="demos/gesture_control_follow_pos.mp4" autoplay loop muted playsinline width="600"></video>  
This feature enables object movement tracking in real time, with gestures controlling the position of objects within the scene.



## Instructions

### 1. **Download the Project**
Download the "Unity Gesture Control V0.1" folder from GitHub.

### 2. **Install Python Dependencies**
Run the following command to install the required Python dependencies listed in `requirements.txt` e.g.

```bash
pip install -r path-to-requirements.txt
```

### 3. **Run the Python Script**
Launch the Python runner by executing the script in the terminal:

```bash
python3 path-to-pythonrunner.py
```

### 4. **Set Up Unity Scene**
- Create your Unity scene and drag the desired C# script (e.g., `scale.cs`, `rotate.cs`, or `position_tracking.cs`) to the Unity Editor's control panel of the object you want to control.

<video src="demos/instruction.mp4" autoplay loop muted playsinline width="600"></video>

### 5. **Run the Scene**
Once your scene is set up, run it in Unity, and the gesture control should be active.


## Notes

- **Single-Hand Tracking**: In v0.1, the library works best with one hand being tracked at a time. It can detect either the left or right hand, but performance may vary with multiple hands.
- **Single C# Script**: Only one C# script can be added to each scene in this version. Future updates will support multiple scripts.
- **Future Plans**: I'm working on integrating the Python code into a Python-Sharp package, eliminating the need to run the Python script separately in the terminal. Stay tuned for updates!

---

## License

This project is open-source and available under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

### Have fun playing with gesture control!
