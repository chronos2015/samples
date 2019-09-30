package org.example.jediterm;

import com.google.common.collect.Maps;
import com.jediterm.terminal.ProcessTtyConnector;
import com.jediterm.terminal.RequestOrigin;
import com.jediterm.terminal.TabbedTerminalWidget;
import com.jediterm.terminal.TtyConnector;
import com.jediterm.terminal.ui.JediTermWidget;
import com.jediterm.terminal.ui.TerminalPanelListener;
import com.jediterm.terminal.ui.TerminalSession;
import com.jediterm.terminal.ui.TerminalWidget;
import com.jediterm.terminal.ui.UIUtil;
import com.jediterm.terminal.ui.settings.DefaultTabbedSettingsProvider;
import com.pty4j.PtyProcess;
import com.pty4j.WinSize;

import javax.swing.AbstractAction;
import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.SwingUtilities;
import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.nio.charset.StandardCharsets;
import java.util.Map;

public class Example {

    private static JediTermWidget openSession(TerminalWidget terminal) {
        if (terminal.canOpenSession()) {
            TtyConnector result;
            try {
                Map<String, String> envs = Maps.newHashMap(System.getenv());
                String[] command;

                if (UIUtil.isWindows) {
                    command = new String[]{"cmd.exe"};
                } else {
                    command = new String[]{"/bin/bash", "--login"};
                    envs.put("TERM", "xterm");
                }

                PtyProcess process = PtyProcess.exec(command, envs, null);

                result = new ProcessTtyConnector(process, StandardCharsets.UTF_8) {
                    private PtyProcess myProcess = process;

                    @Override
                    protected void resizeImmediately() {
                        if (getPendingTermSize() != null && getPendingPixelSize() != null) {
                            myProcess.setWinSize(new WinSize(
                                    getPendingTermSize().width,
                                    getPendingTermSize().height,
                                    getPendingPixelSize().width,
                                    getPendingPixelSize().height));
                        }
                    }

                    @Override
                    public String getName() {
                        return "Local";
                    }

                    @Override
                    public boolean isConnected() {
                        return myProcess.isRunning();
                    }
                };
            } catch (Exception e) {
                throw new IllegalStateException(e);
            }
            JediTermWidget session = terminal.createTerminalSession(result);
            session.start();
            return session;
        }
        return null;
    }

    private static void sizeFrameForTerm(final JFrame frame, TerminalWidget myTerminal) {
        SwingUtilities.invokeLater(() -> {
            Dimension d = myTerminal.getPreferredSize();
            d.width += frame.getWidth() - frame.getContentPane().getWidth();
            d.height += frame.getHeight() - frame.getContentPane().getHeight();
            frame.setSize(d);
        });
    }

    public static void main(String[] args) {
        TerminalWidget myTerminal = new TabbedTerminalWidget(new DefaultTabbedSettingsProvider(), Example::openSession) {
            @Override
            public JediTermWidget createInnerTerminalWidget() {
                return new JediTermWidget(getSettingsProvider());
            }
        };

        final JFrame frame = new JFrame("JediTerm");

        frame.addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(final WindowEvent e) {
                System.exit(0);
            }
        });

        final JMenuBar mb1 = new JMenuBar();
        final JMenu m = new JMenu("File");

        AbstractAction myOpenAction = new AbstractAction("New Session") {
            public void actionPerformed(final ActionEvent e) {
                openSession(myTerminal);
            }
        };
        m.add(myOpenAction);
        mb1.add(m);

        frame.setJMenuBar(mb1);
        sizeFrameForTerm(frame, myTerminal);
        frame.getContentPane().add("Center", myTerminal.getComponent());

        frame.pack();
        frame.setLocationByPlatform(true);
        frame.setVisible(true);
        frame.setSize(644, 456);

        frame.setResizable(true);

        myTerminal.setTerminalPanelListener(new TerminalPanelListener() {
            public void onPanelResize(final Dimension pixelDimension, final RequestOrigin origin) {
                if (origin == RequestOrigin.Remote) {
                    sizeFrameForTerm(frame, myTerminal);
                }
                frame.pack();
            }

            @Override
            public void onSessionChanged(final TerminalSession currentSession) {
                frame.setTitle(currentSession.getSessionName());
            }

            @Override
            public void onTitleChanged(String title) {
                frame.setTitle(myTerminal.getCurrentSession().getSessionName());
            }
        });

        openSession(myTerminal);
    }
}
