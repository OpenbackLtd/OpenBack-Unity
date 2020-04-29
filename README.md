# OpenBack Unity Plugin

Check the full OpenBack Unity plugin documention [here](https://docs.openback.com/plugins/unity/).

## Unity packages

There are 3 packages available:

* [OpenBack.unitypackage](OpenBack.unitypackage?raw=true)

    This package contains the OpenBack plugin only.

* [OpenBackWithResolver.unitypackage](OpenBackWithResolver.unitypackage?raw=true)

    This package contains the OpenBack plugin + the Google resolver _(v1.2.153)_.

* [OpenBackBootstrap.unitypackage](OpenBackBootstrap.unitypackage?raw=true)

    This package contains the bootstrap library for Android for single dex application.

* [OpenBackBootstrap-MultiDex.unitypackage](OpenBackBootstrap-MultiDex.unitypackage?raw=true)

    This package contains the bootstrap library for Android for multi dex application.

> When using multi-dex, make sure to add it to your dependencies (see [OpenBackDependencies.xml](Unity/OpenBack/Editor/OpenBackDependencies.xml))

## Android Bootstrap

This small project simply builds a library `OpenBackBootstrap.aar` for single and multiple dex. In a regular Android application, Openback is normally initialized during the onCreate() call of your application class. This extra package simply provides a manifest that sets the `android:name` attribute to `com.openback.unity.UnityApplication`. The application class simply starts OpenBack.

Open using `Import project (Gradle, Eclipse ADT, etc...)` in Android Studio. To build all flavors, open the Gradle tab and select `build` in the `Android-Bootstrap > Tasks > Build` section. It will generate 4 aar in the build folder (singleDex and multiDex for debug and release types).

> It is recommended to the Gradle build system in your Unity project for the manifest merge to work properly.

If your application already uses a custom Application class, add some code to initialize OpenBack. Check the [Android documentation](https://docs.openback.com/android/integration).

