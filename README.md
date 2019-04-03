# OpenBack Unity Plugin

Check the full OpenBack Unity plugin documention [here](https://docs.openback.com/plugins/unity/).

## Unity packages

There are 3 packages available:

* [OpenBack.unitypackage](OpenBack.unitypackage)

    This package contains the OpenBack plugin only.

* [OpenBackWithResolver.unitypackage](OpenBackWithResolver.unitypackage)

    This package contains the OpenBack plugin + the Google resolver _(v1.2.102)_.

* [OpenBackBootstrap.unitypackage](OpenBackBootstrap.unitypackage)

    This package contains the bootstrap library for Android.

## Android Bootstrap

This small project simply builds a library `OpenBackBootstrap.aar`. In a regular Android application, Openback is normally initialized during the onCreate() call of your application class. This extra package simply provides a manifest that sets the `android:name` attribute to `com.openback.unity.UnityApplication`. The application class simply starts OpenBack.

It is recommended to the Gradle build system in your Unity project for the manifest merge to work properly.

If your application already uses a custom Application class, add some code to initialize OpenBack. Check the [Android documentation](https://docs.openback.com/android/integration).

