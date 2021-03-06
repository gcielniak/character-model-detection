Character Model Detection
=========================
Using the Unity game engine to detect characters in images/screenshots with a list of provided parameters.

Follow the development here:  
[Project Progress](https://github.com/petetech/character-model-detection/wiki/Project-Progress) 

To use the scripts I have written please read:   
[Instructions for using Scripts](https://github.com/petetech/character-model-detection/wiki/Instructions-for-using-Scripts)

You can download the full demo project (CharacterDetection) or just the script files.

=========================

In this project we will be leveraging Unity to do the following things:

- Create images/screenshots of characters automatically depending on parameters. These images are composed of a regular shot (RGB) and a mask (Black and White).
- Automate the process of taking 1000s of screenshots of the rendered scene.
- These scenes will make use of multiple camera angles and lighting angles to simulate the different possibilities.

- Most importantly, the contrast in black/white in the mask image is what will be used with the training images / algorithm.


A more basic explanation:

- Take a pair of pictures, one colour one black (Random in nature, variable pose, lighting and so on).
- Store these to be used later.
- Using the algorithm and the produced image set, the area in the picture that is of closest match is our 'person'.

=========================

The expected result should be greater accuracy in detection due to the use of character outlines and no surrounding area (rectangle).

=========================

For more information please see the Wiki pages.
