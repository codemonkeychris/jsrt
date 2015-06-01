﻿/// <reference path='c:\repos\xs\src\xsrt2\xsrt.ts' />
var App;
(function (App) {
    App.setInitialState = function() {
        host.setState({ 
            mode: "loading",
            text: "test"
        });
    };

    var testHandlers = {
        simple_test_1: {
            state: {
                text: "a"
            },
            ready: function(ev) {
                var label1 = ev.root.findName("label1");
                xsrt.assert(label1, "label1 should exist");
                xsrt.assert(label1 && label1.text === "1", "should have number '1' in label");
            },
            render: function() {
                return (
                    <Xaml.Grid 
                        horizontalAlignment='Stretch'
                        verticalAlignment='Stretch'
                        rows={['auto', '*']}
                        columns={['*']} >
                
                        <Xaml.TextBlock name='label1' grid$row='0'>{host.getState().text.length}</Xaml.TextBlock>
                        <MultiLineTextBox
                            name='textBox1'
                            grid$row='1'
                            grid$column='0'
                            fontFamily='Consolas'
                            fontSize='14'
                            onTextChanged={textChanged}
                            text={host.getState().text}  />
                    </Xaml.Grid>
                );
            }
        },
        simple_test_2: {
            state: {
                text: "ab"
            },
            ready: function(ev) {
                var label1 = ev.root.findName("label1");
                xsrt.assert(label1, "label1 should exist");
                xsrt.assert(label1 && label1.text === "2", "should have number '2' in label");
            },
            render: function() {
                return (
                    <Xaml.Grid 
                        horizontalAlignment='Stretch'
                        verticalAlignment='Stretch'
                        rows={['auto', '*']}
                        columns={['*']} >
                
                        <Xaml.TextBlock name='label1' grid$row='0'>{host.getState().text.length}</Xaml.TextBlock>
                        <MultiLineTextBox
                            name='textBox1'
                            grid$row='1'
                            grid$column='0'
                            fontFamily='Consolas'
                            fontSize='14'
                            onTextChanged={textChanged}
                            text={host.getState().text}  />
                    </Xaml.Grid>
                );
            }
        }
    }
    function testSetup(ev) {
        var s = testHandlers[ev.name].state;
        s.mode = "running";
        s.activeTest = ev.name;
        host.setState(s);
    }
    function testReady(ev) {
        testHandlers[ev.name].ready(ev);
    }
    xsrt.addEventListener("testsetup", testSetup);
    xsrt.addEventListener("testready", testReady);
    xsrt.registerTests(Object.keys(testHandlers));

    function textChanged(sender, e) {
        host.setState({ text: sender.text });
    }

    function MultiLineTextBox() {
        return <Xaml.TextBox
            scrollViewer$horizontalScrollBarVisibility='Auto'
            scrollViewer$verticalScrollBarVisibility='Auto'
            acceptsReturn={true}
            textWrapping='Wrap'
            horizontalAlignment='Stretch'
            verticalAlignment='Stretch' />;
    }

    function renderResults() {
        var logs = xsrt.getLogs();

        return (
            <Xaml.Grid>
                <Xaml.StackPanel>{
                    Array.prototype.map.call(logs, function (entry) {
                        return (
                            <Xaml.StackPanel orientation='Horizontal' >
                                <Xaml.TextBlock fontFamily='Consolas' text={ entry.test } />
                                <Xaml.TextBlock fontFamily='Consolas' text={(entry.result ? "       " : "failed:") + entry.message } />
                            </Xaml.StackPanel>
                        );    
                    })
                }</Xaml.StackPanel>
            </Xaml.Grid>
        );
    }

    function render() {
        switch (host.getState().mode) {
            case "running":
                return testHandlers[host.getState().activeTest].render();
            case "results":
                return renderResults();
            default:
                return <Xaml.TextBlock text='loading...' />
        }
    }
    App.render = render;
})(App || (App = {}));
