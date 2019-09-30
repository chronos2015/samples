import org.apache.velocity.VelocityContext;
import org.apache.velocity.app.Velocity;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.io.StringWriter;

public class HelloWorld {
    public static void main(String[] args) {
        Velocity.init();
        VelocityContext context = new VelocityContext();
        String template = "sample";
        Reader templateReader = new BufferedReader(new InputStreamReader(new ByteArrayInputStream(template.getBytes())));

        StringWriter writer = new StringWriter();
        Velocity.evaluate(context, writer, "logTagName", templateReader);
        System.out.print(writer.toString());
    }
}
