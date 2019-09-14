package org.example.helloWorld;

import io.dropwizard.Application;
import io.dropwizard.setup.Environment;

public class Program extends Application<MyConfiguration> {
    public static void main(String[] args) throws Exception {
        new Program().run(args);
    }

    @Override
    public void run(MyConfiguration configuration, Environment environment) throws Exception {
    }
}
