package org.example.helloWorld;

import com.google.gson.Gson;

public class Program {
    public static void main(String agrs[]) {
        String json = "{\"foo\":\"test\",\"bar\":15,\"baz\":true}";

        Gson gson = new Gson();
        Model model = gson.fromJson(json, Model.class);

        System.out.println(model.getFoo());
    }
}
