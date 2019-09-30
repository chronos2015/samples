package org.example.helloWorld;

import java.time.LocalDateTime;
import java.util.Calendar;

public class Program {
    public static void main(String agrs[]) {
        System.out.println("hello, World!");
    }

    public static boolean isEvenNumber(int test) {
        return test % 2 == 0;
    }

    public static int getYear() {
        Calendar calendar = Calendar.getInstance();
        return calendar.get(Calendar.YEAR);
    }

    public static int getAccountYear() {
        LocalDateTime d = LocalDateTime.now();
        if (d.getMonth().getValue() < 4) {
            return d.getYear() -1;
        }
        return d.getYear();
    }

    public static int getParent() {
        return getParent();
    }
}
