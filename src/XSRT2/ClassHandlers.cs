﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;
using System.Reflection;
using Windows.UI.Xaml.Controls.Primitives;

namespace XSRT2 {
    public static class Handler
    {
        internal static class FrameworkElementHandler
        {
            internal static void SetProperties(FrameworkElement t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                TrySet(obj, lastObj, "grid$row", false, t, (target, x, lastX) => { target.SetValue(Grid.RowProperty, Convert.ToInt32(x.Value<double>())); });
                TrySet(obj, lastObj, "grid$column", false, t, (target, x, lastX) => { target.SetValue(Grid.ColumnProperty, Convert.ToInt32(x.Value<double>())); });
                TrySet(obj, lastObj, "relative$above", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AboveProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignBottomWith", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignBottomWithProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignBottomWithPanel", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignBottomWithPanelProperty, Convert.ToBoolean(Convert.ToBoolean(((JValue)x).Value))));
                TrySet(obj, lastObj, "relative$alignHorizontalCenterWith", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignHorizontalCenterWithProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignLeftWith", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignLeftWithProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignLeftWithPanel", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignLeftWithPanelProperty, Convert.ToBoolean(Convert.ToBoolean(((JValue)x).Value))));
                TrySet(obj, lastObj, "relative$alignRightWith", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignRightWithProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignRightWithPanel", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignRightWithPanelProperty, Convert.ToBoolean(Convert.ToBoolean(((JValue)x).Value))));
                TrySet(obj, lastObj, "relative$alignTopWith", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignTopWithProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignTopWithPanel", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignTopWithPanelProperty, Convert.ToBoolean(Convert.ToBoolean(((JValue)x).Value))));
                TrySet(obj, lastObj, "relative$alignVerticalCenterWith", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignVerticalCenterWithProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$alignVerticalCenterWithPanel", false, t, (target, x, lastX) => target.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, Convert.ToBoolean(Convert.ToBoolean(((JValue)x).Value))));
                TrySet(obj, lastObj, "relative$below", false, t, (target, x, lastX) => target.SetValue(RelativePanel.BelowProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$leftOf", false, t, (target, x, lastX) => target.SetValue(RelativePanel.LeftOfProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "relative$rightOf", false, t, (target, x, lastX) => target.SetValue(RelativePanel.RightOfProperty, namedObjectMap[x.ToString()]), defer);
                TrySet(obj, lastObj, "horizontalAlignment", false, t, (target, x, lastX) => target.HorizontalAlignment = ParseEnum<HorizontalAlignment>(x));
                TrySet(obj, lastObj, "verticalAlignment", false, t, (target, x, lastX) => target.VerticalAlignment = ParseEnum<VerticalAlignment>(x));
                TrySet(obj, lastObj, "margin", false, t, (target, x, lastX) => target.Margin = XamlStringParse<Thickness>(x));
                TrySet(obj, lastObj, "name", false, t, (target, x, lastX) => { target.Name = x.ToString(); namedObjectMap[target.Name] = target; });
            }
        }

        internal static class TextBlockHandler
        {
            internal static TextBlock Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<TextBlock>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(TextBlock t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                FrameworkElementHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "text", true, t, (target, x, lastX) => target.Text = x.ToString());
                TrySet(obj, lastObj, "fontFamily", false, t, (target, x, lastX) => target.FontFamily = new FontFamily(x.ToString()));
                TrySet(obj, lastObj, "fontSize", false, t, (target, x, lastX) => target.FontSize = x.Value<double>());
                TrySet(obj, lastObj, "fontWeight", false, t, (target, x, lastX) => target.FontWeight = ParseEnum<FontWeight>(x));
            }
        }

        internal static class TextBoxHandler
        {
            internal static TextBox Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<TextBox>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(TextBox t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "text", true, t, (target, x, lastX) => target.Text = x.ToString());
                TrySetEvent(obj, lastObj, "TextChanged", t, (target, x, lastX) => SetTextChangedEventHandler(x.ToString(), target));
            }
            static void TextChangedRouter(object sender, RoutedEventArgs e)
            {
                if (Command != null)
                {
                    var map = (Dictionary<string, string>)((FrameworkElement)sender).GetValue(eventMap);
                    Command(null, new CommandEventArgs() { CommandHandlerToken = map["TextChanged"], Sender = sender, EventArgs = e });
                }
            }
            static void SetTextChangedEventHandler(string handlerName, TextBox element)
            {
                var map = (Dictionary<string, string>)element.GetValue(eventMap);
                if (map == null)
                {
                    element.SetValue(eventMap, map = new Dictionary<string, string>());
                }
                map["TextChanged"] = handlerName;
                element.TextChanged -= TextChangedRouter;
                element.TextChanged += TextChangedRouter;
            }
        }

        internal static class ListBoxHandler
        {
            internal static ListBox Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<ListBox>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(ListBox t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ItemsControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySetEvent(obj, lastObj, "SelectionChanged", t, (target, x, lastX) => SetSelectionChangedEventHandler(x.ToString(), target));
            }
            static void SelectionChangedRouter(object sender, SelectionChangedEventArgs e)
            {
                if (Command != null)
                {
                    var map = (Dictionary<string, string>)((FrameworkElement)sender).GetValue(eventMap);
                    Command(null, new CommandEventArgs() { CommandHandlerToken = map["SelectionChanged"], Sender = sender, EventArgs = e });
                }
            }
            static void SetSelectionChangedEventHandler(string handlerName, ListBox element)
            {
                var map = (Dictionary<string, string>)element.GetValue(eventMap);
                if (map == null)
                {
                    element.SetValue(eventMap, map = new Dictionary<string, string>());
                }
                map["SelectionChanged"] = handlerName;
                element.SelectionChanged -= SelectionChangedRouter;
                element.SelectionChanged += SelectionChangedRouter;
            }
        }

        internal static class ItemsControlHandler
        {
            internal static ItemsControl Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<ItemsControl>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(ItemsControl t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "itemsSource", false, t, (target, x, lastX) => { RuntimeHelpers.SetItemsSource(target, x); });
            }
        }

        internal static class SliderHandler
        {
            internal static Slider Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<Slider>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(Slider t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "minimum", false, t, (target, x, lastX) => target.Minimum = x.Value<double>());
                TrySet(obj, lastObj, "maximum", false, t, (target, x, lastX) => target.Maximum = x.Value<double>());
                TrySet(obj, lastObj, "value", false, t, (target, x, lastX) => target.Value = x.Value<double>());
                TrySetEvent(obj, lastObj, "ValueChanged", t, (target, x, lastX) => SetValueChangedEventHandler(x.ToString(), target));
            }
            static void ValueChangedRouter(object sender, RangeBaseValueChangedEventArgs e)
            {
                if (Command != null)
                {
                    var map = (Dictionary<string, string>)((FrameworkElement)sender).GetValue(eventMap);
                    Command(null, new CommandEventArgs() { CommandHandlerToken = map["ValueChanged"], Sender = sender, EventArgs = e });
                }
            }
            static void SetValueChangedEventHandler(string handlerName, Slider element)
            {
                var map = (Dictionary<string, string>)element.GetValue(eventMap);
                if (map == null)
                {
                    element.SetValue(eventMap, map = new Dictionary<string, string>());
                }
                map["ValueChanged"] = handlerName;
                element.ValueChanged -= ValueChangedRouter;
                element.ValueChanged += ValueChangedRouter;
            }
        }

        internal static class ButtonHandler
        {
            internal static Button Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<Button>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(Button t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ButtonBaseHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
            }
        }
        internal static class CalendarDatePickerHandler
        {
            internal static CalendarDatePicker Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<CalendarDatePicker>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(CalendarDatePicker t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
            }
        }
        internal static class CalendarViewHandler
        {
            internal static CalendarView Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<CalendarView>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(CalendarView t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
            }
        }
        internal static class RelativePanelHandler
        {
            internal static RelativePanel Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<RelativePanel>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(RelativePanel t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                PanelHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
            }
        }

        internal static class CheckBoxHandler
        {
            internal static CheckBox Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<CheckBox>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(CheckBox t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ButtonBaseHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "isChecked", false, t, (target, x, lastX) => target.IsChecked = Convert.ToBoolean(((JValue)x).Value));
                TrySetEvent(obj, lastObj, "Checked", t, (target, x, lastX) => SetCheckedEventHandler(x.ToString(), target));
            }
            static void CheckedRouter(object sender, RoutedEventArgs e)
            {
                if (Command != null)
                {
                    var map = (Dictionary<string, string>)((FrameworkElement)sender).GetValue(eventMap);
                    Command(null, new CommandEventArgs() { CommandHandlerToken = map["Checked"], Sender = sender, EventArgs = e });
                }
            }
            static void SetCheckedEventHandler(string handlerName, CheckBox element)
            {
                var map = (Dictionary<string, string>)element.GetValue(eventMap);
                if (map == null)
                {
                    element.SetValue(eventMap, map = new Dictionary<string, string>());
                }
                map["Checked"] = handlerName;
                element.Checked -= CheckedRouter;
                element.Checked += CheckedRouter;
            }
        }

        internal static class ButtonBaseHandler
        {
            internal static void SetProperties(ButtonBase t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                ControlHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "content", true, t, (target, x, lastX) => target.Content = CreateFromState(x, lastX, namedObjectMap, defer));
                TrySetEvent(obj, lastObj, "Click", t, (target, x, lastX) => SetClickEventHandler(x.ToString(), target));
            }
            static void ClickRouter(object sender, RoutedEventArgs e)
            {
                if (Command != null)
                {
                    var map = (Dictionary<string, string>)((FrameworkElement)sender).GetValue(eventMap);
                    Command(null, new CommandEventArgs() { CommandHandlerToken = map["Click"], Sender = sender, EventArgs = e });
                }
            }
            static void SetClickEventHandler(string handlerName, ButtonBase element)
            {
                var map = (Dictionary<string, string>)element.GetValue(eventMap);
                if (map == null)
                {
                    element.SetValue(eventMap, map = new Dictionary<string, string>());
                }
                map["Click"] = handlerName;
                element.Click -= ClickRouter;
                element.Click += ClickRouter;
            }
        }

        internal static class ControlHandler
        {
            internal static void SetProperties(Control t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                FrameworkElementHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "background", false, t, (target, x, lastX) => target.Background = XamlStringParse<Brush>(x));
                TrySet(obj, lastObj, "foreground", false, t, (target, x, lastX) => target.Foreground = XamlStringParse<Brush>(x));
                TrySet(obj, lastObj, "fontFamily", false, t, (target, x, lastX) => target.FontFamily = new FontFamily(x.ToString()));
                TrySet(obj, lastObj, "fontSize", false, t, (target, x, lastX) => target.FontSize = x.Value<double>());
                TrySet(obj, lastObj, "fontWeight", false, t, (target, x, lastX) => target.FontWeight = ParseEnum<FontWeight>(x));
            }
        }

        internal static class StackPanelHandler
        {
            internal static StackPanel Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<StackPanel>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(StackPanel t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                PanelHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
            }
        }
        internal static class GridHandler
        {
            internal static Grid Create(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                var createResult = CreateOrGetLast<Grid>(obj, namedObjectMap);
                SetProperties(createResult.Item2, obj, createResult.Item1 ? lastObj : null, namedObjectMap, defer);
                return createResult.Item2;
            }
            internal static void SetProperties(Grid t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                PanelHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                TrySet(obj, lastObj, "rows", false, t, (target, x, lastX) => { PanelHandler.SetGridRowDefinitions(target, (JArray)x); });
                TrySet(obj, lastObj, "columns", false, t, (target, x, lastX) => { PanelHandler.SetGridColumnDefinitions(target, (JArray)x); });
            }
        }

        internal static class PanelHandler
        {
            internal static void SetGridRowDefinitions(Grid t, JArray obj) 
            {
                t.RowDefinitions.Clear();
                foreach (var d in obj.AsJEnumerable())
                {
                    RowDefinition rd = new RowDefinition();
                    rd.Height = XamlStringParse<GridLength>(d);
                    t.RowDefinitions.Add(rd);
                }
            }
            internal static void SetGridColumnDefinitions(Grid t, JArray obj) 
            {
                t.ColumnDefinitions.Clear();
                foreach (var d in obj.AsJEnumerable())
                {
                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = XamlStringParse<GridLength>(d);
                    t.ColumnDefinitions.Add(cd);
                }
            }

            static void SetPanelChildren(Panel t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                Handler.FrameworkElementHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                List<UIElement> children = new List<UIElement>();
                IJEnumerable<JToken> lastChildren = null;
                JToken last;
                if (lastObj != null && lastObj.TryGetValue("children", out last))
                {
                    lastChildren = last.AsJEnumerable();
                }
                CollectPanelChildrenWorker(t, obj["children"].AsJEnumerable(), lastChildren, children, namedObjectMap, defer);
                var setChildrenNeeded = false;
                if (t.Children.Count == children.Count)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        if (!object.ReferenceEquals(children[i], t.Children[i]))
                        {
                            setChildrenNeeded = true;
                        }
                    }
                }
                else
                {
                    setChildrenNeeded = true;
                }

                if (setChildrenNeeded)
                {
                    t.Children.Clear();
                    foreach (var child in children)
                    {
                        var parent = VisualTreeHelper.GetParent(child) as Panel;
                        if (parent != null)
                        {
                            parent.Children.Remove(child);
                        }
                        t.Children.Add(child);
                    }
                }
            }
            static void CollectPanelChildrenWorker(Panel t, IJEnumerable<JToken> items, IEnumerable<JToken> lastItems, List<UIElement> children, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                IEnumerator<JToken> enumerator = null;
                if (lastItems != null)
                {
                    enumerator = lastItems.GetEnumerator();
                    enumerator.Reset();
                }
                foreach (var child in items)
                {
                    JToken lastChild = null;
                    if (enumerator != null && enumerator.MoveNext()) { lastChild = enumerator.Current; }

                    if (child.Type == JTokenType.Array)
                    {
                        CollectPanelChildrenWorker(t, child.AsJEnumerable(), lastChild != null ? lastChild.AsJEnumerable() : null, children, namedObjectMap, defer);
                    }
                    else
                    {
                        var instance = CreateFromState((JObject)child, lastChild as JObject, namedObjectMap, defer);
                        children.Add(instance);
                    }
                }
            }
            internal static void SetProperties(Panel t, JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
            {
                FrameworkElementHandler.SetProperties(t, obj, lastObj, namedObjectMap, defer);
                SetPanelChildren(t, obj, lastObj, namedObjectMap, defer);
            }

        }


        static DependencyProperty eventMap = DependencyProperty.RegisterAttached("XSEventMap", typeof(Dictionary<string, string>), typeof(FrameworkElement), PropertyMetadata.Create((object)null));
        static Dictionary<string, CreateCallback> handlers;

        public static event EventHandler<CommandEventArgs> Command;

        static Tuple<bool, T> CreateOrGetLast<T>(JObject obj, Dictionary<string, object> namedObjectMap) where T:new()
        {
            JToken name;
            if (obj.TryGetValue("name", out name))
            {
                object value;
                if (namedObjectMap.TryGetValue(name.ToString(), out value))
                {
                    if (value != null && value is T)
                    {
                        return new Tuple<bool, T>(true, (T)value);
                    }
                }
            }
            return new Tuple<bool, T>(false, new T());
        }
        static void TrySet<T>(JObject obj, JObject last, string name, T target, Setter<T> setter)
        {
            TrySet<T>(obj, last, name, false, target, setter);
        }
        static void TrySet<T>(JObject obj, JObject last, string name, bool aliasFirstChild, T target, Setter<T> setter)
        {
            TrySet<T>(obj, last, name, aliasFirstChild, target, setter, null);
        }
        static void TrySet<T>(JObject obj, JObject last, string name, bool aliasFirstChild, T target, Setter<T> setter, List<DeferSetter> defer)
        {
            JToken tok;
            JToken tokLast = null;
            bool found = false;
            if (!obj.TryGetValue(name, out tok))
            {
                if (aliasFirstChild && obj.TryGetValue("children", out tok))
                {
                    found = true;
                    tok = ((JArray)tok).First;
                }
            }
            else
            {
                found = true;
            }
            if (found)
            {
                if (last != null && last.TryGetValue(name, out tokLast))
                {
                    if (tokLast.ToString() == tok.ToString())
                    {
                        return; // bail early if old & new are the same
                    }
                }
                if (defer != null) 
                {
                    defer.Add(new DeferSetter<T>() { setter = setter, target = target, tok = tok, tokLast = tokLast });
                }
                else 
                {
                    setter(target, tok, tokLast);
                }
            }
        }
        static void TrySetEvent<T>(JObject obj, JObject last, string name, T target, Setter<T> setter)
        {
            JToken tok;
            JToken tokLast = null;
            if (obj.TryGetValue("on" + name, out tok))
            {
                if (last != null && last.TryGetValue("on" + name, out tokLast))
                {
                    if (tokLast.ToString() == tok.ToString())
                    {
                        return; // bail early if old & new are the same
                    }
                }
                setter(target, tok, tokLast);
            }
        }
        static T ParseEnum<T>(JToken v) 
        {
            return (T)Enum.Parse(typeof(T), v.ToString());
        }
        static T XamlStringParse<T>(JToken v)
        {
            return (T)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(T), v.ToString());
        }
        static Dictionary<string, CreateCallback> GetHandlers()
        {
            if (handlers == null)
            {
                handlers = new Dictionary<string, CreateCallback>();
                handlers["TextBlock"] = TextBlockHandler.Create;
                handlers["TextBox"] = TextBoxHandler.Create;
                handlers["ListBox"] = ListBoxHandler.Create;
                handlers["ItemsControl"] = ItemsControlHandler.Create;
                handlers["Slider"] = SliderHandler.Create;
                handlers["Button"] = ButtonHandler.Create;
                handlers["CalendarDatePicker"] = CalendarDatePickerHandler.Create;
                handlers["CalendarView"] = CalendarViewHandler.Create;
                handlers["RelativePanel"] = RelativePanelHandler.Create;
                handlers["CheckBox"] = CheckBoxHandler.Create;
                handlers["StackPanel"] = StackPanelHandler.Create;
                handlers["Grid"] = GridHandler.Create;
            }
            return handlers;
        }
        internal static FrameworkElement CreateFromState(JToken item, JToken lastItem, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer)
        {
            if (item.Type == JTokenType.Object)
            {
                var type = item["type"].ToString();
                CreateCallback create;
                if (GetHandlers().TryGetValue(type, out create))
                {
                    return create((JObject)item, (JObject)lastItem, namedObjectMap, defer);
                }
                return new TextBlock() { FontSize = 48, Text = "'" + type + "'Not found" };
            }
            else
            {
                return new TextBlock() { Text = item.ToString() };
            }
        }

        internal delegate void Setter<T>(T target, JToken value, JToken lastValue);
        delegate FrameworkElement CreateCallback(JObject obj, JObject lastObj, Dictionary<string, object> namedObjectMap, List<DeferSetter> defer);

        internal abstract class DeferSetter
        {
            public abstract void Do();
        }
        internal class DeferSetter<T> : DeferSetter
        {
            internal T target;
            internal JToken tok;
            internal JToken tokLast;
            internal Setter<T> setter;

            public override void Do()
            {
                setter(target, tok, tokLast);
            }
        }

    }
}

