package org.example.helloWorld;

import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;

public class Program {
    public static void main(String agrs[]) throws IOException {
        String json = "{\"foo\":\"test\",\"bar\":15,\"baz\":true}";

        ObjectMapper mapper = new ObjectMapper();
        Model model = mapper.readValue(json, Model.class);
        System.out.println(model.getFoo());
    }
}
