ABSTRACT:
Augmented Reality is a rapidly growing technology with use cases in many avenues like healthcare, entertainment, military, etc. Our goal in this project was to leverage this powerful tool to create a 3D AR Coloring book to create a more fun and interactive experience. We want to create a 3D model of a 2D image that gets textured real time based on how the kid is colouring the 2D image. We are using several online datasets and Industry graded tools like Maya, Unity, Vuforia, Jupyter etc.
It consists of two main components, the first part consists of a Convoluted net that converts a 2D image into a 3D model, which will be using an existing database containing labeled high definition RGB images, for the model to create a 3D Mesh of the image and the second would be Live rendering the 3d model to apply the necessary textures so that the colour painted by the on the page would be reflected on the 3D model this would involve image recognition, Realtime Rendering and Motion Tracking.
Since it is a semester project, we won’t doing the research from ground up but will work by leveraging existing research in fields of machine learning, Convoluted Neural Networks, image rendering, image recognition, UV texturing. We have also used modules like AR camera, Region Capture, etc.
By the end of this project we hope to create a robust application that kids would love and have fun while using it.


1. INTRODUCTION
1.1. The emergence of mobile phones and technologies like augmented reality and their applications have created sudden explosion in products services leveraging this technology. We also want to be part of this revolution by creating an application for kids which can use AR and machine learning to create a 3D model of images from coloring book, the kid can color the drawing which will be reflected on the 3d model making the coloring activity a fun activity. By doing this we hope to tap the vast market of kid’s entertainment and we also hope to leverage this technology to improve kids’ education techniques.

1.2. Problem Statement
In the Current world, given the popularity of digital devices and media, old school activities like colouring and drawing seem unexciting for children. Colouring books capture the imagination of children and are one of the first places where creativity blooms. Augmented reality holds the potential to solve this problem by bridging the real world with the digital world. So many of the old school fun activities like board games, drawing, colouring even reading comics and picture books can be made fun and exciting using Augmented Reality. Our project apart from re-innovating colouring books can also be used as a basis for introducing Augmented reality in picture books, comics and board games. 


2. BACKGROUND RESEARCH
We investigated quite a few resources like similar products, journals, current research and other sources to not only understand the technological aspect butt also understand the feasibility of our project, 
There are a few companies that have been working on this domain and have already launched, few of them are QuiverVision, Color4D and 4EXPIEREINCE but they give their own colouring books/pages to the user. They work by creating 3D models manually then giving the user the 2D picture of that specific model, what we want to create is an app that can basically work on as many ordinary coloring books as possible.
We realized that there would be two components to this project a machine learning component which will be used to develop the 3D mesh and a rendering component to render live textures. Naturally each component has its own challenges, while trying to design a machine learning model we looked into several papers(links provided below) and articles and decided a Convolutional Neural Net would be the right architecture for our model. We also learned about an unforeseen problem i.e. Dissimilar to a 2D picture that has just a single all-inclusive portrayal in computer format (pixel), there are numerous approaches to speak to 3D information in computerized design. They accompany their very own points of interest and drawbacks, so the selection of information portrayal legitimately influenced the methodology that can be used. These are Voxel it is a 3D pixel with vertices, edges and dimensions, Point Cloud which comprises 3D coordinates of points, Polygon Mesh here the spatial grid is extended into volume grid each possesing its own advantages and challenges[1]. We also faced challenges where each type of drawing had different 3d interpretations so we had to work our way around that problem as well.
 
 We will try to learn about these advantages and disadvantages in the future as we progress in our app and will try to replicate their advantages and not have their disadvantages. Disney has researched similar topic in 2015 and published a bit of their work but they haven’t updated about it afterward[2]. Since Disney has not published it’s work and hasn’t updated anything about this project, the differences couldn’t be clearly understood. 
 
These are a few Papers and articles that we have referred:
https://www.theverge.com/2015/10/5/9453703/disney-research-augmented-reality-coloring-books
http://candycat1992.github.io/attach/papers/2017-magictoon.pdf
https://ieeexplore.ieee.org/document/7165658
https://github.com/lkhphuc/pytorch-3d-point-cloud-generation
https://medium.com/vitalify-asia/create-3d-model-from-a-single-2d-image-in-pytorch-917aca00bb07
https://towardsdatascience.com/2d-or-3d-a-simple-comparison-of-convolutional-neural-networks-for-automatic-segmentation-of-625308f52aa7

2.1. Proposed System
With this project we plan to bring in AR technology in the field of kids entertainment, by leveraging machine learning models and other tools create an interactive 3D coloring book application. We plan to do this by using two main components, the first is a convoluted neural network which generates a 3D mesh and second would be creating a tool that can do live texturing which enables the interactive part of our application. Kids entertainment is billion dollar market which is why companies like Disney are looking into it and we want to tap into this market to enable growth of our project and ourselves.


3.1. Goals / Vision
Our initial goal was to create a 3D colouring book application, where the colours applied by kid a drawing book will be reflected on the application, all of this was supposed to be done live, we had several checkpoints to determine our progress, we needed to create a neural net that can generate and a 3D mesh from a 2D image, region capture, UV mapping of the model and mainly being able to apply live textures, but in the end were unable to create a UV map for the 3D mesh created by the machine learning model due to which we created passive 3D models.

3.2. Delivered Solution
As we already mentioned we were able to meet most of our initial targets by creating both a machine learning model that create 3D mesh using a 2D image and also an application that can target image and perform real time texturing of the 3D model based on the colouring done by our subject, we were also able to put everything into an application which is easy to use. But we were unable to generate UV maps for 3D mesh developed by our machine learning models so we had to rely on passive generated 3D mesh.

3.3. Remaining Work
Although most of the work is done we still have one objective left incomplete, we have to map 3D meshes actively generated by convolutional autoencoder, but we were unable to that instead we opted to use a passively generated model as proof of concept for the second phase of our project. This is a huge challenge and we were handicapped by time, as we had to develop an algorithm that creates a UV map of any 3D shape and which also should cross platform, although we did a few attempts the results were not as per acceptable standards so choose to discard these, and look into this further to develop a more robust AR Application
 


