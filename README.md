# RandomCodeSnippets
GaugeView - A Cross Platform UI control created entirely in Xamarin.Forms. The gauge control is colorful(4 different colors) and can be used 
in apps representing an analog pressure control meter or speedometer.

MapPage - Simple Cross Platfrom Map Page for Xamarin forms, Make Sure to Add Appropriate permissions on Android/iOS before using it as listed here: https://goo.gl/MFLNjz
This component is a small part of another app which extracts geolocation from Images and then shows those on a Map. The source of that app will be posted soon.

ImagePage - Launcher for Map Page. Reads location data from Image displayed on Page and passes it to the Map Page.
Added Image Pickup from phone gallery and location extraction via cross platform geolocator api.

The usage of MapPage and ImagePage is shown in this app youtube video: https://youtu.be/IdGaBGYr1tE

Testing of Maps Demo App on vaarious Samsung Galaxy Series Devices(S5, S6, Note 5): https://youtu.be/K1YH6POSCYw


MapsAppInfo & mapsdemo_video - Demo Video and Information of the App created using MapPage and ImagePage in Xamarin Forms.

iOSPageRenderer_Anim_Squeeze.cs - A Native Custom Page renderer for Xamarin.Forms Page. It has logic to flip a page(flip animation). Also contains logic for adding icons correctly to the top bar.

VideoPageRenderer - A Video Page Renderer for iOS which renders landscape video on iOS using Xamarin. It uses AVVideoPlayer control on iOS platform to render the video.

A Number picker control created as Xamarin Android Renderer. This was created to pick up duration in hours and minutes.
