package com.openback.unity;

import android.app.Application;
import android.content.Context;
import android.util.Log;

import com.openback.OpenBack;

public class UnityApplication extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        // OpenBack GO!
        initOpenBack();
    }

    private void initOpenBack() {
        try {
            Context context = getApplicationContext();
            OpenBack.start(new OpenBack.Config(context));
        } catch (Exception e) {
            Log.d("OpenBack", "OpenBack start error", e);
        }
    }
}
