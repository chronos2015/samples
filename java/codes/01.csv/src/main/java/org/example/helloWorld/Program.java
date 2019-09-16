package org.example.helloWorld;

import org.apache.commons.csv.CSVFormat;
import org.apache.commons.csv.CSVParser;
import org.apache.commons.csv.CSVPrinter;
import org.apache.commons.csv.CSVRecord;

import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

public class Program {
    public static void main(String agrs[]) throws IOException {
        try (CSVPrinter printer = new CSVPrinter(new FileWriter("csv.txt"), CSVFormat.RFC4180)) {
            printer.printRecord("sample", "1", "2", "3", "4");
        }
        try (CSVPrinter printer = new CSVPrinter(new FileWriter("csv2.txt"), CSVFormat.EXCEL)) {
            printer.printRecord("sample", "1", "2", "3", "4");
        }

        try (CSVParser parser = new CSVParser(new FileReader("csv.txt"), CSVFormat.EXCEL)) {
            List<CSVRecord> record = parser.getRecords();
            if(record.get(0).get(0) == "sample") {
                System.out.println("success");
            }
        }
    }
}
