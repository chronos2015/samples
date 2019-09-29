package org.example.yaml;

import org.yaml.snakeyaml.Yaml;
import org.yaml.snakeyaml.constructor.Constructor;

import java.io.ByteArrayInputStream;
import java.nio.charset.StandardCharsets;

public class Program {
    public static void main(String agrs[]) {
        String test = "first_name: Test first\r\n"
                + "lastName: Test last\r\n";
        ByteArrayInputStream stream = new ByteArrayInputStream(test.getBytes(StandardCharsets.UTF_8));
        Yaml yaml = new Yaml(new Constructor(Person.class));
        Object obj = yaml.load(stream);
        if(obj instanceof Person) {
            Person person = (Person) obj;
            System.out.println(person.getFirstName());
            System.out.println(person.getLastName());
        }
    }
}
