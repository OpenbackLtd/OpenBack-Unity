package com.openback.unity;

import android.content.Context;
import android.support.multidex.MultiDexApplication;
import android.util.Log;

import com.openback.OpenBack;

public class UnityApplication extends MultiDexApplication {
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
