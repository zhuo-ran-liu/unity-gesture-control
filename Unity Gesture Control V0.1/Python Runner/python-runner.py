from cvzone.HandTrackingModule import HandDetector
import cv2
import socket
import math

# Initialize camera
cap = cv2.VideoCapture(1)
cap.set(3, 1280)
cap.set(4, 720)

# Check if camera is open
if not cap.isOpened():
    print("Error: Could not open camera")
    exit()

# Initialize Hand Detector
detector = HandDetector(detectionCon=0.8, maxHands=1)

# Set up UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5050)

while True:
    # Capture frame
    success, img = cap.read()
    
    if not success or img is None:
        print("Failed to capture frame, skipping...")
        continue  # Skip this loop iteration if frame is not available

    img = cv2.flip(img, 1)  # Flip image horizontally

    # Detect hands and get landmark data
    hands, img = detector.findHands(img)  

    if hands:
        hand = hands[0]
        lmList = hand["lmList"]  # 21 Landmark Points
        
        # Get Thumb Tip (Point 4) and Index Finger Tip (Point 8)
        x1, y1, z1 = lmList[4]  # Thumb Tip
        x2, y2, z2 = lmList[8]  # Index Finger Tip
        distance = math.sqrt((x2 - x1)**2 + (y2 - y1)**2 + (z2 - z1)**2) # Calculate Distance
        distance_str = f"{distance:.2f}" # Format distance to 2 decimal places

        #Get Index position
        hand_type = hand["type"] # Flip hand type to correct mirroring issue
        if hand_type == "Left":
            hand_type = "Right"
        else:
            hand_type = "Left"

        if len(lmList) > 8:
            x, y, z = lmList[8] # Get Index Finger Tip (Point 8)

        message = f"{distance:.2f},{x:.2f},{y:.2f},{z:.2f}"

        # Send UDP Message
        sock.sendto(str.encode(str(message)), serverAddressPort)

        # Print distance to console
        print(f"Sent Distance (Thumb Tip to Index Finger Tip) over UDP: {message}")

    # Display the camera feed
    cv2.imshow("Image", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break  # Press 'q' to exit

cap.release()
cv2.destroyAllWindows()