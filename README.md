## VR Cooking Simulator (Meta Quest)

A Unity XR cooking game using Gamepad Controllers with physics-based interactions

## Project Overview

VR Cooking Simulator is an immersive cooking experience built in Unity for the Meta Quest platform using game controllers (not hand tracking). The project emphasizes realistic physics and interactive kitchen objects such as stove knobs, frying pans, meat cooking systems, salt and pepper shakers, fridge doors, and an instructional recipe board.

Players use the Quest controllers to grab, rotate, tilt, shake, and interact with items in a simulated kitchen. The game supports a full cooking workflow—from turning on a stove, frying a steak, seasoning food, and navigating step-by-step cooking instructions.

## Core Features
Stove & Heating System

Physical, controller-grabbable stove knobs

Knobs activate flame VFX on the burner

Configurable min/max rotation and heat states

Frying pan reacts physically to heating and contact

Meat Cooking Mechanic

Multi-stage cooking progression:

Raw → Medium → Well-done

Material color swapping to simulate real meat cooking

Timed heating while in contact with the pan surface

Salt & Pepper Shakers

Particle system for seasoning emission

Emits seasoning when:

Tilted past a threshold, or

Shaken using controller movement

Single-stream particle emission for realism

Pouring Systems (Fluids / Spices)

Particle emitters tuned for single-stream output

Angle detection ensures particles only emit when appropriate

Custom radius/angle settings to prevent offset or double beams

Fully Interactive Cabinets

Controller-grabbed door that you can pull

Player can physically pull or push the cabinet

Instruction Board (Step-by-Step Recipe Guide)

World-space UI panel with:

Next Step button

Previous Step button

Buttons use XR UI Interaction with controllers

Steps stored in a configurable array

## Screenshots

(Add screenshots inside /Screenshots folder and replace the placeholder URLs)

## Hardware & Software Requirements
Software

Unity 6000.x or newer

Unity XR Interaction Toolkit 3.2+

OpenXR Plugin

URP (Universal Render Pipeline)

Android Build Support installed via Unity Hub

Hardware

Meta Quest 2 or Meta Quest 3

Quest controllers

USB-C cable for build & run (or Wi-Fi via Oculus Air Link)

## Build Instructions

Open the project in Unity 6000.x

Go to:

File → Build Settings → Android


Click Switch Platform

Ensure these settings are enabled:

XR Plug-in Management → OpenXR (Android tab)

Feature Groups → Meta Quest Support

Connect the Quest headset

Press Build & Run

## Installation Instructions (Meta Quest)

Enable Developer Mode on your Quest through the Meta mobile app

Connect the headset via USB-C

Unity will automatically install the APK when using Build & Run

After installation:

Put on the headset

Navigate to Apps → Unknown Sources

Select VR Cooking Simulator

## Known Issues / Limitations

Meat collider requires fine-tuning to prevent floating slightly above certain pans

Meat cooked art is not perfect

Device Simulator does not always accurately simulate Quest controller grabbing

Some physics objects may fall through the floor if:

Mesh colliders are not convex, or

Rigidbody interpolation is not set properly

## Future Development Possibilities

Add more ingredients (eggs, vegetables, butter)

Implement a knife cutting mechanic using prefab slicing

Introduce a scoring and timing system

Add audio cues for chopping, seasoning

Create more complex recipes with multiple steps

Add kitchen expansion (oven, sink, counters, etc.)

UI improvements for recipe navigation

## Video Demonstration

(Add your actual project video link here)

Project Demo Video: https://your-video-url-here.com

👤 Author

Joshua Lee
Meta Quest VR Cooking Simulation Project
