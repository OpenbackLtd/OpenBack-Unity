# OpenBack Unity Plugin

<!-- MarkdownTOC -->

- [Introduction](#introduction)
- [Unity Integration](#unity-integration)
- [OpenBack Library API](#openback-library-api)

<!-- /MarkdownTOC -->

## Introduction

<h3>Purpose and Scope</h3>

This document provides technical guidelines to install and setup the OpenBack Platform within any native smartphone application for the Android mobile operating system.

<h3>Pre-requisites</h3>

This document is intended to be used by a developer with existing experience with Unity 5+. 

<h3>Terminology</h3>

Library:	Collective software components added into the mobile app

<h3>Description</h3>

The OpenBack Platform is a smart notification platform built on a real-time rules and reporting engine that automatically uses device and other context triggers (e.g., location, device time, other apps install, signal strength etc.) to send notifications and other messages e.g. interactive display messages, offline push notifications, SMS/text messages etc. and then manage and report on those messages in a user centric way, showing messages to users at the right moment.

The library syncs with the OpenBack Dashboard by downloading and storing the campaign context rules and related content locally and efficiently on the device, meaning notifications and messages work even without a data connection. Once the context triggers and other criteria are met, the device then displays the relevant message for the user to interact with. The Dashboard and Android library support over 30 trigger sets, and multiple trigger sets can be used in any of many campaigns - this means there are over 33 million potential trigger combinations.

The library polls data back to the platform as configured in the dashboard, by uploading the data to the OpenBack Engine (OBE) via HTTPS.

<h3>Overview of Process</h3>

OpenBack is a fast and easy integration for Unity apps and should take less than 15 minutes for each app and involves:

  - Import OpenBack Unity Package
  - Import dependencies
  - Add configuration files

## Unity Integration

Download [`OpenBack.unitypackage`](OpenBack.unitypackage) from this repository. Open and import the files in your project.

### Android Configuration


Open and edit the provided `StreamingAssets/openback.json` file with your `appCode`. You can also configure the material notification icon for Android 5+. Please refer to the [Android documentation][android-init].

- **Play Services resolver**

	OpenBack uses the Unity play services jar resolver from Google. Version 1.2.11 is included in the OpenBack package but importing it is optional if you already have a newer version in your project. The resolver sources can be found [here](https://github.com/googlesamples/unity-jar-resolver).

- **Gson Library**
	
	OpenBack uses Gson from Google. Version 2.8 is included in the OpenBack package but importing it is optional if you already have a newer version in your project. The Gson library JAR file can be found [here](https://mvnrepository.com/artifact/com.google.code.gson/gson).

- **Firebase for Unity**

	Download the [Firebase SDK](https://firebase.google.com/docs/unity/setup) and import `FirebaseMessaging.unitypackage` in your project. Follow the [Setup for Android](https://firebase.google.com/docs/unity/setup#setup_for_android) steps.

- **Bootstrapping**

	We provide a package [`OpenBackUnity.unitypackage`](OpenBackUnity.unitypackage) to help bootstrap OpenBack properly on Unity. In a regular Android application, Openback is normally initialized during the `onCreate()` call of your application class. This extra package simply adds a manifest that sets the `android:name` attribute to use `com.openback.unity.UnityApplication`. 

### iOS Configuration

Open `OpenBack/Plugin/Target/iOS/OpenBackiOSNative.mm`. Update the configuration dictionary with your `App Code`. Read the [iOS Configuration][ios-init] for more info.

Build your project and open it in XCode.

For now, some manual steps are now required:

1. Add `OpenBack.framework` to the Embbed Binaries. 

2. Add a `Run Script` in the build phases to strip the framework.

Follow the steps from the [iOS Integration Guide][ios-embedded].

## OpenBack Library API

These endpoints are used for your app to interact directly with OpenBack.

### Getting the library version

```cs
OpenBack openBack = OpenBack.SharedInstance;
string version = openBack.getSdkVersion ();
```
### Setting Custom Trigger values

```cs
OpenBack openBack = OpenBack.SharedInstance;
openBack.setCustomTrigger (OpenBackTrigger.CustomTrigger1, "Hello");
openBack.setCustomTrigger (OpenBackTrigger.CustomTrigger2, 42);
openBack.setCustomTrigger (OpenBackTrigger.CustomTrigger3, 1.2345);
```

_OpenBack typically supports up to 10 custom values, if you need more please discuss with OpenBack or email integrations@openback.com_

### User Information

The application can pass some extra user information using the `OpenBackUserInfo` srtuct by setting the following fields:

| Name | Type | Description |
| ---- | ---- | ----------- |
| `AddressLine1` | String | Address line 1 |
| `AddressLine2` | String | Address line 2 |
| `AdvertisingId` | String | Advertising identifier set by the application |
| `Age` | String | Age |
| `City` | String | City |
| `Country` | String | Country |
| `CountryCode` | String | ISO-2 country code |
| `DateOfBirth` | String | Date of birth _YYYY-MM-DD_ |
| `Email` | String | Email Address |
| `FirstName` | String | First name |
| `Gender` | String | Gender |
| `OptInUpdates` | String | Opting in for campaign updates _"true"/"false"_ |
| `PhoneNumber` | String | Phone Number (international format) |
| `PostCode` | String | Postal code |
| `Profession` | String | Profession |
| `State` | String | State |
| `Surname` | String | Surname |
| `Title` | String | Title |
| `Identity1` | String | Custom user identifier 1 |
| `Identity2` | String | Custom user identifier 2 |
| `Identity3` | String | Custom user identifier 3 |
| `Identity4` | String | Custom user identifier 4 |
| `Identity5` | String | Custom user identifier 5 |

```cs
OpenBack openBack = OpenBack.SharedInstance;
OpenBackUserInfo userInfo = new OpenBackUserInfo ();
userInfo.Email = "info@openback.com";
userInfo.FirstName = "nicolas";
openBack.setUserInfo (userInfo);
```

[android-init]: https://gist.github.com/npabion/14d5420ec9b13d36d610262f3a3dc632#initializing-the-openback-library
[ios-init]: https://gist.github.com/npabion/9aa26ed1c4297819609a6d9a88c986a8#using-the-openback-library
[ios-embedded]: https://gist.github.com/npabion/9aa26ed1c4297819609a6d9a88c986a8#adding-the-openback-framework-to-your-project