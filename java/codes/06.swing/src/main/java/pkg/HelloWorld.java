package pkg;

import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import java.awt.Color;
import java.awt.Font;

public class HelloWorld {
    public static void main(String[] args) {
        JFrame mainFrame = new MainFrame();
        mainFrame.setTitle("サンプル");
        mainFrame.getContentPane().setBackground(new Color(0X3C3F41));
        mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        mainFrame.setLocationRelativeTo(null);

        JMenuBar menuBar = new JMenuBar();
        menuBar.setBackground(new Color(0X3C3F41));
        mainFrame.setJMenuBar(menuBar);

        JMenu menuFile = new ActionMenu();
        menuFile.setForeground(new Color(0XBBBBBB));
        menuFile.setText("File");
        menuFile.setMnemonic('F');
        menuFile.setFont(new Font("Monospaced", Font.PLAIN, 11));
        menuBar.add(menuFile);

        mainFrame.pack();
        mainFrame.setLocationRelativeTo(null);
        mainFrame.setVisible(true);
        mainFrame.setSize(640, 480);
    }
}
